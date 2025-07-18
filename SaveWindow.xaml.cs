using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WordPad_Kasianova
{
    /// <summary>
    /// Interaction logic for SaveWindow.xaml
    /// </summary>
    
    public partial class SaveWindow : Window
    {
        public string NoteTitle { get; set; }
        public SaveWindow()
        {
            InitializeComponent();
            Title.Focus();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            NoteTitle = Title.Text;
            DialogResult = true;
            Close();
        }
    }
}
