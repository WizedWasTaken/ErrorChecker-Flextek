using System.Collections.Generic;
using Newtonsoft.Json;
using Repository;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using IO;
using Repository.models;

namespace BIZ
{
    public class ClassNoteLogic : ClassNotify
    {
        private ClassNotes _classNotes = new();

        public ObservableCollection<Note> Notes => new(_classNotes.Notes);

        public ClassNoteLogic()
        {
            LoadNotes();
        }

        public void AddNewNote()
        {
            var newNote = new Note { Content = "New Note" };
            Notes.Add(newNote);
            Notify(nameof(Notes));
        }

        public void DeleteAllNotes()
        {
            Notes.Clear();
            Notify(nameof(Notes));
        }

        public void LoadNotes()
        {
            try
            {
                string json = File.ReadAllText("notes.json");
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
                return;
            }

            _classNotes.Notes.Remove(note);
            Notify(nameof(Notes));
        }

        public void SaveNotes()
        {
            string json = JsonConvert.SerializeObject(Notes, Formatting.Indented);
            File.WriteAllText("notes.json", json);
        }


    }
}
