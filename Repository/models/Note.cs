using System;
using System.ComponentModel;

namespace Repository.models
{
    public class Note : ClassNotify
    {
        public Guid Id { get; set; } = Guid.NewGuid();

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

        private bool _isFromErrorTextBox;

        public bool IsFromErrorTextBox
        {
            get { return _isFromErrorTextBox; }
            set
            {
                if (_isFromErrorTextBox != value)
                {
                    _isFromErrorTextBox = value;
                    Notify();
                }
            }
        }

        private string _copiedText;

        public string CopiedText
        {
            get
            {
                return _copiedText;
            }
            set
            {
                if (_copiedText != value)
                {
                    _copiedText = value;
                    Notify();
                }
            }
        }

        private int _startPosition;

        public int StartPosition
        {
            get
            {
                return _startPosition;
            }
            set
            {
                if (_startPosition != value)
                {
                    _startPosition = value;
                    Notify();
                }
            }
        }
    }
}
