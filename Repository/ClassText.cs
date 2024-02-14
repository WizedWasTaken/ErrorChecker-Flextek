using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Repository
{
    public class ClassText : ClassNotify
    {
        private string? _text;

        public ClassText()
        {
            Text = "";
        }

        public string Text
        {
            get
            {
                return _text ?? "";
            }

            set
            {
                _text = value;
                Notify();
                UpdateErrorDocument(value);
            }
        }

        private FlowDocument _errorDocument = new FlowDocument();

        public FlowDocument ErrorDocument
        {
            get => _errorDocument;
            set
            {
                _errorDocument = value;
                Notify();
            }
        }

        private void UpdateErrorDocument(string newText)
        {
            ErrorDocument.Blocks.Clear();
            ErrorDocument.Blocks.Add(new Paragraph(new Run(newText)));
        }
    }
}
