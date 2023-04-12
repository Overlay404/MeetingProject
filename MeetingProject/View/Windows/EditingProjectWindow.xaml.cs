using MeetingProject.Model;
using MeetingProject.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeetingProject.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditingProjectWindow.xaml
    /// </summary>
    public partial class EditingProjectWindow : Window
    {
        public EditingProjectWindow(Project project = null)
        {
            InitializeComponent();
            FramePageEditing.Navigate(new EditingProjectPage(project));

            SettingBtn.MouseDown += (sender, e) => { FramePageEditing.Navigate(new SettingProjectPage(project)); };
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EditingProjectPage.Instance.AcceptChanges();
            App.db.SaveChanges();
            new PortfolioWindow().Show();
        }
    }
}
