using ForCompanyMeetingProject.Model;
using ForCompanyMeetingProject.ModelView;
using ForCompanyMeetingProject.SupportiveClasses;
using ForCompanyMeetingProject.View.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ForCompanyMeetingProject.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для PortfolioWindow.xaml
    /// </summary>
    public partial class PortfolioWindow : Window
    {
        public static PortfolioWindow Instance;
        private double positionCursorX;

        public ManWithResume Man { get; set; }

        public PortfolioWindow(ManWithResume man)
        {
            InitializeDataContext(man);
            InitializeComponent();
            Man = man;
            Instance = this;
            FrameDisplayingContent.Navigate(new InformationPage(man));
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                positionCursorX = PointToScreen(new Point(e.GetPosition(null).X, e.GetPosition(null).Y)).X;
                WindowState = WindowState.Normal;
                Top = 0;
                Left = positionCursorX - Width / 2;
            }
            DragMove();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e) { ShutdownApplication(); }

        private void MyInfoButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                FrameDisplayingContent.Navigate(new InformationPage(Man));
            }
        }

        private void MyProjectButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton) != null)
            {
                FrameDisplayingContent.Navigate(new ProjectPage(Man));
            }
        }

        private void ShutdownApplication()
        {
            Close();
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
