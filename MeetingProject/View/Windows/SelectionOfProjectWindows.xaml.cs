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




        public SelectionOfProjectWindows()
        {
            AsyncInitializeComponent();
        }

        private async void AsyncInitializeComponent()
        {
            ProjectList = await RequestApiGithub(App.user.github);
            RefreshCountProjectIsSelected();
            InitializeComponent();

            AcceptChoiceProjectsBtn.MouseDown += (sender, e) => { AcceptChoiceProjects(); };
        }
        
        readonly Func<string, Task<List<ProjectListParse>>> RequestApiGithub = async (NameUserGithub) =>
        {
            List<ProjectListParse> ProjectListObjectParsed = await RequestManager.Get<List<ProjectListParse>>($"https://api.github.com/users/{NameUserGithub}/repos");

            return ProjectListObjectParsed;
        };

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RefreshCountProjectIsSelected();
        }

        private void RefreshCountProjectIsSelected()
        {
            int count = ProjectList.Where(p => p.IsChecked).Count();
            if(count == 0) { CountProject = $"Не выбрано ни одного проекта"; }
            else { CountProject = $"Подтвердить выбор {count} проектов"; };
        }

        private void AcceptChoiceProjects()
        {
            if (MessageBox.Show($"Будет добавлено {ProjectList.Where(p => p.IsChecked).Count()} проектов. Подтвердить?", "Проекты Guthub", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            var ProjectListCopy = ProjectList;

            Dispatcher.InvokeAsync(() => ProjectListCopy.ForEach(async (item) =>
            {
                if (item.IsChecked)
                {
                    var textInProject = new WebClient().DownloadString($"https://raw.githubusercontent.com/{App.user.github}/{item.name}/{item.default_branch}/README.md");

                    App.db.Project.Add(new Model.Project
                    {
                        date = DateTime.Today,
                        text = textInProject,
                        title = item.name
                    });
                }
                }));

            Close();
        }
    }
}
