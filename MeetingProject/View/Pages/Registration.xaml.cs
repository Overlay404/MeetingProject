using MeetingProject.View.UserControls;
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
            var content = Email.Text + Password.Text + PasswordConfirmation.Text + GitHubConnectionControl.Instance.GithabNameAccount.Text;
            MessageBox.Show(content);
        }
    }
}
