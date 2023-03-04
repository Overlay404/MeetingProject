using AngleSharp;
using AngleSharp.Dom;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для ChangeContactInformation.xaml
    /// </summary>
    public partial class ChangeContactInformation : Window
    {
        public static ChangeContactInformation Instance;

        public static string UriGithubProfileImage;


        public ImageSource GithubProfilePhoto
        {
            get { return (ImageSource)GetValue(GithubProfilePhotoProperty); }
            set { SetValue(GithubProfilePhotoProperty, value); }
        }

        public static readonly DependencyProperty GithubProfilePhotoProperty =
            DependencyProperty.Register("GithubProfilePhoto", typeof(ImageSource), typeof(ChangeContactInformation));


        public string NameGithubProfile
        {
            get { return (string)GetValue(NameGithubProfileProperty); }
            set { SetValue(NameGithubProfileProperty, value); }
        }

        public static readonly DependencyProperty NameGithubProfileProperty =
            DependencyProperty.Register("NameGithubProfile", typeof(string), typeof(ChangeContactInformation));


        public string AboutGithubProfile
        {
            get { return (string)GetValue(AboutGithubProfileProperty); }
            set { SetValue(AboutGithubProfileProperty, value); }
        }

        public static readonly DependencyProperty AboutGithubProfileProperty =
            DependencyProperty.Register("AboutGithubProfile", typeof(string), typeof(ChangeContactInformation));



        public ChangeContactInformation()
        {
            GithubProfilePhoto = null;
            InitializeComponent();
            Instance = this;
        }

        private void ButtonPress_Click(object sender, RoutedEventArgs e)
        {

        }

        public void GetLinkGithudProfileImage()
        {
            try
            {
                var request = System.Net.WebRequest.Create(UriGithubProfileImage);
                var response = request.GetResponse();
                System.Drawing.Bitmap loadedBitmap = null;
                using (var responseStream = response.GetResponseStream())
                {
                    loadedBitmap = new System.Drawing.Bitmap(responseStream);
                }
                MemoryStream ms = new MemoryStream();
                loadedBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.EndInit();
                GithubProfilePhoto = image;
                BorderGithubProfile.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception ex)
            {
                BorderGithubProfile.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        public async Task GetInformationProfileGithubAsync(string NameUserGithub)
        {
            string nameProfile = $"https://github.com/{NameUserGithub}";

            UriGithubProfileImage = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(nameProfile)).QuerySelectorAll(".avatar-user").Select(m => m.Attributes["src"].Value).FirstOrDefault();

            string nameBaseProfile = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(nameProfile)).QuerySelectorAll(".vcard-fullname").Select(m => m.Text()).FirstOrDefault();
            string nameCollaborationProfile = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(nameProfile)).QuerySelectorAll(".h2.lh-condensed").Select(m => m.Text()).FirstOrDefault();
            string usernameCollaborationProfile = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(nameProfile)).QuerySelectorAll(".vcard-username").Select(m => m.Text()).FirstOrDefault();

            if(nameBaseProfile != null) { NameGithubProfile = nameBaseProfile; }
            else if(nameCollaborationProfile != null) { NameGithubProfile = nameCollaborationProfile; }
            else { NameGithubProfile = usernameCollaborationProfile; }

            AboutGithubProfile = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(nameProfile)).QuerySelectorAll(".user-profile-bio").Select(m => m.Attributes["data-bio-text"].Value).FirstOrDefault();
        }

        private async void ValueComponent_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (GithabNameAccount.Text == null || GithabNameAccount.Text == "") { BorderGithubProfile.Visibility = System.Windows.Visibility.Collapsed; return; }
            await GetInformationProfileGithubAsync(GithabNameAccount.Text);
            GetLinkGithudProfileImage();
        }
    }
}
