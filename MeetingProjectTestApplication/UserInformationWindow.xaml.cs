using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для UserInformationWindow.xaml
    /// </summary>
    public partial class UserInformationWindow : Window
    {
        public static UserInformationWindow Instance;

        public UserInformationWindow()
        {
            InitializeComponent();
            Instance = this;
            FrameDisplayingContent.Navigate(new UserInformationPage());
        }
        /// <summary>
        /// Event
        /// </summary>
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

        private void MyInfoButton_Checked(object sender, RoutedEventArgs e) { if (this.IsLoaded) FrameDisplayingContent.Navigate(new UserInformationPage()); }

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
            Profile.Source = byteImage == null ? (ImageSource)new ImageSourceConverter().ConvertFrom(new UserInformationWindowModelView().ProfilePhoto) : (ImageSource)new ImageSourceConverter().ConvertFrom(byteImage);
        }

        private void DeleteProfileImageButton_Click(object sender, RoutedEventArgs e)
        {
            App.user.ProfilePhoto = null;
            if (MessageBox.Show("Поменять фото профиля на стандартное?", "Смена фото профиля", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                Profile.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(new UserInformationWindowModelView().ProfilePhoto);
            App.db.SaveChanges();
        }

        private void GithubLinkEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// Методы
        /// </summary>
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
