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


        public UserInformationWindow()
        {
            InitializeComponent();
            FrameDisplayingContent.Navigate(new UserInformationPage());
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShutdownApplication();
        }

        private static void ShutdownApplication()
        {
            if (MessageBox.Show("Завершить сеанс?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationButton(1);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationButton(0);
        }

        private void GridContent_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationBorder(1);
        }

        private void GridContent_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationBorder(0);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var byteImage = ImageConverter.OpenFileDialogSave();
            App.user.BackgroundImage = byteImage;
            BackgroundImage.ImageSource = (ImageSource) new ImageSourceConverter().ConvertFrom(byteImage);
            new UserInformationWindowModelView().BackgroundImage = App.user.BackgroundImage;
            App.db.SaveChanges();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.user.BackgroundImage = null;
            if (MessageBox.Show("Поменять фон на стандартный?", "Смена фона", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                BackgroundImage.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFrom(new Uri("Image/EmptyBackgroundImage.png", UriKind.Relative));
            App.db.SaveChanges();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameDisplayingContent.Navigate(new UserInformationPage());
            }
            catch { }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton) == null) return;

            FrameDisplayingContent.Navigate(new ProjectPage());
        }
    }
}
