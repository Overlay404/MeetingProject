using ForCompanyMeetingProject;
using ForCompanyMeetingProject.Model;
using ForCompanyMeetingProject.SupportiveClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ForCompanyMeetingProject.View.Windows
{
    partial class PortfolioWindow : Window
    {
        public byte[] BackgroundImage
        {
            get { return (byte[])GetValue(BackgroundImageProperty); }
            set { SetValue(BackgroundImageProperty, value); }
        }

        public static readonly DependencyProperty BackgroundImageProperty =
            DependencyProperty.Register("BackgroundImage", typeof(byte[]), typeof(PortfolioWindow));

        public string GithubLink
        {
            get { return (string)GetValue(GithubLinkProperty); }
            set { SetValue(GithubLinkProperty, value); }
        }

        public static readonly DependencyProperty GithubLinkProperty =
            DependencyProperty.Register("GithubLink", typeof(string), typeof(PortfolioWindow));

        public byte[] ProfilePhoto
        {
            get { return (byte[])GetValue(ProfilePhotoProperty); }
            set { SetValue(ProfilePhotoProperty, value); }
        }

        public static readonly DependencyProperty ProfilePhotoProperty =
            DependencyProperty.Register("ProfilePhoto", typeof(byte[]), typeof(PortfolioWindow));

        public string Fullname
        {
            get { return (string)GetValue(FullnameProperty); }
            set { SetValue(FullnameProperty, value); }
        }

        public static readonly DependencyProperty FullnameProperty =
            DependencyProperty.Register("Fullname", typeof(string), typeof(PortfolioWindow));

        public System.Collections.Generic.List<JobTitle> JobTitle
        {
            get { return (System.Collections.Generic.List<JobTitle>)GetValue(JobTitleProperty); }
            set { SetValue(JobTitleProperty, value); }
        }

        public static readonly DependencyProperty JobTitleProperty =
            DependencyProperty.Register("JobTitle", typeof(System.Collections.Generic.List<JobTitle>), typeof(PortfolioWindow));

        private void InitializeDataContext(ManWithResume man)
        {
            BackgroundImage = man.BackgroundImage ?? ImageConverter.ConvertToByteCollection("Image/EmptyBackgroundImage.png");
            GithubLink = man.github ?? "Не подключен";
            ProfilePhoto = man.ProfilePhoto ?? ImageConverter.ConvertToByteCollection("Image/EmptyPersonImage.png");
            Fullname = $"{man.surname} {man.name} {man.patronomic}";
            JobTitle = man.JobTitle.ToList();
        }
    }
}
