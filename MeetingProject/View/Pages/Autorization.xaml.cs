using MeetingProject.View.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Page
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            App.user = App.db.ManWithResume.Where(u => u.Login.Equals(login.Text.Trim()) && u.Password.Equals(password.Password.Trim())).Select(u => u).FirstOrDefault();

            if (App.user == null)
            {
                MessageBox.Show("Такого пользователя не существует");
                return;
            }
            else
            {
                new PortfolioWindow().Show();
                StartWindow.Instance.Close();
            }

            if (saveDataCheckBox.IsChecked == false)
            {
                Properties.Settings.Default.Login = login.Text.Trim();
                Properties.Settings.Default.Password = password.Password.Trim();
            }
            else
            {
                Properties.Settings.Default.Login = null;
                Properties.Settings.Default.Password = null;
            }
            Properties.Settings.Default.Save();

            
        }
    }
}
