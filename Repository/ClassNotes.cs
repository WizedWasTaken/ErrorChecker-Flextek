using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Repository;
using Repository.models;

public class ClassNotes : ClassNotify
{
    private List<Note> _notes = new List<Note>();

    public List<Note> Notes
    {
        get => _notes;
        set
        {
            if (_notes != value)
            {
                _notes = value;
                Notify();
            }
        }
    }
}
