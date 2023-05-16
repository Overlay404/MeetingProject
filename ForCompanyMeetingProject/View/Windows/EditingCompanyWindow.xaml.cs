using ForCompanyMeetingProject.Model;
using ForCompanyMeetingProject.ModelView;
using ForCompanyMeetingProject.View.Pages;
using System.Windows;

namespace ForCompanyMeetingProject.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditingCompanyWindow.xaml
    /// </summary>
    public partial class EditingCompanyWindow : Window
    {

        public Company CompanyBin
        {
            get { return (Company)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("CompanyBin", typeof(Company), typeof(EditingCompanyWindow));



        public EditingCompanyWindow()
        {
            CompanyBin = App.user;
            InitializeComponent();

            SaveChangesBtn.MouseDown += (sender, e) =>
            {
                RefreshAndSaveData();
            };
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void RefreshAndSaveData()
        {
            App.db.SaveChanges();
            App.user = ContentConrtol.Content as Company;
            CompanyPage.Instance.DataContext = new CompanyWindowVM();
            Close();
        }
    }
}
