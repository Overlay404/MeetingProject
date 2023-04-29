using MeetingProject.Model;
using MeetingProject.SupportiveClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для SelectionOfProjectWindows.xaml
    /// </summary>
    public partial class SelectionOfProjectWindows : Window
    {


        public List<ProjectListParse> ProjectList
        {
            get { return (List<ProjectListParse>)GetValue(ProjectListProperty); }
            set { SetValue(ProjectListProperty, value); }
        }

        public static readonly DependencyProperty ProjectListProperty =
            DependencyProperty.Register("ProjectList", typeof(List<ProjectListParse>), typeof(SelectionOfProjectWindows));




        public string CountProject
        {
            get { return (string)GetValue(CountProjectProperty); }
            set { SetValue(CountProjectProperty, value); }
        }

        public static readonly DependencyProperty CountProjectProperty =
            DependencyProperty.Register("CountProject", typeof(string), typeof(SelectionOfProjectWindows));




        public Visibility IsLoadedCompanentVisible
        {
            get { return (Visibility)GetValue(IsLoadedCompanentVisibleProperty); }
            set { SetValue(IsLoadedCompanentVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsLoadedCompanentVisibleProperty =
            DependencyProperty.Register("IsLoadedCompanentVisible", typeof(Visibility), typeof(SelectionOfProjectWindows));




        public Visibility IsLoadingCompanentVisible
        {
            get { return (Visibility)GetValue(IsLoadingCompanentVisibleProperty); }
            set { SetValue(IsLoadingCompanentVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingCompanentVisibleProperty =
            DependencyProperty.Register("IsLoadingCompanentVisible", typeof(Visibility), typeof(SelectionOfProjectWindows));




        public SelectionOfProjectWindows()
        {
            InitializeComponent();
            StartInitializeComponent();
            AsyncInitializeComponent();
        }

        private void StartInitializeComponent()
        {
            IsLoadingCompanentVisible = Visibility.Collapsed;
            IsLoadedCompanentVisible = Visibility.Visible;
        }

        private async void AsyncInitializeComponent()
        {
            ProjectList = await RequestApiGithub(App.user.github);
            RefreshCountProjectIsSelected();
            IsLoadingCompanentVisible = Visibility.Visible;
            IsLoadedCompanentVisible = Visibility.Collapsed;
            InitializeComponent();

            AcceptChoiceProjectsBtn.MouseDown += (sender, e) => { AcceptChoiceProjects(); };
        }

        readonly Func<string, Task<List<ProjectListParse>>> RequestApiGithub = async (NameUserGithub) =>
        {
            List<ProjectListParse> ProjectListObjectParsed = await RequestManager.Get<List<ProjectListParse>>($"https://api.github.com/users/{NameUserGithub}/repos?per_page=1000");

            return ProjectListObjectParsed;
        };

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RefreshCountProjectIsSelected();
        }

        private void RefreshCountProjectIsSelected()
        {
            if(ProjectList == null) return;
            int count = ProjectList.Where(p => p.IsChecked).Count();
            if (count == 0) { CountProject = $"Не выбрано ни одного проекта"; }
            else { CountProject = $"Подтвердить выбор {count} проектов"; };
        }

        private void AcceptChoiceProjects()
        {
            if (MessageBox.Show($"Будет добавлено {ProjectList.Where(p => p.IsChecked).Count()} проектов. Подтвердить?", "Проекты Guthub", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            var ProjectListCopy = ProjectList;

            List<string> paramMessage = new List<string>();

            ProjectListCopy.ForEach((item) =>
            {
                if (item.IsChecked)
                {
                    var textInProject = "";

                    try
                    {
                        textInProject = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString($"https://raw.githubusercontent.com/{App.user.github}/{item.name}/{item.default_branch}/README.md");
                    }
                    catch
                    {
                        paramMessage.Add($"{item.name}");
                        return;
                    }

                    App.db.Project.Add(new Model.Project
                    {
                        ManWithResumeId = App.user.id,
                        date = DateTime.Today,
                        text = textInProject,
                        title = item.name
                    });
                }
            });

            if (paramMessage.Count == 0)
            {
                MessageBox.Show("Все проекты успешно добавлены");
            }
            else
            {
                MessageBox.Show($"В проектах {paramMessage.Aggregate((x,y)=> x + ", " + y)} нет README.md файла, остальные проекты успешно добавлены");
            }
            Close();
        }
    }
}
