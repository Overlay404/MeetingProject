using ForCompanyMeetingProject.Model;
using ForCompanyMeetingProject.SupportiveClasses;
using System.Linq;
using System.Windows;

namespace ForCompanyMeetingProject.ModelView
{
    internal class CompanyWindowVM : Window
    {
        public byte[] BackgroundImage
        {
            get { return (byte[])GetValue(BackgroundImageProperty); }
            set { SetValue(BackgroundImageProperty, value); }
        }

        public static readonly DependencyProperty BackgroundImageProperty =
            DependencyProperty.Register("BackgroundImage", typeof(byte[]), typeof(CompanyWindowVM));

        public byte[] ProfilePhoto
        {
            get { return (byte[])GetValue(ProfilePhotoProperty); }
            set { SetValue(ProfilePhotoProperty, value); }
        }

        public static readonly DependencyProperty ProfilePhotoProperty =
            DependencyProperty.Register("ProfilePhoto", typeof(byte[]), typeof(CompanyWindowVM));

        public string NameCompany
        {
            get { return (string)GetValue(FullnameProperty); }
            set { SetValue(FullnameProperty, value); }
        }

        public static readonly DependencyProperty FullnameProperty =
            DependencyProperty.Register("Fullname", typeof(string), typeof(CompanyWindowVM));

        public System.Collections.Generic.List<JobTitle> JobTitle
        {
            get { return (System.Collections.Generic.List<JobTitle>)GetValue(JobTitleProperty); }
            set { SetValue(JobTitleProperty, value); }
        }

        public static readonly DependencyProperty JobTitleProperty =
            DependencyProperty.Register("JobTitle", typeof(System.Collections.Generic.List<JobTitle>), typeof(CompanyWindowVM));

        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }
        public static readonly DependencyProperty PhoneProperty =
            DependencyProperty.Register("Phone", typeof(string), typeof(CompanyWindowVM));


        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }
        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string), typeof(CompanyWindowVM));


        public string Telegram
        {
            get { return (string)GetValue(TelegramProperty); }
            set { SetValue(TelegramProperty, value); }
        }
        public static readonly DependencyProperty TelegramProperty =
            DependencyProperty.Register("Telegram", typeof(string), typeof(CompanyWindowVM));

        public string About
        {
            get { return (string)GetValue(AboutProperty); }
            set { SetValue(AboutProperty, value); }
        }

        public static readonly DependencyProperty AboutProperty =
            DependencyProperty.Register("About", typeof(string), typeof(CompanyWindowVM));



        public static readonly CompanyWindowVM Instance = new CompanyWindowVM();

        public CompanyWindowVM()
        {
            if (App.user == null) return;
            BackgroundImage = App.user.BackgroundImage ?? ImageConverter.ConvertToByteCollection("Image/EmptyBackgroundImage.png");
            ProfilePhoto = App.user.ProfilePhoto ?? ImageConverter.ConvertToByteCollection("Image/EmptyPersonImage.png");
            NameCompany = $"{App.user.LegalForm} {App.user.Name}";
            Phone = App.user.number ?? "Поле не заполнено";
            Email = App.user.email ?? "Поле не заполнено";
            Telegram = App.user.telegram ?? "Поле не заполнено";
            About = App.user.about ?? "Поле не заполнено";
        }
    }
}
