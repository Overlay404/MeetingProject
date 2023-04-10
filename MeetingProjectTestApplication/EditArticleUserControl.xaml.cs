using System;
using System.Linq;
using System.Text.RegularExpressions;
using Markdig;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для EditArticleUserControl.xaml
    /// </summary>
    public partial class EditArticleUserControl : System.Windows.Controls.UserControl
    {
        public static EditArticleUserControl Instance { get; private set; }

        public string MdText
        {
            get { return (string)GetValue(MdTextProperty); }
            set { SetValue(MdTextProperty, value); }
        }

        public static readonly DependencyProperty MdTextProperty =
            DependencyProperty.Register("MdText", typeof(string), typeof(EditArticleUserControl));

        public string InitialText = "";

        public Regex regexForImageProcessing = new Regex(@"%\d+image");
        public Regex regexNumberInImage = new Regex(@"\d+");

        public EditArticleUserControl()
        {
            MdText = @"
| Заголовок 1 | Заголовок 2 |
| ----------- | ----------- |
| Строка 1, ячейка 1 | Строка 1, ячейка 2 |
| Строка 2, ячейка 1 | Строка 2, ячейка 2 |
";
            InitializeComponent();
            Instance = this;


        }

        private void CheckingTextForImages()
        {
            int lineLengthOnThePlot = 0;

            InitializedMarkdownStartStart(MdText.Length);

            foreach (var item in MdText.Split(new string[] { "\n" }, StringSplitOptions.None))
            {
                lineLengthOnThePlot += item.Length;

                if (regexForImageProcessing.IsMatch(item))
                {
                    CreatingImage(regexForImageProcessing.Match(item).Value);
                }
            }

            if (TextGeneratingPreview.Children.Count == 0)
            {
                InitializedMarkdownStartStart(MdText.Length);
            }

            InitialText = MdText;
        }

        private void InitializedMarkdownStartStart(int characters)
        {
            var pipeline = new MarkdownPipelineBuilder()
                                    .UseAdvancedExtensions()
                                    .UsePipeTables()
                                    .UseSoftlineBreakAsHardlineBreak()
                                    .Build();

            var htmlTextInPreview = Markdown.ToHtml(regexForImageProcessing.Replace(MdText.Substring(0, characters), ""), pipeline);

            var htmlText = $@"
                <html>
                    <head>
                        <meta http-equiv='Content-Type' content='text/html;charset=utf-8'>
                        <style>
                            body {{
                                font-family: 'Hubot Sans', sans-serif;
                                font-size: 12px;
                            }}
                            code {{
                                display: block;
                                font-family: 'Source Code Pro', monospace;
                                width: auto;
                                background-color: black;
                                color: rgb(211, 211, 211);
                                font-size: 20px;
                                border: 1px solid grey;
                                padding: 5px 10px 5px 10px;
                                white-space: pre;
                                overflow: auto;
                            }}
                            table {{
                                width: 100%;
	                            margin-bottom: 20px;
	                            border: 1px solid #dddddd;
	                            border-collapse: collapse; 
                            }}
                            th {{
                                font - weight: bold;
	                            padding: 5px;
	                            background: #efefef;
	                            border: 1px solid #dddddd;
                            }}
                            td {{
                                border: 1px solid #dddddd;
	                            padding: 5px;
                            }}
                        </style>
                    </head>
                    <body>
                        {htmlTextInPreview}
                    </body>
                </html>";


            BrowserForPreviewMarkdown.NavigateToString(htmlText);
        }




        private void CreatingImage(string contentId)
        {
            var idPicture = int.Parse(regexNumberInImage.Match(contentId).Value);

            Image image = new Image()
            {
                Source = ImageConverter.ConvertToImageSource(App.db.PictureProject.Where(p => p.id == idPicture).FirstOrDefault().codeImage)
            };

            TextGeneratingPreview.Children.Add(image);
        }

        private void TextGenerating_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (!System.Windows.Clipboard.ContainsImage()) return;

                SavePictureInDataBase();
                GenerateIndividualNamePicture();
            }
        }

        private static void SavePictureInDataBase()
        {
            App.db.PictureProject.Add(new Model.PictureProject
            {
                codeImage = ImageConverter.ConvertToByteCollection(System.Windows.Clipboard.GetImage()),
                ProjectId = 1
            });
            App.db.SaveChanges();
        }

        private void GenerateIndividualNamePicture()
        {
            var numberImage = App.db.PictureProject.ToList().LastOrDefault().id;
            TextInScrollView.SelectedText = $"\n%{numberImage}image";
        }



        #region Обработчики
        private void PreviewTabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CheckingTextForImages();
        }

        private void EditTabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MdText = InitialText;
        }

        private void BrowserForPreviewMarkdown_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.Uri != null)
            {
                e.Cancel = true;
                System.Windows.Clipboard.SetText(e.Uri.ToString());
            }
        }
        #endregion

        //вход
        //wqawdas
        //asdasdasd
        //%1image.png ✅
        //asdasdasd
        //asd
        //as
        //d
        //as
        //dasdasdas
        //%2image.png ✅
        //sdasdasd
        //asdasdasd

        //выход
        //wqawdas
        //asdasdasd
        //Картинка 1
        //asdasdasd
        //asd
        //as
        //d
        //as
        //dasdasdas
        //        Картинка 2
        //sdasdasd
        //asdasdasd


        //1.Проходить по всем строкам.Находить с помощью regex значение. Создать тектовый блок, и картинку.
        //2.Замена подстрок, присвоение значнию %\d+image значение ®. Разделение строки на массив. Сохранение в массив значение заменённых строк. Генерация с помощью цикла.


        //Берем картинку из бд если в тексте символ % то берем значение после до пробела находим значение в базе создаем картинку в качестве дочернего элемента стак панели.
        //цель обработать текст на наличие % получить значение после, получить объект, создать image. Это все в при переходе в область отображения md
        //при cntl+v загружаем в бд, добавляем в текст ссылку.
        //при сборе данных из github перегенерация md github'а и скачивание картинок из папок проекта и добавление их в бд, в тексте присвоение ссылки.


        //private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    SymbolGenerate("# ", false);
        //}

        //private void SymbolGenerate(string symbol, bool isDoublegenerate)
        //{
        //    string textLine = EditTextBox.GetLineText(EditTextBox.GetLineIndexFromCharacterIndex(EditTextBox.SelectionStart));
        //    if (textLine.Contains(symbol) || textLine == "" || textLine == Environment.NewLine) return;

        //    string fullText = EditTextBox.Text;
        //    int cursorPosition = EditTextBox.SelectionStart;
        //    if (isDoublegenerate)
        //    {
        //        EditTextBox.Text = fullText.Replace(textLine, symbol + textLine + symbol);
        //        EditTextBox.SelectionStart = cursorPosition;
        //        return;
        //    }
        //    EditTextBox.Text = fullText.Replace(textLine, symbol + textLine);
        //    EditTextBox.SelectionStart = cursorPosition;
        //}

        //private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        //{
        //    SymbolGenerate("*", true);
        //}

        //private void Border_MouseDown_2(object sender, MouseButtonEventArgs e)
        //{
        //    SymbolGenerate("~", true);
        //}
    }
}
