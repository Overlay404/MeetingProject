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
    }
}
