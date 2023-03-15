using AngleSharp;
using AngleSharp.Dom;
using MeetingProject.ModelView;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeetingProject.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для GithubConnection.xaml
    /// </summary>
    public partial class GithubConnection : System.Windows.Window
    {
        public static GithubConnection Instance { get; private set; }

        public static string UriGithubProfileImage;

        string NameProfile;

        string NameBaseProfile;

        string NameCollaborationProfile;

        string UsernameCollaborationProfile;

        string AboutProfileGithub;

        public ImageSource GithubProfilePhoto
        {
            get { return (ImageSource)GetValue(GithubProfilePhotoProperty); }
            set { SetValue(GithubProfilePhotoProperty, value); }
        }

        public static readonly DependencyProperty GithubProfilePhotoProperty =
            DependencyProperty.Register("GithubProfilePhoto", typeof(ImageSource), typeof(GithubConnection));


        public string NameGithubProfile
        {
            get { return (string)GetValue(NameGithubProfileProperty); }
            set { SetValue(NameGithubProfileProperty, value); }
        }

        public static readonly DependencyProperty NameGithubProfileProperty =
            DependencyProperty.Register("NameGithubProfile", typeof(string), typeof(GithubConnection));


        public string AboutGithubProfile
        {
            get { return (string)GetValue(AboutGithubProfileProperty); }
            set { SetValue(AboutGithubProfileProperty, value); }
        }

        public static readonly DependencyProperty AboutGithubProfileProperty =
            DependencyProperty.Register("AboutGithubProfile", typeof(string), typeof(GithubConnection));

        public GithubConnection()
        {
            GithubProfilePhoto = null;
            InitializeComponent();
            Instance = this;
        }

        private void ButtonPress_Click(object sender, RoutedEventArgs e)
        {
            App.user.github = GithabNameAccount.Text.Trim();
            App.db.SaveChanges();
            PortfolioWindow.Instance.DataContext = new PortfolioWindowVM();
            Close();
        }


        public void GetLinkGithudProfileImage()
        {
            try
            {
                Bitmap loadedBitmap = null;
                using (var responseStream = System.Net.WebRequest.Create(UriGithubProfileImage).GetResponse().GetResponseStream())
                {
                    loadedBitmap = new Bitmap(responseStream);
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
            catch (Exception)
            {
                GithubProfilePhoto = null;
                NameGithubProfile = "Нет такого пользователя";
            }
        }

        public async Task GetInformationProfileGithubAsync(string NameUserGithub)
        {
            LoadComponetState(true);
            await ParsingDataInSite(NameUserGithub);
            LoadComponetState(false);
        }

        private async Task ParsingDataInSite(string NameUserGithub)
        {
            //Ссылка на Github пользователя
            NameProfile = $"https://github.com/{NameUserGithub}";
            //Картинка профиля пользователя
            UriGithubProfileImage = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(NameProfile)).QuerySelectorAll(".avatar-user").Select(m => m.Attributes["src"].Value).FirstOrDefault();
            //Имя обычного профиля
            NameBaseProfile = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(NameProfile)).QuerySelectorAll(".vcard-fullname").Select(m => m.Text()).FirstOrDefault() ?? "";
            //Имя корпоративного профиля
            NameCollaborationProfile = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(NameProfile)).QuerySelectorAll(".h2.lh-condensed").Select(m => m.Text()).FirstOrDefault() ?? "";
            //Имя пользователя обычного пользователя
            UsernameCollaborationProfile = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(NameProfile)).QuerySelectorAll(".vcard-username").Select(m => m.Text()).FirstOrDefault() ?? "";
            //О пользователе
            AboutProfileGithub = (await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(NameProfile)).QuerySelectorAll(".user-profile-bio").Select(m => m.Attributes["data-bio-text"].Value).FirstOrDefault() ?? "";

            VilidateDataRequest();
        }

        private void VilidateDataRequest()
        {
            if (NameBaseProfile.Replace(Environment.NewLine, "").Trim() != "") { NameGithubProfile = NameBaseProfile.Replace(Environment.NewLine, "").Trim(); }
            else if (NameCollaborationProfile != "") { NameGithubProfile = NameCollaborationProfile.Replace(Environment.NewLine, "").Trim(); }
            else { NameGithubProfile = UsernameCollaborationProfile.Replace(Environment.NewLine, "").Trim(); }

            AboutGithubProfile = AboutProfileGithub.Replace(Environment.NewLine, "").Trim();
        }

        private void LoadComponetState(bool IsLoad)
        {
            if (IsLoad)
            {
                ImageLoad.Visibility = System.Windows.Visibility.Visible;
                BorderGithubProfile.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ImageLoad.Visibility = System.Windows.Visibility.Collapsed;
                BorderGithubProfile.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private async void GithabNameAccount_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (GithabNameAccount == null) return;
            if (GithabNameAccount.Text == "")
            {
                BorderGithubProfile.Visibility = System.Windows.Visibility.Collapsed;
                return;
            }
            await GetInformationProfileGithubAsync(GithabNameAccount.Text);
            GetLinkGithudProfileImage();
        }
}
}
