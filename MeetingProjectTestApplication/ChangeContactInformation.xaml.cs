using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;
using MeetingProject.SupportiveClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HttpMethod = System.Net.Http.HttpMethod;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для ChangeContactInformation.xaml
    /// </summary>
    public partial class ChangeContactInformation : System.Windows.Window
    {
        public static ChangeContactInformation Instance;

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
            UbdateDataG();
        }

        public async void UbdateDataG()
        {

            var asd= Get<AccountGitHub>("users/overlay404");
        }

        private void ButtonPress_Click(object sender, RoutedEventArgs e)
        {

        }

        public void GetLinkGithudProfileImage()
        {
            try
            {
                System.Drawing.Bitmap loadedBitmap = null;
                using (var responseStream = System.Net.WebRequest.Create(UriGithubProfileImage).GetResponse().GetResponseStream())
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

            var AccountGitHubObject = await Get<List<AccountGitHub>>("users/Overlay404");
            //Ссылка на Github пользователя
            //NameProfile = $"https://github.com/{NameUserGithub}";
            ////Картинка профиля пользователя
            //UriGithubProfileImage = AccountGitHubObject.AvatarUrl;
            ////Имя обычного профиля
            //NameBaseProfile = AccountGitHubObject.Name ?? "";
            ////Имя корпоративного профиля
            //NameCollaborationProfile = AccountGitHubObject.Login;
            ////Имя пользователя обычного пользователя
            //UsernameCollaborationProfile = AccountGitHubObject.Login;
            ////О пользователе
            //AboutProfileGithub = AccountGitHubObject.Bio;

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

        private async void ValueComponent_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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

        public static async Task<T> Get<T>(string controller)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MeetingProject", "1.0"));
            var request = new HttpRequestMessage(HttpMethod.Get, "http://api.github.com/" + controller);
            var response = await http.SendAsync(request);
            MessageBox.Show(response.Content.ReadAsStringAsync().Result);
            var data = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return data;
        }
    }
}



public class AccountGitHub
{
    public string login { get; set; }
    public int id { get; set; }
    public string node_id { get; set; }
    public string avatar_url { get; set; }
    public string gravatar_id { get; set; }
    public string url { get; set; }
    public string html_url { get; set; }
    public string followers_url { get; set; }
    public string following_url { get; set; }
    public string gists_url { get; set; }
    public string starred_url { get; set; }
    public string subscriptions_url { get; set; }
    public string organizations_url { get; set; }
    public string repos_url { get; set; }
    public string events_url { get; set; }
    public string received_events_url { get; set; }
    public string type { get; set; }
    public string site_admin { get; set; }
    public string name { get; set; }
    public string company { get; set; }
    public string blog { get; set; }
    public string location { get; set; }
    public string email { get; set; }
    public string hireable { get; set; }
    public string bio { get; set; }
    public string twitter_username { get; set; }
    public int public_repos { get; set; }
    public int public_gists { get; set; }
    public int followers { get; set; }
    public int following { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
}
