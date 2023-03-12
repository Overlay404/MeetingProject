using MeetingProject.Model;
using MeetingProject.ModelView;
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


        public List<Experience> ExperienceList
        {
            get { return (List<Experience>)GetValue(ExperienceListProperty); }
            set { SetValue(ExperienceListProperty, value); }
        }

        public static readonly DependencyProperty ExperienceListProperty =
            DependencyProperty.Register("ExperienceList", typeof(List<Experience>), typeof(InformationPageVM));


        public List<Education> EducationList
        {
            get { return (List<Education>)GetValue(EducationListProperty); }
            set { SetValue(EducationListProperty, value); }
        }

        public static readonly DependencyProperty EducationListProperty =
            DependencyProperty.Register("EducationList", typeof(List<Education>), typeof(InformationPageVM));


        public string About
        {
            get { return (string)GetValue(AboutProperty); }
            set { SetValue(AboutProperty, value); }
        }

        public static readonly DependencyProperty AboutProperty =
            DependencyProperty.Register("About", typeof(string), typeof(InformationPageVM));



        public InformationPageVM()
        {
            Phone = App.user.number ?? "Поле не заполнено";
            Email = App.user.email ?? "Поле не заполнено";
            Telegram = App.user.telegram ?? "Поле не заполнено";
            ExperienceList = App.user.Experience.ToList();
            EducationList = App.user.Education.ToList();
            About = App.user.about ?? "Поле не заполнено";
        }
    }
}
