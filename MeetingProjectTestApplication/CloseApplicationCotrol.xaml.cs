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

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для CloseApplicationCotrol.xaml
    /// </summary>
    public partial class CloseApplicationCotrol : UserControl
    {
        public CloseApplicationCotrol()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeContactInformation.Instance.DragMove();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeContactInformation.Instance.Close();
        }
    }
}
