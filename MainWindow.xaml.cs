using System.Windows;
using System.Windows.Controls;
using WordPad_Kasianova.ViewModel;

namespace WordPad_Kasianova
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new NoteUtilsViewModel(new Model.NoteUtils());
            NoteArea.Focus();
        }

        private void NoteTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NoteUtilsViewModel.FocusedElement = sender as RichTextBox;
        }

        private void Color_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DataContext is NoteUtilsViewModel noteUtilsVM && noteUtilsVM.SettingsCommand != null)
            {
                var selectedColor = noteUtilsVM.SelectedFontColor;
                if (selectedColor != null)
                    noteUtilsVM.SettingsCommand.Execute($"Color|{selectedColor}");
            }
        }
        private void FontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is NoteUtilsViewModel noteUtilsVM && noteUtilsVM.SettingsCommand != null)
            {
                var selectedFontSize = noteUtilsVM.SelectedFontSize;
                if (selectedFontSize != 0)
                    noteUtilsVM.SettingsCommand.Execute($"FontSize|{selectedFontSize}");
            }
        }
        private void FontStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is NoteUtilsViewModel noteUtilsVM && noteUtilsVM.SettingsCommand != null)
            {
                var selectedFontStyle = noteUtilsVM.SelectedFontStyle;
                if (selectedFontStyle != null)
                    noteUtilsVM.SettingsCommand.Execute($"Style|{selectedFontStyle}");
            }
        }
    }
}