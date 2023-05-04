using ForCompanyMeetingProject.ModelView;
using ForCompanyMeetingProject.SupportiveClasses;
using ForCompanyMeetingProject.View.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ForCompanyMeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CompanePage.xaml
    /// </summary>
    public partial class CompanyPage : Page
    {
        public CompanyPage()
        {
            InitializeComponent();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e) { AnimationButton(1); }

        private void Grid_MouseLeave(object sender, MouseEventArgs e) { AnimationButton(0); }

        private void GridContent_MouseEnter(object sender, MouseEventArgs e) { AnimationBorder(1); }

        private void GridContent_MouseLeave(object sender, MouseEventArgs e) { AnimationBorder(0); }

        private void ImageAwesome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.MainFrame.Navigate(new ManWithResumeListPage());
        }

        private void ProfileImage_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = ChangeBackgroundImage.Opacity,
                To = 0.5,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            Profile.BeginAnimation(OpacityProperty, animation);
            ButtonProfile.Opacity = 1;
        }

        private void ProfileImage_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = ChangeBackgroundImage.Opacity,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            Profile.BeginAnimation(OpacityProperty, animation);
            ButtonProfile.Opacity = 0;
        }

        private void AddProfileImageButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] byteImage = ImageConverter.OpenFileDialogSave();
            if (byteImage != null)
            {
                App.user.ProfilePhoto = byteImage;
            }

            App.db.SaveChanges();
            CompanyWindowVM.Instance.ProfilePhoto = App.user.ProfilePhoto;
            UpdateDataContext();
        }

        private void DeleteProfileImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Поменять фото профиля на стандартное?", "Смена фото профиля", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                App.user.ProfilePhoto = null;
                CompanyWindowVM.Instance.ProfilePhoto = App.user.ProfilePhoto;
                App.db.SaveChanges();
                UpdateDataContext();
            }
        }

        private void AddBackgroundImageButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] byteImage = ImageConverter.OpenFileDialogSave();
            if (byteImage == null)
            {
                return;
            }

            App.user.BackgroundImage = byteImage;
            App.db.SaveChanges();
            UpdateDataContext();
        }

        private void DeleteBackgroundImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Поменять фон на стандартный?", "Смена фона", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                App.user.BackgroundImage = null;
                App.db.SaveChanges();
                UpdateDataContext();
            }
        }


        private void AnimationButton(int Opacity)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = ChangeBackgroundImage.Opacity,
                To = Opacity,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            ChangeBackgroundImage.BeginAnimation(OpacityProperty, animation);
        }

        private void AnimationBorder(int Opacity)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = ChangeBackgroundImage.Opacity,
                To = Opacity,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            ChangeBackgroundImageButtons.BeginAnimation(OpacityProperty, animation);
        }

        private void UpdateDataContext()
        {
            DataContext = new CompanyWindowVM();
        }
    }
}
