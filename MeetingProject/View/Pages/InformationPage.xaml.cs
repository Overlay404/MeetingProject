using MeetingProject.View.UserControls;
using System.Windows.Controls;

namespace MeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для InformationPage.xaml
    /// </summary>
    public partial class InformationPage : Page
    {
        public InformationPage()
        {
            InitializeComponent();
            PhoneButton.Click += (sender, e) => { MessageControl.Instance.StartAnimation(); };
            EmailButton.Click += (sender, e) => { MessageControl.Instance.StartAnimation(); };
            TelegramButton.Click += (sender, e) => { MessageControl.Instance.StartAnimation(); };
        }
    }
}
