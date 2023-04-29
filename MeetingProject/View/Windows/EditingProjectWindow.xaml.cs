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

        Project ProjectObject { get; set; }

        public static EditingProjectWindow Instance;

        public EditingProjectWindow(Project project = null)
        {
            VisibilityListBox = Visibility.Hidden;
            InitializeHintMarkdownSyntax();
            InitializeComponent();
            if (project == null)
            {
                ProjectObject = new Project()
                {
                    ManWithResumeId = App.user.id,
                    text = "",
                    date = DateTime.Today
                };
            }
            else
            {
                ProjectObject = project;
            }

            FramePageEditing.Navigate(new EditingProjectPage(ProjectObject));
            Instance = this;

            SettingBtn.MouseDown += (sender, e) => 
            {
                UpdateProjectObject();
                FramePageEditing.Navigate(new SettingProjectPage(ProjectObject)); 
            };
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
                EditingProjectPage.Instance.MarkdownText.SelectedText = (HintListBox.SelectedItem as HintMarkdownSyntax).Content;
            };
        }

        private void InitializeHintMarkdownSyntax()
        {
            Hint = new List<HintMarkdownSyntax>
            {
                new HintMarkdownSyntax
                {
                    Header = "Заголовок первого уровня",
                    Content = "# Заголовок первого уровня"
                },

                new HintMarkdownSyntax
                {
                    Header = "Заголовок второго уровня",
                    Content = "## Заголовок второго уровня"
                },

                new HintMarkdownSyntax
                {
                    Header = "Заголовок третьего уровня",
                    Content = "### Заголовок третьего уровня"
                },

                new HintMarkdownSyntax
                {
                    Header = "Заголовок четвертого уровня",
                    Content = "#### Заголовок четвертого уровня"
                },

                new HintMarkdownSyntax
                {
                    Header = "Заголовок пятого уровня",
                    Content = "##### Заголовок пятого уровня"
                },

                new HintMarkdownSyntax
                {
                    Header = "Заголовок шестого уровня",
                    Content = "###### Заголовок шестого уровня"
                },

                new HintMarkdownSyntax
                {
                    Header = "Жирный текст",
                    Content = "**Жирный текст**"
                },

                new HintMarkdownSyntax
                {
                    Header = "Курсивный текст",
                    Content = "*Курсивный текст*"
                },

                new HintMarkdownSyntax
                {
                    Header = "Цитата",
                    Content = @"
> Каждый в цирке думает, что знает в цирке,
> но не каждый, что в цирке знает, 
> что в цирке не каждый знает думает."
                },

                new HintMarkdownSyntax
                {
                    Header = "Список",
                    Content = @"
- Первый
- Второй
    - Второй.Первый
- Третий"
                },

                new HintMarkdownSyntax
                {
                    Header = "Картинка",
                    Content = "![Текст заменяющий картинку](https://sun9-14.userapi.com/impg/iVKXr8bNqzesqrS91HNOgTKnQkEPZuW2xLjHTg/IxJHuOhO4K4.jpg?size=716x541&quality=96&sign=de8f75d1b1043c2dadb9805f2da88a39&type=album)"
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

        private void UpdateProjectObject()
        {
            ProjectObject.text = EditingProjectPage.Instance.MdText;
        }

        private void ClosingWindow(bool IsClosing)
        {
            App.db.SaveChanges();
            CheckForAvailability();
            EditingProjectPage.Instance.AcceptChanges();
            PortfolioWindow.Instance.Visibility = Visibility.Visible;
            if(!IsClosing) Close();
            PortfolioWindow.Instance.FrameDisplayingContent.Navigate(new ProjectPage());
        }

        private void CheckForAvailability()
        {
            if (App.db.Project.ToList().Contains(ProjectObject)) return;

            if (string.IsNullOrEmpty(ProjectObject.title) || string.IsNullOrEmpty(ProjectObject.text))
                if(MessageBox.Show("Вы хотите создать пустой проект", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            App.db.Project.Add(ProjectObject);
            App.db.SaveChanges();
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
