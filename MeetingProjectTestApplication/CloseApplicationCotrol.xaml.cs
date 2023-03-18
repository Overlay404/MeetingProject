using System.Windows.Controls;
using System.Windows.Input;

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
