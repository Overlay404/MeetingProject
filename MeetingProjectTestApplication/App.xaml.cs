using MeetingProjectTestApplication.Model;
using System.Linq;
using System.Windows;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MeetingProjectEntities db = new MeetingProjectEntities();

        public static ManWithResume user = db.ManWithResume.FirstOrDefault();
    }
}
