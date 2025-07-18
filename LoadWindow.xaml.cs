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
    /// Interaction logic for LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window
    {
        public string SelectedFileName { get;set; }
        public LoadWindow(List<string> fileNames)
        {
            InitializeComponent();
            FileListBox.ItemsSource = fileNames;
        }

        private void OnLoadClick(object sender, RoutedEventArgs e)
        {
            SelectedFileName = FileListBox.SelectedItem as string;
            if (!string.IsNullOrWhiteSpace(SelectedFileName))
            {
                DialogResult = true;
            }
            else
            {
                //*********************************
            }
        }
    }
}
