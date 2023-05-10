using ForCompanyMeetingProject.Model;
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

namespace ForCompanyMeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для InformationPage.xaml
    /// </summary>
    public partial class InformationPage : Page
    {

        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }
        public static readonly DependencyProperty PhoneProperty =
            DependencyProperty.Register("Phone", typeof(string), typeof(InformationPage));


        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }
        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string), typeof(InformationPage));


        public string Telegram
        {
            get { return (string)GetValue(TelegramProperty); }
            set { SetValue(TelegramProperty, value); }
        }
        public static readonly DependencyProperty TelegramProperty =
            DependencyProperty.Register("Telegram", typeof(string), typeof(InformationPage));


        public List<Experience> ExperienceList
        {
            get { return (List<Experience>)GetValue(ExperienceListProperty); }
            set { SetValue(ExperienceListProperty, value); }
        }

        public static readonly DependencyProperty ExperienceListProperty =
            DependencyProperty.Register("ExperienceList", typeof(List<Experience>), typeof(InformationPage));


        public List<Education> EducationList
        {
            get { return (List<Education>)GetValue(EducationListProperty); }
            set { SetValue(EducationListProperty, value); }
        }

        public static readonly DependencyProperty EducationListProperty =
            DependencyProperty.Register("EducationList", typeof(List<Education>), typeof(InformationPage));


        public string About
        {
            get { return (string)GetValue(AboutProperty); }
            set { SetValue(AboutProperty, value); }
        }

        public static readonly DependencyProperty AboutProperty =
            DependencyProperty.Register("About", typeof(string), typeof(InformationPage));


        public InformationPage(ManWithResume man)
        {
            InitializeDataContext(man);
            InitializeComponent();
        }

        private void InitializeDataContext(ManWithResume man)
        {
            Phone = man.number ?? "Поле не заполнено";
            Email = man.email ?? "Поле не заполнено";
            Telegram = man.telegram ?? "Поле не заполнено";
            ExperienceList = man.Experience.ToList();
            EducationList = man.Education.ToList();
            About = man.about ?? "Поле не заполнено";
        }
    }
}
