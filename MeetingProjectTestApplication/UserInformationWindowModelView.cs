using MeetingProjectTestApplication.Model;
using System.Linq;
using System.Windows;

namespace MeetingProjectTestApplication
{

    public class UserInformationWindowModelView : Window
    {
        public byte[] BackgroundImage
        {
            get { return (byte[])GetValue(BackgroundImageProperty); }
            set { SetValue(BackgroundImageProperty, value); }
        }

        public static readonly DependencyProperty BackgroundImageProperty =
            DependencyProperty.Register("BackgroundImage", typeof(byte[]), typeof(UserInformationWindow));




        public string GithubLink
        {
            get { return (string)GetValue(GithubLinkProperty); }
            set { SetValue(GithubLinkProperty, value); }
        }

        public static readonly DependencyProperty GithubLinkProperty =
            DependencyProperty.Register("GithubLink", typeof(string), typeof(UserInformationWindow));




        public byte[] ProfilePhoto
        {
            get { return (byte[])GetValue(ProfilePhotoProperty); }
            set { SetValue(ProfilePhotoProperty, value); }
        }

        public static readonly DependencyProperty ProfilePhotoProperty =
            DependencyProperty.Register("github", typeof(byte[]), typeof(UserInformationWindow));




        public string Fullname
        {
            get { return (string)GetValue(FullnameProperty); }
            set { SetValue(FullnameProperty, value); }
        }

        public static readonly DependencyProperty FullnameProperty =
            DependencyProperty.Register("Fullname", typeof(string), typeof(UserInformationWindow));




        public System.Collections.Generic.List<JobTitle> JobTitle
        {
            get { return (System.Collections.Generic.List<JobTitle>)GetValue(JobTitleProperty); }
            set { SetValue(JobTitleProperty, value); }
        }

        public static readonly DependencyProperty JobTitleProperty =
            DependencyProperty.Register("JobTitle", typeof(System.Collections.Generic.List<JobTitle>), typeof(UserInformationWindow));


        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }
        public static readonly DependencyProperty PhoneProperty =
            DependencyProperty.Register("Phone", typeof(string), typeof(UserInformationWindowModelView));


        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }
        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string), typeof(UserInformationWindowModelView));


        public string Telegram
        {
            get { return (string)GetValue(TelegramProperty); }
            set { SetValue(TelegramProperty, value); }
        }
        public static readonly DependencyProperty TelegramProperty =
            DependencyProperty.Register("Telegram", typeof(string), typeof(UserInformationWindowModelView));

        public UserInformationWindowModelView()
        {
            UpdateData();
        }

        public void UpdateData()
        {
            BackgroundImage = App.user.BackgroundImage ?? ImageConverter.ConvertToByteCollection("Image/EmptyBackgroundImage.png");
            GithubLink = App.user.github ?? "Не подключен";
            ProfilePhoto = App.user.ProfilePhoto ?? ImageConverter.ConvertToByteCollection("Image/EmptyPersonImage.png");
            Fullname = $"{App.user.surname} {App.user.name} {App.user.patronomic}";
            JobTitle = App.user.JobTitle.ToList();
            Phone = App.user.number ?? "Поле не заполнено";
            Email = App.user.email ?? "Поле не заполнено";
            Telegram = App.user.telegram ?? "Поле не заполнено";
        }
    }
}
