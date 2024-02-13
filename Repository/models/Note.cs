using System.ComponentModel;

namespace Repository.models
{
    public class Note : ClassNotify
    {
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