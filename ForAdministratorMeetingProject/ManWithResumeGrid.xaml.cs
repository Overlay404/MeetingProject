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
    /// Логика взаимодействия для ManWithResumeGrid.xaml
    /// </summary>
    public partial class ManWithResumeGrid : Page
    {
        public static ManWithResumeGrid Instance { get; set; }

        public IEnumerable<ManWithResume> ManWithResumeDG
        {
            get { return (IEnumerable<ManWithResume>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("ManWithResumeDG", typeof(IEnumerable<ManWithResume>), typeof(ManWithResumeGrid));


        public ManWithResumeGrid()
        {
            ManWithResumeDG = App.db.ManWithResume.Local;
            InitializeComponent();
            Instance = this;
        }

        public void BannedUser()
        {
            foreach(var item in DGManWithResume.SelectedItems)
            {
                (item as ManWithResume).IsBanned = true;
            }

            DGManWithResume.Items.Refresh();
            App.db.SaveChanges();
        }

        public void UnbannedUser()
        {
            foreach (var item in DGManWithResume.SelectedItems) 
            {
                (item as ManWithResume).IsBanned = false;
            }

            DGManWithResume.Items.Refresh();
            App.db.SaveChanges();
        }
    }
}
