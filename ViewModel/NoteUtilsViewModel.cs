using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WordPad_Kasianova.Model;

namespace WordPad_Kasianova.ViewModel
{
    public class NoteUtilsViewModel : INotifyPropertyChanged
    {
        public static RichTextBox? FocusedElement { get; set; }
        public NoteUtils NoteUtils;
        private string JsonDirFullPath;
        private string Title;
        public NoteUtilsViewModel(NoteUtils noteUtils)
        {
            Title = "";
            NoteUtils = noteUtils;
            AllFontStyles = new ObservableCollection<string>(NoteUtils.MyFontStyles.Cast<string>());
            AllFontSizes = new ObservableCollection<int>(NoteUtils.FontSizes.Cast<int>());
            AllColors = new ObservableCollection<string>(NoteUtils.MyColors.Cast<string>());
            SelectedFontStyle = NoteUtils.FontStyle;
            SelectedFontSize = NoteUtils.FontSize;
            SelectedFontColor = NoteUtils.FontColor;
            SelectedBold = NoteUtils.IsBold;
            SelectedCursive = NoteUtils.IsCursive;
            SelectedUnderlined = NoteUtils.IsUnderlined;
            SelectedHighlighted = NoteUtils.IsUnderlined;
            SelectedMode = NoteUtils.IsLightMode;

            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = @"..\..\..\Assets\Json\";
            JsonDirFullPath = Path.GetFullPath(Path.Combine(projectRoot, relativePath));

        }
        #region ИЗМЕНЕНИЕ ЦВЕТА КНОПКИ
        public string BoldButtonColor { get; set; } = "AliceBlue";
        public string CursiveButtonColor { get; set; } = "AliceBlue";
        public string UnderlineButtonColor { get; set; } = "AliceBlue";
        public string HighlightButtonColor { get; set; } = "Transparent";
        public string ModeButtonChar { get; set; } = char.ConvertFromUtf32(0x2600);
        #endregion

        #region ДАННЫЕ ДЛЯ ПРЕДСТАВЛЕНИЯ
        public ObservableCollection<string> AllFontStyles { get; }
        public ObservableCollection<int> AllFontSizes { get; }

        public ObservableCollection<string> AllColors { get; }

        private string fontStyle;
        private int fontSize;
        private string fontColor;
        private bool bold;
        private bool cursive;
        private bool underlined;
        private bool highlighted;
        private bool mode;
        public string SelectedFontStyle
        {
            get => fontStyle;
            set
            {
                fontStyle = value;
                NoteUtils.FontStyle = value;
                OnPropertyChanged(nameof(SelectedFontStyle));
            }
        }

        public int SelectedFontSize
        {
            get => fontSize;
            set
            {
                fontSize = value;
                NoteUtils.FontSize = value;
                OnPropertyChanged(nameof(SelectedFontSize));
            }
        }
        public string SelectedFontColor
        {
            get => fontColor;
            set
            {
                fontColor = value;
                NoteUtils.FontColor = value;
                OnPropertyChanged(nameof(SelectedFontColor));
            }
        }

        public bool SelectedBold
        {
            get => bold;
            set
            {
                bold = value;
                NoteUtils.IsBold = value;
                BoldButtonColor = bold ? "DeepSkyBlue" : "AliceBlue";
                OnPropertyChanged(nameof(SelectedBold));
                OnPropertyChanged(nameof(BoldButtonColor));
            }
        }
        public bool SelectedCursive
        {
            get => cursive;
            set
            {
                cursive = value;
                NoteUtils.IsCursive = value;
                CursiveButtonColor = cursive ? "DeepSkyBlue" : "AliceBlue";
                OnPropertyChanged(nameof(SelectedCursive));
                OnPropertyChanged(nameof(CursiveButtonColor));
            }
        }

