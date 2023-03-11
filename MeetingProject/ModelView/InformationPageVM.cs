using MeetingProject.SupportiveClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeetingProject.ModelView
{
    internal class InformationPageVM : Window
    {
        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }
        public static readonly DependencyProperty PhoneProperty =
            DependencyProperty.Register("Phone", typeof(string), typeof(InformationPageVM));


        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }
        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string), typeof(InformationPageVM));


        public string Telegram
        {
            get { return (string)GetValue(TelegramProperty); }
            set { SetValue(TelegramProperty, value); }
        }
        public static readonly DependencyProperty TelegramProperty =
            DependencyProperty.Register("Telegram", typeof(string), typeof(InformationPageVM));

        public static readonly InformationPageVM Instance = new InformationPageVM();

        private InformationPageVM()
        {
            UpdateData();
        }

        public static void UpdateData()
        {
            Instance.Phone = App.user.number ?? "Поле не заполнено";
            Instance.Email = App.user.email ?? "Поле не заполнено";
            Instance.Telegram = App.user.telegram ?? "Поле не заполнено";
        }
    }
}
