using System;
using System.Windows;
using System.Windows.Controls;

namespace MeetingProjectTestApplication
{
    public partial class UserInformationPage : Page
    {

        public static UserInformationPage Instance;


        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }
        public static readonly DependencyProperty PhoneProperty =
            DependencyProperty.Register("Phone", typeof(string), typeof(UserInformationPage));


        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }
        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string), typeof(UserInformationPage));


        public string Telegram
        {
            get { return (string)GetValue(TelegramProperty); }
            set { SetValue(TelegramProperty, value); }
        }
        public static readonly DependencyProperty TelegramProperty =
            DependencyProperty.Register("Telegram", typeof(string), typeof(UserInformationPage));


        public UserInformationPage()
        {
            InitializeComponent();
            Update();
            Instance = this;
        }

        public void InitializeEvent() { ChangeContactInformation.Instance.Closed += ChangeContactInformationWindow_Closed; }

        private void ChangeContactInformationWindow_Closed(object sender, EventArgs e) { Update(); UserInformationWindow.Instance.FrameDisplayingContent.Navigate(new UserInformationPage()); }

        private void Update()
        {
            Phone = App.user.number;
            Email = App.user.email;
            Telegram = App.user.telegram;
        }

        private void TelegramButton_Click(object sender, RoutedEventArgs e)
        {
            new ChangeContactInformation("Тег телеграм:", "Telegram").Show();
        }

        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            new ChangeContactInformation("Почта:", "Email").Show();
        }

        private void PhoneButton_Click(object sender, RoutedEventArgs e)
        {
            new ChangeContactInformation("Телефон:", "Phone").Show();
        }
    }
}
