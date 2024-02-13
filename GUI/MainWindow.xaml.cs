using BIZ;
using Repository;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BIZ.models;
using Note = Repository.models.Note;

namespace GUI
{
    public partial class MainWindow : Window
    {
        private readonly ClassBiz BIZ;
        public MainWindow()
        {
            InitializeComponent();
            BIZ = new ClassBiz();
            this.DataContext = BIZ;
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            BIZ.AddNewNote();
        }

        private void DeleteAllNotesButton_Click(object sender, RoutedEventArgs e)
        {
            BIZ.DeleteAllNotes();
        }

        protected override void OnClosed(EventArgs e)
        {
            BIZ.SaveNotes();
            base.OnClosed(e);
        }

        /// <summary>
        /// Makes it possible to delete notes by right clicking them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            BIZ.RemoveSpecificNote((Note)((Button)sender).DataContext);
        }

        /// <summary>
        /// Makes it possible to scroll, even tho hovering text boxes (Notes)
        ///
        /// TODO: Buggy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                };

                var parent = NotesListBox.Parent as UIElement;
                parent?.RaiseEvent(eventArg);
            }
        }

        /// <summary>
        /// Makes it impossible for the resize to make the first column larger than the window
        /// User is and will always be an ass hat 👌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FirstColumn.MaxWidth = Application.Current.MainWindow.ActualWidth - 50;
        }

        /// <summary>
        /// Context Menu item to save all notes to the users clip path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyAllNotesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!e.Handled)
            {
                string copyValue = BIZ.CopyAllNotes();
                Clipboard.SetText(copyValue);
            }
        }

        private void LoadNotesButton_Click(object sender, RoutedEventArgs e)
        {
            BIZ.LoadNotes();
        }
    }
}