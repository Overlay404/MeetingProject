using MeetingProject.Model;
using MeetingProject.View.Windows;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static MeetingProject.SupportiveClasses.ImageConverter;

namespace MeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingProjectPage.xaml
    /// </summary>
    public partial class SettingProjectPage : Page
    {
        public Project ProjectObject { get; private set; }

        public BitmapSource MainPicture
        {
            get { return (BitmapSource)GetValue(MainPictureProperty); }
            set { SetValue(MainPictureProperty, value); }
        }

        public static readonly DependencyProperty MainPictureProperty =
            DependencyProperty.Register("MainPicture", typeof(BitmapSource), typeof(SettingProjectPage));



        public string TitleProject
        {
            get { return (string)GetValue(TitleProjectProperty); }
            set { SetValue(TitleProjectProperty, value); }
        }

        public static readonly DependencyProperty TitleProjectProperty =
            DependencyProperty.Register("TitleProject", typeof(string), typeof(SettingProjectPage));




        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(SettingProjectPage));



        public SettingProjectPage(Project project = null)
        {
            ProjectObject = project;
            MainPicture = (BitmapSource)ConvertToImageSource(project.MainPicture);
            TitleProject = project.title;
            Description = project.description;
            InitializeComponent();
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                BitmapImage image = new BitmapImage(new System.Uri((e.Data.GetData(DataFormats.FileDrop) as string[])[0]));
                MainPicture = image;
            }
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var image = (BitmapSource)ConvertToImageSource(OpenFileDialogSave());
            if (image == null) return;
            MainPicture = image;
        }

        private void AcceptingChanges_Click(object sender, RoutedEventArgs e)
        {
            ProjectObject.MainPicture = ConvertToByteCollection(MainPicture);
            ProjectObject.title = TitleProject;
            ProjectObject.description = Description;

            App.db.SaveChanges();

            EditingProjectWindow.Instance.FramePageEditing.Navigate(new EditingProjectPage(ProjectObject));
        }
    }
}
