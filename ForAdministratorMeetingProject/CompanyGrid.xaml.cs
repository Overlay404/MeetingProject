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
    /// Логика взаимодействия для CompanyGrid.xaml
    /// </summary>
    public partial class CompanyGrid : Page
    {
        public static CompanyGrid Instance { get; set; }

        public IEnumerable<Company> CompanyDG
        {
            get { return (IEnumerable<Company>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(IEnumerable<Company>), typeof(CompanyGrid));



        public CompanyGrid()
        {
            CompanyDG = App.db.Company.Local;
            InitializeComponent();
            Instance = this;
        }

        public void BannedUser()
        {
            foreach (var item in DGCompany.SelectedItems)
            {
                (item as Company).IsBanned = true;
            }

            DGCompany.Items.Refresh();
            App.db.SaveChanges();
        }

        public void UnbannedUser()
        {
            foreach (var item in DGCompany.SelectedItems)
            {
                (item as Company).IsBanned = false;
            }

            DGCompany.Items.Refresh();
            App.db.SaveChanges();
        }
    }
}
