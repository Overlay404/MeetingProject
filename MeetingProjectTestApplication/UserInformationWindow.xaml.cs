using System;
using System.Windows;
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
            DoubleAnimationUsingPath doubleAnimationUsing = new DoubleAnimationUsingPath();

            PathGeometry animationPath = new PathGeometry();
            PathFigure pFigure = new PathFigure();
            pFigure.StartPoint = new Point(10, 100);
            PolyBezierSegment pBezierSegment = new PolyBezierSegment();
            pBezierSegment.Points.Add(new Point(35, 0));
            pBezierSegment.Points.Add(new Point(135, 0));
            pBezierSegment.Points.Add(new Point(160, 100));
            pBezierSegment.Points.Add(new Point(180, 190));
            pBezierSegment.Points.Add(new Point(285, 200));
            pBezierSegment.Points.Add(new Point(310, 100));
            pFigure.Segments.Add(pBezierSegment);
            animationPath.Figures.Add(pFigure);
            animationPath.Freeze();

            doubleAnimationUsing.PathGeometry = animationPath;
            doubleAnimationUsing.Duration = TimeSpan.FromSeconds(5);
            doubleAnimationUsing.Source = PathAnimationSource.Y;

            Storyboard pathAnimationStoryboard = new Storyboard();
            pathAnimationStoryboard.RepeatBehavior = RepeatBehavior.Forever;
            pathAnimationStoryboard.Children.Add(doubleAnimationUsing);

            Storyboard.SetTargetName(doubleAnimationUsing, "AnimatedTranslateTransform");
            Storyboard.SetTargetProperty(doubleAnimationUsing,
                new PropertyPath(TranslateTransform.YProperty));

            pathAnimationStoryboard.Begin();

        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeBackgroundImage.Width = 1000;
        }
    }
}
