using MeetingProject.ModelView;
using MeetingProject.SupportiveClasses;
using MeetingProject.View.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MeetingProject.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для PortfolioWindow.xaml
    /// </summary>
    public partial class PortfolioWindow : Window
    {
        public static PortfolioWindow Instance;
        private double screenWidth;
        private double positionCursorX;

        public PortfolioWindow()
        {
            DataContext = new PortfolioWindowVM();

            InitializeComponent();

            Instance = this;
            FrameDisplayingContent.Navigate(new InformationPage());
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                screenWidth = SystemParameters.FullPrimaryScreenWidth;
                positionCursorX = PointToScreen(new Point(e.GetPosition(null).X, e.GetPosition(null).Y)).X;
                WindowState = WindowState.Normal;
                Top = 0;
                Left = positionCursorX - Width / 2;
            }
            DragMove();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e) { ShutdownApplication(); }

        private void Grid_MouseEnter(object sender, MouseEventArgs e) { AnimationButton(1); }

        private void Grid_MouseLeave(object sender, MouseEventArgs e) { AnimationButton(0); }

        private void GridContent_MouseEnter(object sender, MouseEventArgs e) { AnimationBorder(1); }

        private void GridContent_MouseLeave(object sender, MouseEventArgs e) { AnimationBorder(0); }

        private void MyInfoButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                FrameDisplayingContent.Navigate(new InformationPage());
            }
        }

        private void MyProjectButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton) != null)
            {
                FrameDisplayingContent.Navigate(new ProjectPage());
            }
        }

        private void GithubLinkEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Вызов окна изменения Github проектов 
        }

        private void ImageAwesome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.user = null;
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Login = "";
            Properties.Settings.Default.Save();
            new StartWindow().Show();
            Close();
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
            PortfolioWindowVM.Instance.ProfilePhoto = App.user.ProfilePhoto;
            UpdateDataContext();
        }

        private void DeleteProfileImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Поменять фото профиля на стандартное?", "Смена фото профиля", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                App.user.ProfilePhoto = null;
                PortfolioWindowVM.Instance.ProfilePhoto = App.user.ProfilePhoto;
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

        private static void ShutdownApplication()
        {
            if (MessageBox.Show("Завершить сеанс?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }


        private void UpdateDataContext()
        {
            DataContext = new PortfolioWindowVM();
        }


        #region ResizeWindows
        private bool ResizeInProcess = false;
        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            if (sender is Rectangle senderRect)
            {
                ResizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = false; ;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (ResizeInProcess)
            {
                Rectangle senderRect = sender as Rectangle;
                Window mainWindow = senderRect.Tag as Window;
                if (senderRect != null)
                {
                    double width = e.GetPosition(mainWindow).X;
                    double height = e.GetPosition(mainWindow).Y;
                    senderRect.CaptureMouse();
                    if (senderRect.Name.ToLower().Contains("right"))
                    {
                        width += 5;
                        if (width > 0)
                        {
                            mainWindow.Width = width;
                        }
                    }
                    if (senderRect.Name.ToLower().Contains("left"))
                    {
                        width -= 5;
                        mainWindow.Left += width;
                        width = mainWindow.Width - width;
                        if (width > 0)
                        {
                            mainWindow.Width = width;
                        }
                    }
                    if (senderRect.Name.ToLower().Contains("bottom"))
                    {
                        height += 5;
                        if (height > 0)
                        {
                            mainWindow.Height = height;
                        }
                    }
                    if (senderRect.Name.ToLower().Contains("top"))
                    {
                        height -= 5;
                        mainWindow.Top += height;
                        height = mainWindow.Height - height;
                        if (height > 0)
                        {
                            mainWindow.Height = height;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
