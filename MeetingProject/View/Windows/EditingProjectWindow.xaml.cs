using MeetingProject.Model;
using MeetingProject.View.Pages;
using static MeetingProject.SupportiveClasses.LocationWindows;
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

namespace MeetingProject.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditingProjectWindow.xaml
    /// </summary>
    public partial class EditingProjectWindow : Window
    {
        public List<HintMarkdownSyntax> Hint
        {
            get { return (List<HintMarkdownSyntax>)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(List<HintMarkdownSyntax>), typeof(EditingProjectWindow));


        public Visibility VisibilityListBox
        {
            get { return (Visibility)GetValue(VisibilityListBoxProperty); }
            set { SetValue(VisibilityListBoxProperty, value); }
        }

        public static readonly DependencyProperty VisibilityListBoxProperty =
            DependencyProperty.Register("VisibilityListBox", typeof(Visibility), typeof(EditingProjectWindow));



        public static EditingProjectWindow Instance;

        public EditingProjectWindow(Project project = null)
        {
            VisibilityListBox = Visibility.Hidden;
            InitializeHintMarkdownSyntax();
            InitializeComponent();
            FramePageEditing.Navigate(new EditingProjectPage(project));
            Instance = this;


            SettingBtn.MouseDown += (sender, e) => { FramePageEditing.Navigate(new SettingProjectPage(project)); };
            ExitBtn.MouseDown += (sender, e) =>
            {
                ClosingWindow(false);
            };
            HelpBtn.MouseDown += (sender, e) => 
            {
                if(VisibilityListBox == Visibility.Hidden)
                {
                    VisibilityListBox = Visibility.Visible;
                }
                else
                {
                    VisibilityListBox = Visibility.Hidden;
                }
            };
            HintListBox.SelectionChanged += (sender, e) =>
            {
                EditingProjectPage.
            };
        }

        private void InitializeHintMarkdownSyntax()
        {
            Hint = new List<HintMarkdownSyntax>
            {
                new HintMarkdownSyntax
                {
                    Header = "Заголовок 1",
                    Content = "# Заголовок"
                },

                new HintMarkdownSyntax
                {
                    Header = "Заголовок 2",
                    Content = "## Заголовок"
                },

                new HintMarkdownSyntax
                {
                    Header = "Заголовок 3",
                    Content = "### Заголовок"
                },

                new HintMarkdownSyntax
                {
                    Header = "Заголовок 4",
                    Content = "#### Заголовок"
                },

                new HintMarkdownSyntax
                {
                    Header = "Таблица",
                    Content = @"
| Заголовок 1 | Заголовок 2 |
| ----------- | ----------- |
| Строка 1, ячейка 1 | Строка 1, ячейка 2 |
| Строка 2, ячейка 1 | Строка 2, ячейка 2 |"
                }
            };
        }

        private void ClosingWindow(bool IsClosing)
        {

            EditingProjectPage.Instance.AcceptChanges();
            App.db.SaveChanges();
            PortfolioWindow.Instance.Visibility = Visibility.Visible;
            if(!IsClosing) Close();
            PortfolioWindow.Instance.FrameDisplayingContent.Navigate(new ProjectPage());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClosingWindow(true);
        }
    }

    public class HintMarkdownSyntax
    {
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
