using MeetingProjectTestApplication.Model;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms.VisualStyles;

namespace MeetingProjectTestApplication
{

    public class UserInformationWindowModelView : Window
    {
        public byte[] BackgroundImages
        {
            get { return (byte[])GetValue(BackgroundImageProperty); }
            set { SetValue(BackgroundImageProperty, value); }
        }

        public static readonly DependencyProperty BackgroundImageProperty =
            DependencyProperty.Register("BackgroundImage", typeof(byte[]), typeof(UserInformationWindowModelView));

        
        
        public string github
        {
            get { return (string)GetValue(githubProperty); }
            set { SetValue(githubProperty, value); }
        }

        public static readonly DependencyProperty githubProperty =
            DependencyProperty.Register("github", typeof(string), typeof(UserInformationWindowModelView));

        
        
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
            BackgroundImages = App.user.BackgroundImage;
            github = App.user.github;
            ProfilePhoto = App.user.ProfilePhoto;
            Fullname = $"{App.user.name} {App.user.surname} {App.user.patronomic}";
            JobTitle = App.user.JobTitle.ToList();
        }
    }
}
