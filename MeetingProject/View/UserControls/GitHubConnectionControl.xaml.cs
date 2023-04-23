using AngleSharp;
using AngleSharp.Dom;
using MeetingProject.SupportiveClasses;
using MeetingProject.View.Pages;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeetingProject.View.UserControls
{
    /// <summary>
    /// Контролл для отображения информации о пользователе
    /// </summary>
    public partial class GitHubConnectionControl : UserControl
    {
        #region DependencyProperty
        public static GitHubConnectionControl Instance { get; private set; }

        public ImageSource GithubProfilePhoto
        {
            get { return (ImageSource)GetValue(GithubProfilePhotoProperty); }
            set { SetValue(GithubProfilePhotoProperty, value); }
        }

        public static readonly DependencyProperty GithubProfilePhotoProperty =
            DependencyProperty.Register("GithubProfilePhoto", typeof(ImageSource), typeof(GitHubConnectionControl));


        public string NameGithubProfile
        {
            get { return (string)GetValue(NameGithubProfileProperty); }
            set { SetValue(NameGithubProfileProperty, value); }
        }

        public static readonly DependencyProperty NameGithubProfileProperty =
            DependencyProperty.Register("NameGithubProfile", typeof(string), typeof(GitHubConnectionControl));


        public string AboutGithubProfile
        {
            get { return (string)GetValue(AboutGithubProfileProperty); }
            set { SetValue(AboutGithubProfileProperty, value); }
        }

        public static readonly DependencyProperty AboutGithubProfileProperty =
            DependencyProperty.Register("AboutGithubProfile", typeof(string), typeof(GitHubConnectionControl));

        public GitHubConnectionControl()
        {
            GithubProfilePhoto = null;
            InitializeComponent();
            Instance = this;
        }
#endregion

        #region Из интренет-ссылки в картинку
        public void ConvertProfileImage(string uriImage)
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
                MessageToUser.Text = "Нет такого пользователя";
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Анимация
        private void LoadComponetState(bool v)
        {
            if (v)
            {
                MyStoryboard.Stop();
                BorderGithubProfile.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                BorderGithubProfile.Visibility = System.Windows.Visibility.Collapsed;
                TimeSpan ts = TimeSpan.FromMilliseconds(500);
                Prope11.Duration = ts;
                MyStoryboard.Begin();
            }
        }
        #endregion

        #region Получение объекта из GET request
        public async Task AnswerRequest(string NameUserGithub)
        {
            MessageToUser.Text = "";
            //Объект полученный из запроса
            var AccountGitHubObject = await RequestManager.Get<ParseAccountInformation>($"https://api.github.com/users/{NameUserGithub}") ??
                new ParseAccountInformation
                {
                    avatar_url = "",
                    name = "",
                    bio = "",
                    login = ""
                };
            //Картинка пользователя
            var imageUri = AccountGitHubObject.avatar_url;
            //Username или login пользователя
            NameGithubProfile = AccountGitHubObject.name ?? AccountGitHubObject.login;
            //О пользователе
            AboutGithubProfile = AccountGitHubObject.bio;
            //Конвертация изображения
            ConvertProfileImage(imageUri);
        }
        #endregion

        #region Отображение информации
        public async Task DataDisplay(string username)
        {
            LoadComponetState(false);
            await AnswerRequest(username);
            LoadComponetState(true);
        }
        #endregion

        #region События
        private async void Image_MouseDownAsync(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameGithubText.Text.Trim()))
            {
                BorderGithubProfile.Visibility = System.Windows.Visibility.Collapsed;
                return;
            }

            await DataDisplay(UsernameGithubText.Text.Trim());
        }
        #endregion
    }
}

