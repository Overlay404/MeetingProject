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

namespace MeetingProject.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CloseApplicationControl.xaml
    /// </summary>
    public partial class CloseApplicationControl : UserControl
    {
        public CloseApplicationControl()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GithubConnection.Instance.DragMove();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GithubConnection.Instance.Close();
        }
    }
}
