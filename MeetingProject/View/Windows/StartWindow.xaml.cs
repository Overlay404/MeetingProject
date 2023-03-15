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

            StartWindowFrame.Navigate(new Autorization());

            if (String.IsNullOrEmpty(Properties.Settings.Default.Login) && String.IsNullOrEmpty(Properties.Settings.Default.Password)) return;
            
            App.user = App.db.ManWithResume.Where(u => u.Login.Equals(Properties.Settings.Default.Login) && u.Password.Equals(Properties.Settings.Default.Password)).Select(u => u).FirstOrDefault();
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
    }
}
