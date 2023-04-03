﻿using Avalonia.Media;
using FontAwesome.WPF;
using MeetingProjectTestApplication.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using Colors = System.Windows.Media.Colors;
using SolidColorBrush = System.Windows.Media.SolidColorBrush;
using TextBox = System.Windows.Controls.TextBox;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для EditArticleUserControl.xaml
    /// </summary>
    public partial class EditArticleUserControl : System.Windows.Controls.UserControl
    {
        public static EditArticleUserControl Instance { get; private set; }

        public static PictureProject PictureObject { get; set; }

        public string MdText
        {
            get { return (string)GetValue(MdTextProperty); }
            set { SetValue(MdTextProperty, value); }
        }

        public static readonly DependencyProperty MdTextProperty =
            DependencyProperty.Register("MdText", typeof(string), typeof(EditArticleUserControl));

        public int PositionCursorInText;
        public TextBox TextBoxObjectRefactoring;

        public EditArticleUserControl()
        {
            MdText = "# Dillinger\n##The Last Markdown Editor, Ever\n\n[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)\n\n![N|Solid](https://icdn.lenta.ru/images/2021/12/28/20/20211228202958360/wide_4_3_4a433584cb35ec4ce60e1316564b86d1.jpg)\n\nDillinger is a cloud-enabled, mobile-ready, offline-storage compatible,\nAngularJS-powered HTML5 Markdown editor.\n\n- Type some Markdown on the left\n- See HTML in the right\n- ✨Magic ✨\n\n## Features\n\n- Import a HTML file and watch it magically convert to Markdown\n- Drag and drop images (requires your Dropbox account be linked)\n- Import and save files from GitHub, Dropbox, Google Drive and One Drive\n- Drag and drop markdown and HTML files into Dillinger\n- Export documents as Markdown, HTML and PDF\n\nMarkdown is a lightweight markup language based on the formatting conventions\nthat people naturally use in email.\nAs [John Gruber] writes on the [Markdown site][df1]\n\n> The overriding design goal for Markdown's\n> formatting syntax is to make it as readable\n> as possible. The idea is that a\n> Markdown-formatted document should be\n> publishable as-is, as plain text, without\n> looking like it's been marked up with tags\n> or formatting instructions.\n\nThis text you see here is *actually- written in Markdown! To get a feel\nfor Markdown's syntax, type some text into the left window and\nwatch the results in the right.\n\n## Tech\n\nDillinger uses a number of open source projects to work properly:\n\n- [AngularJS] - HTML enhanced for web apps!\n- [Ace Editor] - awesome web-based text editor\n- [markdown-it] - Markdown parser done right. Fast and easy to extend.\n- [Twitter Bootstrap] - great UI boilerplate for modern web apps\n- [node.js] - evented I/O for the backend\n- [Express] - fast node.js network app framework [@tjholowaychuk]\n- [Gulp] - the streaming build system\n- [Breakdance](https://breakdance.github.io/breakdance/) - HTML\nto Markdown converter\n- [jQuery] - duh\n\nAnd of course Dillinger itself is open source with a [public repository][dill]\n on GitHub.\n\n## Installation\n\nDillinger requires [Node.js](https://nodejs.org/) v10+ to run.\n\nInstall the dependencies and devDependencies and start the server.\n\n```sh\ncd dillinger\nnpm i\nnode app\n```\n\nFor production environments...\n\n```sh\nnpm install --production\nNODE_ENV=production node app\n```\n\n## Plugins\n\nDillinger is currently extended with the following plugins.\nInstructions on how to use them in your own application are linked below.\n\n| Plugin | README |\n| ------ | ------ |\n| Dropbox | [plugins/dropbox/README.md][PlDb] |\n| GitHub | [plugins/github/README.md][PlGh] |\n| Google Drive | [plugins/googledrive/README.md][PlGd] |\n| OneDrive | [plugins/onedrive/README.md][PlOd] |\n| Medium | [plugins/medium/README.md][PlMe] |\n| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |\n\n## Development\n\nWant to contribute? Great!\n\nDillinger uses Gulp + Webpack for fast developing.\nMake a change in your file and instantaneously see your updates!\n\nOpen your favorite Terminal and run these commands.\n\nFirst Tab:\n\n```sh\nnode app\n```\n\nSecond Tab:\n\n```sh\ngulp watch\n```\n\n(optional) Third:\n\n```sh\nkarma test\n```\n\n#### Building for source";
            InitializeComponent();
            Instance = this;

            CreateTextBox(MdText);
        }

        #region Сохранение и удаление картинок из бд
        private async static void SavePictureInDataBase()
        {
            App.db.PictureProject.Add(PictureObject);
            await App.db.SaveChangesAsync();
        }

        private async static void RemovePictureInDataBase(PictureProject picture)
        {
            App.db.PictureProject.Remove(picture);
            await App.db.SaveChangesAsync();
        } 
        #endregion

        private void GenerateIndividualNamePicture()
        {
            InitializePictureObject();
            AddingTextBox();
        }

        private void InitializePictureObject()
        {
            PictureObject = new Model.PictureProject
            {
                codeImage = ImageConverter.ConvertToByteCollection(System.Windows.Clipboard.GetImage()),
                ProjectId = 1
            };

            EditingTextStackPanel.Children.Add(BorderCreating(PictureObject));
        }

        private void AddingTextBox()
        {
            var DivisibleText = TextBoxObjectRefactoring.Text.Trim();
            var DivisibleTextLenght = DivisibleText.Length;

            if (DivisibleText.Length == PositionCursorInText - 1)
            {
                CreateTextBox("\n");
                return;
            }
            else if (PositionCursorInText == 0)
            {
                EditingTextStackPanel.Children.Add(new TextBlock());
                return;
            }
            TextBoxObjectRefactoring.Text = DivisibleText.Substring(0, PositionCursorInText - 1);
            CreateTextBox(DivisibleText.Substring(PositionCursorInText, DivisibleTextLenght - PositionCursorInText));
        }

        private void CreateTextBox(string text)
        {
            TextBox textBox = new TextBox()
            {
                Text = text
            };

            textBox.KeyDown += (sender, e) =>
            {
                if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl) && e.Key == Key.V)
                {
                    if (!System.Windows.Clipboard.ContainsImage()) return;

                    PositionCursorInText = textBox.SelectionStart;
                    TextBoxObjectRefactoring = textBox;

                    GenerateIndividualNamePicture();
                    SavePictureInDataBase();
                }
            };

            EditingTextStackPanel.Children.Add(textBox);
        }

        #region Создание элемента картинки
        private Border BorderCreating(PictureProject pictureObject)
        {
            Border border = new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1.5),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(2),
                Margin = new Thickness(10, 0, 0, 0)
            };

            Grid grid = new Grid();

            TextBlock textBlock = new TextBlock()
            {
                Text = $"%{pictureObject.id + 1}image",
                Margin = new Thickness(0, 0, 10, 0)
            };

            FontAwesome.WPF.FontAwesome fontAwesome = new FontAwesome.WPF.FontAwesome()
            {
                Icon = FontAwesomeIcon.Trash,
                Width = 15,
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                DataContext = pictureObject
            };

            fontAwesome.MouseDown += (sender, e) =>
            {
                EditingTextStackPanel.Children.Remove(border);
                var FontAwesomeDataContext = ((sender as FontAwesome.WPF.FontAwesome).DataContext as PictureProject).id;
                RemovePictureInDataBase(App.db.PictureProject.Where(p => p.id.Equals(FontAwesomeDataContext)).FirstOrDefault());
            };

            grid.Children.Add(textBlock);
            grid.Children.Add(fontAwesome);
            border.Child = grid;

            return border;
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
