using MeetingProject.SupportiveClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConnectionGithub.xaml
    /// </summary>
    public partial class ConnectionGithub : Page
    {
        #region DependencyProperty
        public ImageSource GithubProfilePhoto
        {
            get { return (ImageSource)GetValue(GithubProfilePhotoProperty); }
            set { SetValue(GithubProfilePhotoProperty, value); }
        }

        public static readonly DependencyProperty GithubProfilePhotoProperty =
            DependencyProperty.Register("GithubProfilePhoto", typeof(ImageSource), typeof(ConnectionGithub));


        public string NameGithubProfile
        {
            get { return (string)GetValue(NameGithubProfileProperty); }
            set { SetValue(NameGithubProfileProperty, value); }
        }

        public static readonly DependencyProperty NameGithubProfileProperty =
            DependencyProperty.Register("NameGithubProfile", typeof(string), typeof(ConnectionGithub));


        public string AboutGithubProfile
        {
            get { return (string)GetValue(AboutGithubProfileProperty); }
            set { SetValue(AboutGithubProfileProperty, value); }
        }

        public static readonly DependencyProperty AboutGithubProfileProperty =
            DependencyProperty.Register("AboutGithubProfile", typeof(string), typeof(ConnectionGithub));
        #endregion

        public ConnectionGithub()
        {
            InitializeComponent();
        }


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
            }
            catch (Exception ex)
            {
                GithubProfilePhoto = null;
                NameGithubProfile = "Нет такого пользователя";
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Анимация загрузки
        private void LoadComponetState(bool v)
        {
            if(v) ImageRefresh.Style = (Style)Resources["AnimationStart"];
            else ImageRefresh.Style = (Style)Resources["AnimationEnd"];
        }
        #endregion

        #region Получение объекта из GET request
        private async Task AnswerRequest(string NameUserGithub)
        {
            //Объект полученный из запроса
            var AccountGitHubObject = await RequestManager.Get<ParseAccountInformation>($"users/{NameUserGithub}");
            //Картинка пользователя
            ConvertProfileImage(AccountGitHubObject.avatar_url);
            //Username или login пользователя
            NameGithubProfile = AccountGitHubObject.name ?? AccountGitHubObject.login;
            //О пользователе
            AboutGithubProfile = AccountGitHubObject.bio ?? "";
        }
        #endregion

        private async void Image_MouseDownAsync(object sender, MouseButtonEventArgs e)
        {
            LoadComponetState(true);
            await AnswerRequest(UsernameGithubText.Text.Trim());
            LoadComponetState(false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
