using MeetingProject.Model;
using MeetingProject.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
        public ObservableCollection<Project> ProjectList
        {
            get { return (ObservableCollection<Project>)GetValue(ProjectListProperty); }
            set { SetValue(ProjectListProperty, value); }
        }

        public static readonly DependencyProperty ProjectListProperty =
            DependencyProperty.Register("ProjectList", typeof(ObservableCollection<Project>), typeof(ProjectPage));

        public ProjectPage()
        {
            RefreshListProject();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProjectList);
            view.SortDescriptions.Add(new SortDescription("date", ListSortDirection.Descending));


            InitializeComponent();

            AddingBtn.MouseDown += (sender, e) => { AddingProject(); };
            DeletingBtn.MouseDown += (sender, e) => { DeletingProject(); };

            ListViewProject.MouseDoubleClick += (sender, e) =>
            {
                new EditingProjectWindow(ListViewProject.SelectedItem as Project).Show();
                PortfolioWindow.Instance.Visibility = Visibility.Hidden;
            };
        }

        private void RefreshListProject()
        {
            ProjectList = new ObservableCollection<Project>(App.db.Project.Local.Where(p => p.ManWithResumeId == App.user.id));
        }

        private void AddingProject() 
        {
            new EditingProjectWindow().Show();
        }
        private void DeletingProject() 
        {
            if(ListViewProject.SelectedItem == null) return;

            if (MessageBox.Show("Вы действительно хотите удалить этот проект?", "Удаление проекта", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            App.db.Project.Remove(ListViewProject.SelectedItem as Project);
            App.db.SaveChanges();

            RefreshListProject();
        }
    }
}