        public bool SelectedUnderlined
        {
            get => underlined;
            set
            {
                underlined = value;
                NoteUtils.IsUnderlined = value;
                UnderlineButtonColor = underlined ? "DeepSkyBlue" : "AliceBlue";
                OnPropertyChanged(nameof(SelectedUnderlined));
                OnPropertyChanged(nameof(UnderlineButtonColor));
            }
        }
        public bool SelectedHighlighted
        {
            get => highlighted;
            set
            {
                highlighted = value;
                NoteUtils.IsHighlight = value;
                HighlightButtonColor = highlighted ? "DeepSkyBlue" : "AliceBlue";
                OnPropertyChanged(nameof(SelectedHighlighted));
                OnPropertyChanged(nameof(HighlightButtonColor));
            }
        }

        public bool SelectedMode
        {
            get => mode;
            set
            {
                mode = value;
                NoteUtils.IsLightMode = value;
                ModeButtonChar = NoteUtils.IsLightMode ? char.ConvertFromUtf32(0x2600) : char.ConvertFromUtf32(0x1F319);
                OnPropertyChanged(nameof(ModeButtonChar));
                OnPropertyChanged(nameof(SelectedMode));
            }
        }

        #endregion

        #region КОМАНДЫ
        private BaseCommand settingsCommand;
        private BaseCommand loadImageCommand;
        private BaseCommand saveStyleCommand;
        private BaseCommand loadStyleCommand;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
       => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public BaseCommand SettingsCommand
        {
            get
            {
                if (settingsCommand != null)
                    return settingsCommand;
                else
                {
                    Action<object> Execute = o =>
                    {
                        if (FocusedElement is RichTextBox NoteArea)
                        {
                            NoteArea.Focus();
                            if (o is string param)
                            {
                                var parts = param.Split('|');
                                var commandType = parts[0];
                                string? value = parts.Length > 1 ? parts[1] : null;
                                switch (commandType)
                                {
                                    case "Bold":
                                        SelectedBold = !SelectedBold;
                                        break;
                                    case "Cursive":
                                        SelectedCursive = !SelectedCursive;
                                        break;
                                    case "Color":
                                        if (value != null)
                                        {
                                            try
                                            {
                                                SelectedFontColor = value;
                                            }
                                            catch
                                            {
                                                SelectedFontColor = "Black";
                                            }
                                        }
                                        break;
                                    case "FontSize":
                                        if (value != null)
                                        {
                                            try
                                            {
                                                SelectedFontSize = int.Parse(value);
                                            }
                                            catch
                                            {
                                                SelectedFontSize = 18;
                                            }
                                        }
                                        break;
                                    case "Underline":
                                        SelectedUnderlined = !SelectedUnderlined;
                                        break;
                                    case "Highlight":
                                        SelectedHighlighted = !SelectedHighlighted;
                                        break;
                                    case "Mode":
                                        SelectedMode = !SelectedMode;
                                        break;
                                    case "Style":
                                        if (value != null)
                                        {
                                            try
                                            {

                                                SelectedFontStyle = value;

                                            }
                                            catch
                                            {
                                                SelectedFontStyle = "Courier New";
                                            }
                                        }
                                        break;
                                }
                                NoteArea.Focus();

                                var style = NoteUtils.IsCursive ? FontStyles.Italic : FontStyles.Normal;
                                var weight = NoteUtils.IsBold ? FontWeights.Bold : FontWeights.Normal;
                                var brushConverter = new BrushConverter();
                                var color = (SolidColorBrush)brushConverter.ConvertFromString(NoteUtils.FontColor);
                                var size = (double)NoteUtils.FontSize;
                                var underline = NoteUtils.IsUnderlined ? TextDecorations.Underline : null;
                                var highlight = NoteUtils.IsHighlight ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.Transparent);
                                var modeBack = NoteUtils.IsLightMode ? new SolidColorBrush(Colors.GhostWhite) : new SolidColorBrush(Colors.DarkBlue);
                                var modeBackNoteArea = NoteUtils.IsLightMode ? new SolidColorBrush(Colors.GhostWhite) : new SolidColorBrush(Colors.DarkBlue);
                                var modeBackTextGrid = NoteUtils.IsLightMode ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.DarkSlateBlue);
                                var modeBackSidePanel = NoteUtils.IsLightMode ? new SolidColorBrush(Colors.CornflowerBlue) : new SolidColorBrush(Colors.Indigo);
                                if (color.Color == Colors.White && NoteUtils.IsLightMode)
                                {
                                    color = new SolidColorBrush(Colors.Black);
                                    SelectedFontColor = "Black";
                                }
                                else if (color.Color == Colors.Black && !NoteUtils.IsLightMode)
                                {
                                    color = new SolidColorBrush(Colors.White);
                                    SelectedFontColor = "White";
                                }
                                var fontFamily = new FontFamily(SelectedFontStyle);

                                var selection = NoteArea.Selection;
                                var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                                var noteGrid = window.FindName("NoteArea") as RichTextBox;
                                if (noteGrid != null)
                                    noteGrid.Background = modeBackNoteArea;
                                var textGrid = window.FindName("TextAreaGrid") as Grid;
                                if (textGrid != null)
                                    textGrid.Background = modeBackTextGrid;
                                var sidePanel = window.FindName("SidePanel") as Grid;
                                if (sidePanel != null)
                                    sidePanel.Background = modeBackSidePanel;

                                if (!selection.IsEmpty)
                                {
                                    switch (commandType)
                                    {
                                        case "Cursive":
                                            selection.ApplyPropertyValue(TextElement.FontStyleProperty, style);
                                            break;
                                        case "Bold":
                                            selection.ApplyPropertyValue(TextElement.FontWeightProperty, weight);
                                            break;
                                        case "Color":
                                            selection.ApplyPropertyValue(TextElement.ForegroundProperty, color);
                                            break;
                                        case "FontSize":
                                            selection.ApplyPropertyValue(TextElement.FontSizeProperty, (double)size);
                                            break;
                                        case "Underline":
                                            selection.ApplyPropertyValue(Inline.TextDecorationsProperty, underline);
                                            break;
                                        case "Highlight":
                                            selection.ApplyPropertyValue(TextElement.BackgroundProperty, highlight);
                                            break;
                                    }
                                }
                                else
                                {
                                    NoteArea.FontFamily = fontFamily;
                                    var caret = NoteArea.CaretPosition;
                                    if (caret.Paragraph != null)
                                    {
                                        Run run = new Run()
                                        {
                                            FontStyle = style,
                                            FontWeight = weight,
                                            Foreground = color,
                                            FontSize = size,
                                            TextDecorations = underline,
                                            Background = highlight
                                        };

                                        caret.Paragraph.Inlines.Add(run);
                                        NoteArea.CaretPosition = run.ContentEnd;
                                    }
                                    NoteArea.Focus();
                                }
                            }
                        }
                    };
                    settingsCommand = new BaseCommand(Execute);
                    return settingsCommand;
                }
            }
        }

        public BaseCommand LoadImageCommand
        {
            get
            {
                if (loadImageCommand == null)
                {
                    Action<object> Execute = o =>
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog
                        {
                            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                            Title = "Выберите изображение"
                        };
                        if (openFileDialog.ShowDialog() == true && FocusedElement is RichTextBox NoteArea)
                        {
                            string imagePath = openFileDialog.FileName;
                            string fileName = Path.GetFileName(imagePath);
                            string imagesDir = Path.Combine(JsonDirFullPath, "Images");

                            Directory.CreateDirectory(imagesDir); // если папки нет — создаём

                            string destPath = Path.Combine(imagesDir, fileName);
                            File.Copy(imagePath, destPath, true); // копируем картинку


                            var bitmap = new BitmapImage(new Uri(imagePath));
                            var image = new Image
                            {
                                Source = bitmap,
                                MaxWidth = 200,
                                Stretch = Stretch.Uniform,
                                Margin = new Thickness(20, 20, 20, 20)

                            };

                            InlineUIContainer container = new InlineUIContainer(image, NoteArea.CaretPosition);
                            NoteArea.Focus();
                        }
                    };
                    loadImageCommand = new BaseCommand(Execute);
                }
                return loadImageCommand;
            }
        }

        public void SaveNoteToJson(FlowDocument document, string filePath)
        {
            var note = new Note();
            note.Title = Title;

            foreach (var block in document.Blocks.OfType<System.Windows.Documents.Paragraph>())
            {
                var paragraphData = new ParagraphData();

                foreach (Inline inline in block.Inlines)
                {
                    if (inline is Run run)
                    {
                        var style = new NoteUtils
                        {
                            FontStyle = inline.FontFamily?.Source ?? NoteUtils.MyFontStyles[0],
                            FontSize = (int)inline.FontSize,
                            FontColor = (inline.Foreground as SolidColorBrush)?.Color.ToString() ?? "Black",
                            IsBold = inline.FontWeight == FontWeights.Bold,
                            IsCursive = inline.FontStyle == FontStyles.Italic,
                            IsUnderlined = inline.TextDecorations != null &&
                   inline.TextDecorations.Contains(TextDecorations.Underline[0]),
                            IsHighlight = (inline.Background as SolidColorBrush)?.Color != null &&
                  (inline.Background as SolidColorBrush)?.Color != Colors.Transparent,
                            IsLightMode = true
                        };

                        paragraphData.Paragraphs.Add(new NoteParagraph
                        {
                            Text = run.Text,
                            Style = style
                        });
                    }
                    else if (inline is InlineUIContainer container && container.Child is Image image)
                    {
                        if (image.Source is BitmapImage bitmapImage)
                        {
                            string fileName = System.IO.Path.GetFileName(bitmapImage.UriSource?.LocalPath);


                            paragraphData.Paragraphs.Add(new NoteParagraph
                            {
                                Text = $"[image:{fileName}]",
                                Style = new NoteUtils { IsLightMode = true }
                            });
                        }
                    }
                }


                note.Text.Add(paragraphData);
                note.NoteStyles = NoteUtils;
                
            }

            var json = JsonSerializer.Serialize(note, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public FlowDocument LoadNoteFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var note = JsonSerializer.Deserialize<Note>(json);
            Title = note.Title;
            NoteUtils = note.NoteStyles;

            FlowDocument document = new FlowDocument();

            foreach (var paragraphData in note.Text)
            {
                Paragraph paragraph = new Paragraph();

                foreach (var runData in paragraphData.Paragraphs)
                {
                    if (runData.Text.StartsWith("[image:") && runData.Text.EndsWith("]"))
                    {
                        string fileName = runData.Text.Substring(7, runData.Text.Length - 8);
                        string imagePath = JsonDirFullPath + "/Images/" + fileName;

                        if (File.Exists(imagePath))
                        {
                            BitmapImage bitmap = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                            Image image = new Image
                            {
                                Source = bitmap,
                                MaxWidth = 200,
                                Stretch = Stretch.Uniform,
                                Margin = new Thickness(20)
                            };

                            paragraph.Inlines.Add(new InlineUIContainer(image));
                        }
                    }
                    else
                    {
                        var s = runData.Style;

                        var run = new Run(runData.Text)
                        {
                            FontFamily = new FontFamily(s.FontStyle),
                            FontSize = s.FontSize,
                            FontWeight = s.IsBold ? FontWeights.Bold : FontWeights.Normal,
                            FontStyle = s.IsCursive ? FontStyles.Italic : FontStyles.Normal,
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(s.FontColor)),
                            Background = s.IsHighlight ? Brushes.Yellow : Brushes.Transparent
                        };

                        if (s.IsUnderlined)
                            run.TextDecorations = TextDecorations.Underline;

                        paragraph.Inlines.Add(run);
                    }
                }

                document.Blocks.Add(paragraph);
            }

            return document;
        }

        private BaseCommand loadNoteCommand;
        public BaseCommand LoadNoteCommand
        {
            get
            {
                if (loadNoteCommand == null)
                {
                    Action<object> Execute = o =>
                    {
                        if (Directory.Exists(JsonDirFullPath))
                        {
                            var selectWindow = new LoadWindow(GetAllNotes(JsonDirFullPath));
                            if (selectWindow.ShowDialog() == true)
                            {
                                string selectedFile = selectWindow.SelectedFileName;


                                if (FocusedElement is RichTextBox NoteArea)
                                {
                                    var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

                                    string filePath = JsonDirFullPath + selectedFile + ".json";
                                    NoteArea.Document = LoadNoteFromJson(filePath);
                                    window.Title = Title;
                                    window.DataContext = new NoteUtilsViewModel(NoteUtils);
                                }
                            }
                        }
                    };

                    loadNoteCommand = new BaseCommand(Execute);
                }

                return loadNoteCommand;
            }
        }

        private BaseCommand saveNoteCommand;
        public BaseCommand SaveNoteCommand
        {
            get
            {
                if (saveNoteCommand == null)
                {
                    Action<object> Execute = o =>
                    {
                        string title = Title ?? "";
                        if (title == "")
                        {
                            var dialog = new SaveWindow();
                        if (dialog.ShowDialog() == true)
                        {
                            title = dialog.NoteTitle ?? "Note";
                                if (string.IsNullOrWhiteSpace(title))
                                {
                                    title = "Note";
                                }
                             Title = title;
                        }
                        }
                        if (title != "" && FocusedElement is RichTextBox NoteArea)
                        {
                            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                            window.Title = title;
                            string filePath = JsonDirFullPath + title + ".json";
                            SaveNoteToJson(NoteArea.Document, filePath);
                        }
                    };

                    saveNoteCommand = new BaseCommand(Execute);
                }

                return saveNoteCommand;
            }
        }

        public BaseCommand SaveStyleCommand
        {
            get
            {
                if (saveStyleCommand == null)
                {
                    Action<object> Execute = o =>
                    {
                        var dialog = new SaveWindow();
                        if (dialog.ShowDialog() == true)
                        {
                            string styleName = dialog.NoteTitle ?? "Style";
                            if (string.IsNullOrWhiteSpace(styleName)) styleName = "Style";

                            string dir = JsonDirFullPath+"/Styles/";

                            string json = JsonSerializer.Serialize(NoteUtils, new JsonSerializerOptions { WriteIndented = true });
                            File.WriteAllText(Path.Combine(dir, styleName + ".json"), json);
                        }
                    };
                    saveStyleCommand = new BaseCommand(Execute);
                }
                return saveStyleCommand;
            }
        }

        public BaseCommand LoadStyleCommand
        {
            get
            {
                if (loadStyleCommand == null)
                {
                    Action<object> Execute = o =>
                    {
                        if (Directory.Exists(JsonDirFullPath))
                        {
                            string dir = JsonDirFullPath + "/Styles/";
                            var selectWindow = new LoadWindow(GetAllNotes(dir));
                            if (selectWindow.ShowDialog() == true)
                            {
                                string selectedFile = selectWindow.SelectedFileName;
                                
                                if (!string.IsNullOrWhiteSpace(selectedFile))
                                {
                                    string path = Path.Combine(dir, selectedFile + ".json");
                                    if (File.Exists(path))
                                    {
                                        string json = File.ReadAllText(path);
                                        var style = JsonSerializer.Deserialize<NoteUtils>(json);

                                        if (style != null)
                                        {
                                            NoteUtils = style;
                                            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                                            window.DataContext = new NoteUtilsViewModel(NoteUtils);
                                            ApplyNoteUtilsToRichTextBox(style);
                                        }
                                    }
                                }
                            }
                        }
                    };
                        loadStyleCommand = new BaseCommand(Execute);
                }
                return loadStyleCommand;
            }
        }

        
        #endregion

        private List<string> GetAllNotes(string dir)
        {
            var files = Directory.GetFiles(dir, "*.json");
            return files.Select(f => Path.GetFileNameWithoutExtension(f)).ToList();
        }
        private void ApplyNoteUtilsToRichTextBox(NoteUtils style)
        {
            if (FocusedElement is RichTextBox rtb)
            {
                foreach (var block in rtb.Document.Blocks)
                {
                    if (block is Paragraph p)
                    {
                        foreach (var inline in p.Inlines)
                        {
                            inline.FontFamily = new FontFamily(style.FontStyle);
                            inline.FontSize = style.FontSize;
                            inline.FontWeight = style.IsBold ? FontWeights.Bold : FontWeights.Normal;
                            inline.FontStyle = style.IsCursive ? FontStyles.Italic : FontStyles.Normal;
                            inline.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(style.FontColor));
                            inline.TextDecorations = style.IsUnderlined ? TextDecorations.Underline : null;
                            inline.Background = style.IsHighlight ? Brushes.Yellow : Brushes.Transparent;
                        }
                    }
                }
            }
        }
    }
}

