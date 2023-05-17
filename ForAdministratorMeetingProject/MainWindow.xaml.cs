using ForAdministratorMeetingProject.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ForAdministratorMeetingProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            MainFrame.Navigate(new ManWithResumeGrid());

            Company.Checked += (sender, e) => 
            {
                MainFrame.Navigate(new CompanyGrid());
            };
            ManWithResumeBtn.Checked += (sender, e) =>
            {
                MainFrame.Navigate(new ManWithResumeGrid());
            };
            BlockBtn.Click += (sender, e) =>
            {
                if(MainFrame.Content is ManWithResumeGrid)
                {
                    ManWithResumeGrid.Instance.BannedUser();
                }
                else
                {
                    CompanyGrid.Instance.BannedUser();
                }
            };

            UnblockBtn.Click += (sender, e) =>
            {
                if (MainFrame.Content is ManWithResumeGrid)
                {
                    ManWithResumeGrid.Instance.UnbannedUser();
                }
                else
                {
                    CompanyGrid.Instance.UnbannedUser();
                }
            };

            Closing += (sender, e) => App.db.SaveChanges();
        }
    }
}
