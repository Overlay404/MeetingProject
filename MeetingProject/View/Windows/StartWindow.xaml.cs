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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeetingProject.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {

        public static StartWindow Instance;

        public StartWindow()
        {
            InitializeComponent();
            Instance = this;

            if (String.IsNullOrEmpty(Properties.Settings.Default.Login) && String.IsNullOrEmpty(Properties.Settings.Default.Password)) return;
            
            App.user = App.db.ManWithResume.Local.Where(u => u.Login.Equals(Properties.Settings.Default.Login) && u.Password.Equals(Properties.Settings.Default.Password)).Select(u => u).FirstOrDefault();
            new PortfolioWindow().Show();
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { DragMove(); }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) { ShutdownApplication(); }

        private static void ShutdownApplication()
        {
            if (MessageBox.Show("Завершить сеанс?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void AutorizationButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Autorization == null || StartWindowFrame == null) return;
            StartWindowFrame.Navigate(new Autorization());
        }
        private void RegistrationButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Registration == null || StartWindowFrame == null) return;
            StartWindowFrame.Navigate(new Registration());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Autorization.IsChecked == true)
                StartWindowFrame.Navigate(new Autorization());
            else
                StartWindowFrame.Navigate(new Registration());
        }
    }
}
