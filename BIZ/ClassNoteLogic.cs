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
        private ClassNotes _classNotes = new();
        private ClassFileHandler _fileHandler = new();
        private string _filePath = "notes.json";

        public ObservableCollection<Note> Notes => new(_classNotes.Notes);

        public ClassNoteLogic()
        {
        }

        public void AddNewNote(string selectedText)
        {
            var newNote = new Note { Content = $"{selectedText}" };
            _classNotes.Notes.Add(newNote);
            Notify(nameof(Notes));
        }

        public void DeleteAllNotes()
        {
            _classNotes.Notes.Clear();
            Notify(nameof(Notes));
        }

        public void LoadNotes()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string initialDirectory = Path.Combine(documentsPath, "Flextek", "ErrorCatching");

            string filePath = _fileHandler.LoadFromFile(initialDirectory);

            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = _filePath;
            }

            SaveNotes();
            _filePath = filePath;

            string json = File.ReadAllText(filePath);
            var notesFromJson = JsonConvert.DeserializeObject<List<Note>>(json);
            if (notesFromJson != null)
            {
                _classNotes.Notes = notesFromJson;
                Notify(nameof(Notes));
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

        public void SaveNotes()
        {
            string filePath = _filePath;
            string json = JsonConvert.SerializeObject(Notes, Formatting.Indented);
            File.WriteAllText(filePath, json);
            File.WriteAllText("notes.json", json);
        }

        public void SaveNoteToFilePath()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string initialDirectory = Path.Combine(documentsPath, "Flextek", "ErrorCatching");

            string filePath = _fileHandler.LoadFromFile(initialDirectory);
            _filePath = filePath;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Fil stien kunne ikke findes.");
            }

            string json = JsonConvert.SerializeObject(Notes, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
