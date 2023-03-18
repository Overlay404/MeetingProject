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

        string NameBaseProfile;

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
        }

        #region Из интренет-ссылки в картинку
        public void GetLinkGithudProfileImage(string uriImage)
        {
            try
            {
                System.Drawing.Bitmap loadedBitmap = null;
                using (var responseStream = WebRequest.Create(uriImage).GetResponse().GetResponseStream())
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
                GithubProfilePhoto = null;
                NameGithubProfile = "Нет такого пользователя";
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Анимирование загрузки
        public async Task GetInformationProfileGithubAsync(string NameUserGithub)
        {
            LoadComponetState(true);
            await AnswerRequest(NameUserGithub);
            LoadComponetState(false);
        }
        #endregion

        #region Получение объекта из GET request
        private async Task AnswerRequest(string NameUserGithub)
        {
            //Объект полученный из запроса
            var AccountGitHubObject = await Get<AccountGitHub>($"users/{NameUserGithub}");
            //Картинка пользователя
            UriGithubProfileImage = AccountGitHubObject.avatar_url;
            //Username или login пользователя
            NameGithubProfile = AccountGitHubObject.name ?? AccountGitHubObject.login;
            //О пользователе
            AboutGithubProfile = AccountGitHubObject.bio ?? "";
        }
        #endregion

        #region Анимация
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
        #endregion

        private async void ButtonPress_ClickAsync(object sender, RoutedEventArgs e)
        {
            await GetInformationProfileGithubAsync(GithabNameAccount.Text);
            GetLinkGithudProfileImage(UriGithubProfileImage);
        }

        #region
        public static async Task<T> Get<T>(string controller)
        {
            HttpResponseMessage response = null;
            T data = default;
            try{
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MeetingProject", "1.0"));
                var request = new HttpRequestMessage(HttpMethod.Get, "http://api.github.com/" + controller);
                response = await http.SendAsync(request);
                data = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return data;
        }
        #endregion
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
