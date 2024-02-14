using System;
using System.IO;
using System.Text;
using System.Windows;
using IO;
using Repository;

namespace BIZ
{
    public class ClassErrorHandling : ClassNotify
    {
        private readonly ClassFileHandler _fileHandler = new ClassFileHandler();
        public ClassText ErrorText { get; set; }

        public ClassErrorHandling()
        {
            ErrorText = new ClassText();
            FindAndLoadErrorFile();
        }

        private void FindAndLoadErrorFile()
        {
            try
            {
                //Inital Directory to "This PC"
                string initialDirectory = "";
                // TODO: Kig på encoding
                string filePath = _fileHandler.LoadFromFile(initialDirectory);

                string text = File.ReadAllText(filePath);
                if (text.Length > 0)
                {
                    ErrorText.Text = text;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Der skete en fejl under indlæsning af fejltekst. \n\n" + e.Message);
            }
        }

        public void ClearErrorText()
        {
            ErrorText.Text = "";
        }
    }
}