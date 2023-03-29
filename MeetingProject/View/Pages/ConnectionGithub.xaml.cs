using MeetingProject.ModelView;
using MeetingProject.SupportiveClasses;
using MeetingProject.View.UserControls;
using MeetingProject.View.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
            StartWindow.Instance.Autorization.IsChecked = false;
            StartWindow.Instance.Registration.IsChecked = false;

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
                MessageToUser.Text = "Нет такого пользователя";
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Анимация загрузки
        private void LoadComponetState(bool v)
        {
            if (v)
            {
                MyStoryboard.Stop();
            }
            else
            {
                TimeSpan ts = TimeSpan.FromMilliseconds(500);
                Prope11.Duration = ts;
                MyStoryboard.Begin();
            }
        }
        #endregion

        #region Получение объекта из GET request
        private async Task AnswerRequest(string NameUserGithub)
        {
            MessageToUser.Text = "";
            //Объект полученный из запроса
            var AccountGitHubObject = await RequestManager.Get<ParseAccountInformation>($"users/{NameUserGithub}") ??
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

        private async void Image_MouseDownAsync(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameGithubText.Text.Trim())) return;

            LoadComponetState(false);
            await AnswerRequest(UsernameGithubText.Text.Trim());
            LoadComponetState(true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Проверка что пользователь пуст
            if (App.user == null)
            {
                //Проверка есть ли такой GitHub в базе данных
                var objectUser = App.db.ManWithResume.Where(m => m.github.Equals(UsernameGithubText.Text.Trim())).FirstOrDefault();
                if (objectUser == null)
                {
                    //Создание нового пользователя с таким GitHub
                    if (MessageBox.Show("Нет такого пользователя создать нового?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        new StartWindow().Show();
                        StartWindow.Instance.Registration.IsChecked = true;
                        GitHubConnectionControl.Instance.GithabNameAccount.Text = UsernameGithubText.Text.Trim();
                        return;
                    }
                }
                //Вход в аккаунт с таким GitHub
                App.user = objectUser;
                new PortfolioWindow().Show();
                PortfolioWindow.Instance.DataContext = new PortfolioWindowVM();
                StartWindow.Instance.Close();
                return;
            }


            //Редактирование github авторизаваного пользователя

            //App.user.github = UsernameGithubText.Text.Trim();
            //App.db.SaveChanges();
            //PortfolioWindow.Instance.DataContext = new PortfolioWindowVM();
        }
    }
}
