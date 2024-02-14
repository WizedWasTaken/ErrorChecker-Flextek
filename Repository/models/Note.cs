using System;
using System.ComponentModel;

namespace Repository.models
{
    public class Note : ClassNotify
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier for each note

        private string? _content;
        public string? Content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    _content = value;
                    Notify();
                }
            }
        }
    }
}