using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPad_Kasianova.Model
{
    public class Note
    {
        public string Title { get; set; }
        public List<ParagraphData> Text { get; set; } = new();
        public NoteUtils NoteStyles{get;set;}
    }
}
