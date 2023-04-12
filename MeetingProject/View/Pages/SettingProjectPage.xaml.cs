using MeetingProject.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingProjectPage.xaml
    /// </summary>
    public partial class SettingProjectPage : Page
    {
        public Project ProjectObject { get; private set; }

        public SettingProjectPage(Project project = null)
        {
            ProjectObject = project;
            InitializeComponent();
        }

        private void Image_DragEnter(object sender, DragEventArgs e)
        {
            MainPictureDrop.Source = e.Source as ImageSource;
        }

        private void AcceptingChanges_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
