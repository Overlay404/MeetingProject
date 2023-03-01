using System;
using System.Windows;
using System.Windows.Controls;

namespace MeetingProjectTestApplication
{
    public partial class UserInformationPage : Page
    {
        public static UserInformationPage Instance;

        public UserInformationPage()
        {
            InitializeComponent();
            Instance = this;
        }

        public void InitializeEvent() { ChangeContactInformation.Instance.Closed += ChangeContactInformationWindow_Closed; }

        private void ChangeContactInformationWindow_Closed(object sender, EventArgs e) { UserInformationWindow.Instance.FrameDisplayingContent.Navigate(new UserInformationPage()); }

        private void TelegramButton_Click(object sender, RoutedEventArgs e) { new ChangeContactInformation("Тег телеграм:", "Telegram").Show(); }

        private void EmailButton_Click(object sender, RoutedEventArgs e) { new ChangeContactInformation("Почта:", "Email").Show(); }

        private void PhoneButton_Click(object sender, RoutedEventArgs e) { new ChangeContactInformation("Телефон:", "Phone").Show(); }
    }
}
