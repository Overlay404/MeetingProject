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
            InitializeListBox();

            foreach(var element in ListBox.Items)
            {
                (element as WebBrowser).NavigateToString("<h1>Заголовок</h1>");
            }
        }

        private void InitializeListBox()
        {
            ListBox.Items.Add(new ListBoxSupportive() { NameHint = "Заголовок 1" });
            ListBox.Items.Add(new ListBoxSupportive() { NameHint = "Заголовок 2" });
            ListBox.Items.Add(new ListBoxSupportive() { NameHint = "Заголовок 3" });
            ListBox.Items.Add(new ListBoxSupportive() { NameHint = "Заголовок 4" });
            ListBox.Items.Add(new ListBoxSupportive() { NameHint = "Заголовок 5" });
        }
    }

    public class ListBoxSupportive
    {
        public string NameHint;
        public string Uri;
    }
}
