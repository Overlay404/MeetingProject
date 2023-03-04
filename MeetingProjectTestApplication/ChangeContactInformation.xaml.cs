using AngleSharp;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для ChangeContactInformation.xaml
    /// </summary>
    public partial class ChangeContactInformation : Window
    {
        public static ChangeContactInformation Instance;

        public ChangeContactInformation()
        {
            InitializeComponent();
            Instance = this;
        }

        private async void ButtonPress_Click(object sender, RoutedEventArgs e)
        {
            await GetLinkGithudProfileImageAsync();
        }

        public async Task GetLinkGithudProfileImageAsync()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://github.com/Overlay404";
            var document = await BrowsingContext.New(config).OpenAsync(address);
            var cellSelector = ".avatar-user";
            var cells = document.QuerySelectorAll(cellSelector);
            var titles = cells.Select(m => m.Attributes["src"].Value);
            MessageBox.Show(string.Join(" ", titles));
        }
    }
}
