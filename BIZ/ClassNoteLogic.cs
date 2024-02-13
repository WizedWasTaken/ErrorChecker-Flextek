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
    {
        private ClassNotes _classNotes = new();
        private ClassFileHandler _fileHandler = new();
        private string _filePath = "notes.json";

        public ObservableCollection<Note> Notes => new(_classNotes.Notes);

        public ClassNoteLogic()
        {
            LoadNotes();
        }

        {
            Notify(nameof(Notes));
        }

        public void DeleteAllNotes()
        {
            Notify(nameof(Notes));
        }

        public void LoadNotes()
        {
            {
                var notesFromJson = JsonConvert.DeserializeObject<List<Note>>(json);
                if (notesFromJson != null)
                {
                    _classNotes.Notes = notesFromJson;
                    Notify(nameof(Notes));
                }
            }
            catch (FileNotFoundException)
            {
                // Do nothing
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

        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string initialDirectory = Path.Combine(documentsPath, "Flextek", "ErrorCatching");

            string filePath = _fileHandler.LoadFromFile(initialDirectory);
            _filePath = filePath;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Fil stien kunne ikke findes.");
        }

        public void SaveNotes()
        {
            string json = JsonConvert.SerializeObject(Notes, Formatting.Indented);
        }
    }
}
