using System;
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
            BIZ.AddNewNote("Ny Note", "", false, 0);
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
            // Assuming you have a way to identify the selected note to delete
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem?.Parent as ContextMenu;
            var textBox = contextMenu?.PlacementTarget as TextBox;
            if (textBox != null && textBox.DataContext is Note note)
            {
                BIZ.RemoveSpecificNote(note);
            }
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
            BIZ.LoadNotesFromFilePath();
        }

        private void SaveNotesButton_Click(object sender, RoutedEventArgs e)
        {
            BIZ.SaveNoteToFilePath();
        }

        private void CreateNoteWithMarkedTextButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(ErrorTextBox.Selection.Start, ErrorTextBox.Selection.End);
            string copiedText = textRange.Text;
            int startIndex = GetSelectionStartIndex(ErrorTextBox);

            BIZ.AddNewNote("Ny Note", copiedText, true, startIndex);
        }

        private int GetSelectionStartIndex(RichTextBox richTextBox)
        {
            TextPointer startPointer = richTextBox.Document.ContentStart;
            TextPointer selectionStartPointer = richTextBox.Selection.Start;

            // Calculate the offset by getting the number of characters between the start of the document and the start of the selection.
            int offset = startPointer.GetOffsetToPosition(selectionStartPointer);

            // The offset includes symbols that are not actual text characters (like paragraph symbols), so we adjust the count by subtracting 1 for each symbol encountered.
            // This adjustment is a rough approximation and might need refinement based on your document's structure and content.
            int textOffset = 0;
            TextPointer pointer = startPointer;
            while (pointer != null && pointer.CompareTo(selectionStartPointer) < 0)
            {
                if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text ||
                    pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.None)
                {
                    textOffset++;
                }

                if (pointer.GetPositionAtOffset(1, LogicalDirection.Forward) == null)
                    break;

                pointer = pointer.GetPositionAtOffset(1, LogicalDirection.Forward);
            }

            return textOffset;
        }


        private void DeleteAllErrorButton_Click(object sender, RoutedEventArgs e)
        {
            BIZ.ClearErrorText();
        }

        private void NoteClick_Handler(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Note note)
            {
                // Find the text range for the note's copied text within the RichTextBox's document
                TextRange range = FindTextInRange(ErrorTextBox.Document.ContentStart, ErrorTextBox.Document.ContentEnd,
                    note.CopiedText);

                if (range != null)
                {
                    ErrorTextBox.Selection.Select(range.Start, range.End);
                    ErrorTextBox.Focus();
                }
            }
        }

        private TextRange FindTextInRange(TextPointer start, TextPointer end, string text)
        {
            while (start != null && start.CompareTo(end) < 0)
            {
                if (start.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = start.GetTextInRun(LogicalDirection.Forward);
                    int index = textRun.IndexOf(text, StringComparison.CurrentCultureIgnoreCase);
                    if (index >= 0)
                    {
                        TextPointer startText = start.GetPositionAtOffset(index);
                        TextPointer endText = start.GetPositionAtOffset(index + text.Length);
                        return new TextRange(startText, endText);
                    }
                }

                start = start.GetNextContextPosition(LogicalDirection.Forward);
            }

            return null;
        }
    }
}