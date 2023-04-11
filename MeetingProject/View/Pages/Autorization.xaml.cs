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
            try { App.user = App.db.ManWithResume.Local.Where(u => u.Login.Equals(Login.Text.Trim()) && u.Password.Equals(Password.Password.Trim())).Select(u => u).FirstOrDefault(); }
            catch
            {
                MessageBox.Show("База данных отсутствует");
                return;
            }

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

            if (SaveDataCheckBox.IsChecked == false)
            {
                Properties.Settings.Default.Login = Login.Text.Trim();
                Properties.Settings.Default.Password = Password.Password.Trim();
            }
            else
            {
                Properties.Settings.Default.Login = null;
                Properties.Settings.Default.Password = null;
            }

            Properties.Settings.Default.Save();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartWindow.Instance.StartWindowFrame.Navigate(new ConnectionGithub());
        }
    }
}
