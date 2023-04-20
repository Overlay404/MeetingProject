using MeetingProject.SupportiveClasses;
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
    /// Логика взаимодействия для SelectionOfProjectWindows.xaml
    /// </summary>
    public partial class SelectionOfProjectWindows : Window
    {


        public List<ProjectListParse> ProjectListParse
        {
            get { return (List<ProjectListParse>)GetValue(ProjectListParseProperty); }
            set { SetValue(ProjectListParseProperty, value); }
        }

        public static readonly DependencyProperty ProjectListParseProperty =
            DependencyProperty.Register("ProjectListParse", typeof(List<ProjectListParse>), typeof(SelectionOfProjectWindows));



        public SelectionOfProjectWindows()
        {
            InitializeComponent();
        }

        public async Task AnswerRequest(string NameUserGithub)
        {
            //Объект полученный из запроса
            var AccountGitHubObject = await RequestManager.Get<List<ProjectListParse>>($"users/{NameUserGithub}/repos") ??
                new List<ProjectListParse>(){ new ProjectListParse
                {
                    default_branch = "",
                    name = "",
                    visibility = "",
                    size = 0,
                    language = ""
                } };

        }
    }
}
