using MeetingProject.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {


        public IEnumerable<Project> ProjectList
        {
            get { return (IEnumerable<Project>)GetValue(ProjectListProperty); }
            set { SetValue(ProjectListProperty, value); }
        }

        public static readonly DependencyProperty ProjectListProperty =
            DependencyProperty.Register("ProjectList", typeof(IEnumerable<Project>), typeof(ProjectPage));



        public ProjectPage()
        {
            ProjectList = App.db.Project.ToList();

            InitializeComponent();

            ListViewProject.SelectionChanged += delegate(object sender, SelectionChangedEventArgs e) 
            {
                var dataImage = SupportiveClasses.ImageConverter.OpenFileDialogSave();
                (ListViewProject.SelectedItem as Project).MainPicture = dataImage;

                App.db.SaveChanges();

                ProjectList = App.db.Project.ToList();
            };
        }
    }
}
