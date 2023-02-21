using MeetingProjectTestApplication.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms.VisualStyles;

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
            DependencyProperty.Register("BackgroundImage", typeof(byte[]), typeof(UserInformationWindowModelView));

        
        

        public string GithubLink
        {
            get { return (string)GetValue(GithubLinkProperty); }
            set { SetValue(GithubLinkProperty, value); }
        }

        public static readonly DependencyProperty GithubLinkProperty =
            DependencyProperty.Register("GithubLink", typeof(string), typeof(UserInformationWindowModelView));

        
        

        public byte[] ProfilePhoto
        {
            get { return (byte[])GetValue(ProfilePhotoProperty); }
            set { SetValue(ProfilePhotoProperty, value); }
        }

        public static readonly DependencyProperty ProfilePhotoProperty =
            DependencyProperty.Register("github", typeof(byte[]), typeof(UserInformationWindowModelView));




        public string Fullname
        {
            get { return (string)GetValue(FullnameProperty); }
            set { SetValue(FullnameProperty, value); }
        }

        public static readonly DependencyProperty FullnameProperty =
            DependencyProperty.Register("Fullname", typeof(string), typeof(UserInformationWindowModelView));




        public System.Collections.Generic.List<JobTitle> JobTitle
        {
            get { return (System.Collections.Generic.List<JobTitle>)GetValue(JobTitleProperty); }
            set { SetValue(JobTitleProperty, value); }
        }

        public static readonly DependencyProperty JobTitleProperty =
            DependencyProperty.Register("JobTitle", typeof(System.Collections.Generic.List<JobTitle>), typeof(UserInformationWindowModelView));


        

        public UserInformationWindowModelView()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            BackgroundImage = App.user.BackgroundImage ?? ImageConverter.ConvertToByteCollection("Image/EmptyBackgroundImage.png");
            GithubLink = App.user.github ?? "Не подключен";
            ProfilePhoto = App.user.ProfilePhoto ?? ImageConverter.ConvertToByteCollection("Image/EmptyPersonImage.png");
            Fullname = $"{App.user.name} {App.user.surname} {App.user.patronomic}";
            JobTitle = App.user.JobTitle.ToList();
        }
    }
}
