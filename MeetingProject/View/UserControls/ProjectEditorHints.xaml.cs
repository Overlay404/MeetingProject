using System.Linq;
using System.Windows.Controls;

namespace MeetingProject.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ProjectEditorHints.xaml
    /// </summary>
    public partial class ProjectEditorHints : UserControl
    {
        public ProjectEditorHints()
        {
            InitializeComponent();
            Header1.NavigateToString("<head><meta http-equiv='Content-Type' content='text/html;charset=utf-8'></head><body><h1>Заголовок</h1></body></html>");
            Header2.NavigateToString("<head><meta http-equiv='Content-Type' content='text/html;charset=utf-8'></head><body><h2>Заголовок</h2s></body></html>");
            Header3.NavigateToString("<head><meta http-equiv='Content-Type' content='text/html;charset=utf-8'></head><body><h3>Заголовок</h3></body></html>");
            Header4.NavigateToString("<head><meta http-equiv='Content-Type' content='text/html;charset=utf-8'></head><body><h4>Заголовок</h4></body></html>");
        }
    }

    public class ListBoxSupportive
    {
        public string NameHint;
        public string Uri;
    }
}
