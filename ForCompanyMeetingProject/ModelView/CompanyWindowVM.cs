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

        public string GithubLink
        {
            get { return (string)GetValue(GithubLinkProperty); }
            set { SetValue(GithubLinkProperty, value); }
        }

        public static readonly DependencyProperty GithubLinkProperty =
            DependencyProperty.Register("GithubLink", typeof(string), typeof(CompanyWindowVM));

        public byte[] ProfilePhoto
        {
            get { return (byte[])GetValue(ProfilePhotoProperty); }
            set { SetValue(ProfilePhotoProperty, value); }
        }

        public static readonly DependencyProperty ProfilePhotoProperty =
            DependencyProperty.Register("ProfilePhoto", typeof(byte[]), typeof(CompanyWindowVM));

        public string Fullname
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


        public static readonly CompanyWindowVM Instance = new CompanyWindowVM();

        public CompanyWindowVM()
        {
            if (App.user == null) return;
            BackgroundImage = App.user.BackgroundImage ?? ImageConverter.ConvertToByteCollection("Image/EmptyBackgroundImage.png");
            GithubLink = App.user.github ?? "Не подключен";
            ProfilePhoto = App.user.ProfilePhoto ?? ImageConverter.ConvertToByteCollection("Image/EmptyPersonImage.png");
            Fullname = $"{App.user.surname} {App.user.name} {App.user.patronomic}";
            JobTitle = App.user.JobTitle.ToList();
        }

        public static void UpdateData()
        {
            Instance.BackgroundImage = App.user.BackgroundImage ?? ImageConverter.ConvertToByteCollection("Image/EmptyBackgroundImage.png");
            Instance.GithubLink = App.user.github ?? "Не подключен";
            Instance.ProfilePhoto = App.user.ProfilePhoto ?? ImageConverter.ConvertToByteCollection("Image/EmptyPersonImage.png");
            Instance.Fullname = $"{App.user.surname} {App.user.name} {App.user.patronomic}";
            Instance.JobTitle = App.user.JobTitle.ToList();
        }
    }
}
