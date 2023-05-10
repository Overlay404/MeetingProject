using ForCompanyMeetingProject.Model;
using ForCompanyMeetingProject.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ForCompanyMeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public ObservableCollection<Project> ProjectList
        {
            get { return (ObservableCollection<Project>)GetValue(ProjectListProperty); }
            set { SetValue(ProjectListProperty, value); }
        }

        public static readonly DependencyProperty ProjectListProperty =
            DependencyProperty.Register("ProjectList", typeof(ObservableCollection<Project>), typeof(ProjectPage));

        public ProjectPage(ManWithResume man)
        {
            RefreshListProject(man);

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProjectList);
            view.SortDescriptions.Add(new SortDescription("date", ListSortDirection.Descending));


            InitializeComponent();

            ListViewProject.MouseDoubleClick += (sender, e) =>
            {
                new ShowingProjectWindow(ListViewProject.SelectedItem as Project).Show();
            };
        }

        private void RefreshListProject(ManWithResume man)
        {
            ProjectList = new ObservableCollection<Project>(App.db.Project.Local.Where(p => p.ManWithResumeId == man.id));
        }
    }
}
