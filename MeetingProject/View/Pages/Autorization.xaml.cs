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
        public static Autorization Instance;

        public Autorization()
        {
            Instance = this;
            InitializeComponent();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateAndAutorizateData(Login.Text.Trim(), Password.Password.Trim(), false);
        }

        public void ValidateAndAutorizateData(string login, string password, bool IsReg)
        {
            try { App.user = App.db.ManWithResume.Where(u => (u.email.Equals(login) && u.Password.Equals(password) && u.IsBanned == false)).Select(u => u).FirstOrDefault(); }
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
                Properties.Settings.Default.Login = login;
                Properties.Settings.Default.Password = password;
            }
            else
            {
                Properties.Settings.Default.Login = null;
                Properties.Settings.Default.Password = null;
            }

            Properties.Settings.Default.Save();

            if (IsReg)
            {
                new SelectionOfProjectWindows().ShowDialog();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartWindow.Instance.StartWindowFrame.Navigate(new ConnectionGithub());
        }
    }
}
