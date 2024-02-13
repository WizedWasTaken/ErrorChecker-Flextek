using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassText : ClassNotify
    {
        private string _text;

        public ClassText()
        {
            Text = "";
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                Notify();
            }
        }
    }
}
