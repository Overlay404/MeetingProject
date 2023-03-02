using System.Text.RegularExpressions;
using System.Windows;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для ChangeContactInformation.xaml
    /// </summary>
    public partial class ChangeContactInformation : Window
    {
        public static ChangeContactInformation Instance;
        string NameComponent;

        public ChangeContactInformation(string Title, string nameComponent)
        {
            InitializeComponent();
            TitleComponent.Text = Title;
            NameComponent = nameComponent;
            Instance = this;
        }

        private void ButtonPress_Click(object sender, RoutedEventArgs e)
        {
            Regex phoneRegex = new Regex(@"(\+7|8|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)");
            Regex telegramRegex = new Regex(@"");
            Regex emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            switch (NameComponent)
            { 
                case "Phone":
                    if (ValueComponent.Text.Trim() == "" || !phoneRegex.IsMatch(ValueComponent.Text)) return;
                        App.user.number = ValueComponent.Text;
                    Close();
                    break;
                case "Telegram":
                    if (ValueComponent.Text.Trim() == "" || !telegramRegex.IsMatch(ValueComponent.Text)) return;
                        App.user.telegram = ValueComponent.Text;
                    Close();
                    break;
                case "Email":
                    if (ValueComponent.Text.Trim() == "" || !emailRegex.IsMatch(ValueComponent.Text)) return;
                        App.user.email = ValueComponent.Text;
                    Close();
                    break;
                default:
                    break;
            }
            new UserInformationWindowModelView().UpdateData();
        }
    }
}
