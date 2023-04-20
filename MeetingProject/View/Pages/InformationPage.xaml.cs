using MeetingProject.View.UserControls;
using System.Windows;
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
            PhoneButton.Click += (sender, e) => { PhoneCopy(); };
            EmailButton.Click += (sender, e) => { EmailCopy(); };
            TelegramButton.Click += (sender, e) => { TelegramCopy(); };
        }

        private void PhoneCopy()
        {
            Clipboard.SetText(PhoneButton.Content.ToString());
            MessageControl.Instance.StartAnimation();
        }

        private void EmailCopy()
        {
            Clipboard.SetText(EmailButton.Content.ToString());
            MessageControl.Instance.StartAnimation();
        }

        private void TelegramCopy()
        {
            Clipboard.SetText(TelegramButton.Content.ToString());
            MessageControl.Instance.StartAnimation();
        }
    }
}
