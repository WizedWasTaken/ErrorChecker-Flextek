using System.Collections.Generic;
using Newtonsoft.Json;
using Repository;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using IO;
using Repository.models;
using System;

namespace BIZ
{
    public class ClassNoteLogic : ClassErrorHandling
    {
        private readonly ClassNotes _classNotes = new();
        private readonly ClassFileHandler _fileHandler = new();
        private string _filePath = "notes.json";

        public ObservableCollection<Note> Notes => new(_classNotes.Notes);

        public ClassNoteLogic()
        {
            _classNotes = new ClassNotes();
            LoadNotes();
        }

        public void AddNewNote(string content, string copiedText, bool isFromErrorTextBox, int startIndex)
        {
            _classNotes.Notes.Add(new Note { Content = $"{content}", CopiedText = $"{copiedText}", IsFromErrorTextBox = isFromErrorTextBox, StartPosition = startIndex });
            Notify(nameof(Notes));
        }

        public void DeleteAllNotes()
        {
            _classNotes.Notes.Clear();
            Notify(nameof(Notes));
        }

        public void LoadNotes()
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var notesFromJson = JsonConvert.DeserializeObject<List<Note>>(jsonString);
                if (notesFromJson != null)
                {
                    _classNotes.Notes = notesFromJson;
                    Notify(nameof(Notes));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Der skete en fejl under indlæsning af noter. \n\n" + e.Message);
            }
        }

        public string CopyAllNotes()
        {
            string copyValue = string.Empty;
            foreach (var note in Notes)
            {
                copyValue += $"{note.Content}\n";
            }

            return copyValue;
        }

        public void RemoveSpecificNote(Note note)
        {
            if (!_classNotes.Notes.Contains(note))
            {
                MessageBox.Show("Noten blev ikke fundet? \n\nVed ikke hvordan det her er muligt... Det virkede ikke i test, så fandt en ret varm hotfix :D");
                return;
            }

            _classNotes.Notes.Remove(note);
            Notify(nameof(Notes));
        }

        public void SaveNoteToFilePath()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string initialDirectory = Path.Combine(documentsPath, "Flextek", "ErrorCatching");

            string filePath = ClassFileHandler.LoadFromFile(initialDirectory);
            _filePath = filePath;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Fil stien kunne ikke findes.");
                return;
            }

            File.WriteAllText(filePath, JsonConvert.SerializeObject(Notes, Formatting.Indented));
        }

        public void LoadNotesFromFilePath()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string initialDirectory = Path.Combine(documentsPath, "Flextek", "ErrorCatching");
            string filePath = ClassFileHandler.LoadFromFile(initialDirectory);
            _filePath = filePath;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Fil stien kunne ikke findes.");
                return;
            }

            string jsonString = File.ReadAllText(filePath);
            {
                var notesFromJson = JsonConvert.DeserializeObject<List<Note>>(jsonString);
                if (notesFromJson != null)
                {
                    _classNotes.Notes = notesFromJson;
                    Notify(nameof(Notes));
                }
            }
        }

        public void SaveNotes()
        {
            string json = JsonConvert.SerializeObject(Notes, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
