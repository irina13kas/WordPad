using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WordPad_Kasianova.Model
{
    public class NoteUtils 
    {
        private int fontSize;
        private string fontStyle;

        public static int[] FontSizes = new int[] {12, 14, 18, 20, 24, 28, 32 };

        public static List<string> MyColors = new List<string>()
        {
            "Black", "Red", "White", "Blue", "Green", "Purple"
        };
        public static List<string> MyFontStyles = new List<string>()
        {
            "Times New Roman",
            "Arial",
            "Courier New",
            "Lucida Fax"
        };

        public NoteUtils() 
        {
            FontColor = MyColors[0];
            FontSize = FontSizes[2];
            IsBold = false;
            IsCursive = false;
            IsUnderlined = false;
            IsHighlight = false;
            IsLightMode = true;
            FontStyle = MyFontStyles[2];
        }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public bool IsBold { get; set; }
        public bool IsCursive { get; set; }
        public bool IsUnderlined{ get; set; }
        public bool IsHighlight { get; set; }
        public bool IsLightMode { get; set; }
        public string FontStyle { get; set; }

    }
}
