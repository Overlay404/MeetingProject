using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ForCompanyMeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public static Registration Instance { get; private set; }

        public Registration()
        {
            Instance = this;
            InitializeComponent();
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            string email = Email.Text.Trim();
            string password = Password.Text.Trim();
            string passwordConfirmation = PasswordConfirmation.Text.Trim();

            Regex emailRegex = new Regex(@"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)");
            Regex passwordRegex = new Regex(@"(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}");

            if (emailRegex.IsMatch(email) == false && !password.Equals(passwordConfirmation) && passwordRegex.IsMatch(password) == false)
            {
                MessageBox.Show("Ваши данные не прошли валидацию");
                return;
            }

            App.db.Company.Add(new Model.Company
            {
                email = email,
                Password = password
            });

            App.db.SaveChanges();

            Autorization.Instance.ValidateAndAutorizateData(email, password);
        }
    }
}
