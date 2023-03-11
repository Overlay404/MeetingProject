using MeetingProject.ModelView;
using MeetingProject.SupportiveClasses;
using MeetingProject.View.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeetingProject.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для PortfolioWindow.xaml
    /// </summary>
    public partial class PortfolioWindow : Window
    {
        public static PortfolioWindow Instance;

        public PortfolioWindow()
        {
            InitializeComponent();
            Instance = this;
            FrameDisplayingContent.Navigate(new InformationPage());
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { DragMove(); }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e) { ShutdownApplication(); }

        private void Grid_MouseEnter(object sender, MouseEventArgs e) { AnimationButton(1); }

        private void Grid_MouseLeave(object sender, MouseEventArgs e) { AnimationButton(0); }

        private void GridContent_MouseEnter(object sender, MouseEventArgs e) { AnimationBorder(1); }

        private void GridContent_MouseLeave(object sender, MouseEventArgs e) { AnimationBorder(0); }

        private void AddBackgroundImageButton_Click(object sender, RoutedEventArgs e)
        {
            var byteImage = ImageConverter.OpenFileDialogSave();
            App.user.BackgroundImage = byteImage;
            App.db.SaveChanges();
            BackgroundImage.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFrom(byteImage) ?? (ImageSource)new ImageSourceConverter().ConvertFrom(new Uri("Image/EmptyBackgroundImage.png", UriKind.Relative));
        }

        private void DeleteBackgroundImageButton_Click(object sender, RoutedEventArgs e)
        {
            App.user.BackgroundImage = null;
            if (MessageBox.Show("Поменять фон на стандартный?", "Смена фона", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                BackgroundImage.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFrom(new Uri("Image/EmptyBackgroundImage.png", UriKind.Relative));
            App.db.SaveChanges();
        }

        private void MyInfoButton_Checked(object sender, RoutedEventArgs e) { if (this.IsLoaded) FrameDisplayingContent.Navigate(new InformationPage()); }

        private void MyProjectButton_Checked(object sender, RoutedEventArgs e) { if ((sender as RadioButton) != null) FrameDisplayingContent.Navigate(new ProjectPage()); }

        private void ProfileImage_MouseEnter(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation();
            animation.From = ChangeBackgroundImage.Opacity;
            animation.To = 0.5;
            animation.Duration = TimeSpan.FromSeconds(0.2);
            Profile.BeginAnimation(OpacityProperty, animation);
            ButtonProfile.Opacity = 1;
        }

        private void ProfileImage_MouseLeave(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation();
            animation.From = ChangeBackgroundImage.Opacity;
            animation.To = 1;
            animation.Duration = TimeSpan.FromSeconds(0.2);
            Profile.BeginAnimation(OpacityProperty, animation);
            ButtonProfile.Opacity = 0;
        }

        private void AddProfileImageButton_Click(object sender, RoutedEventArgs e)
        {
            var byteImage = ImageConverter.OpenFileDialogSave();
            if (byteImage != null) App.user.ProfilePhoto = byteImage;
            App.db.SaveChanges();
            PortfolioWindowVM.Instance.ProfilePhoto = App.user.ProfilePhoto;
        }

        private void DeleteProfileImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Поменять фото профиля на стандартное?", "Смена фото профиля", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                App.user.ProfilePhoto = null;
                PortfolioWindowVM.Instance.ProfilePhoto = App.user.ProfilePhoto;
                App.db.SaveChanges();
        }

        private void GithubLinkEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //new ChangeContactInformation().Show();
        }
       

        private void AnimationButton(int Opacity)
        {
            var animation = new DoubleAnimation();
            animation.From = ChangeBackgroundImage.Opacity;
            animation.To = Opacity;
            animation.Duration = TimeSpan.FromSeconds(0.3);
            ChangeBackgroundImage.BeginAnimation(OpacityProperty, animation);
        }

        private void AnimationBorder(int Opacity)
        {
            var animation = new DoubleAnimation();
            animation.From = ChangeBackgroundImage.Opacity;
            animation.To = Opacity;
            animation.Duration = TimeSpan.FromSeconds(0.3);
            ChangeBackgroundImageButtons.BeginAnimation(OpacityProperty, animation);
        }

        private static void ShutdownApplication()
        {
            if (MessageBox.Show("Завершить сеанс?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

    }
}
