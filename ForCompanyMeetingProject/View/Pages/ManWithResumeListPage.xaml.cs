﻿using ForCompanyMeetingProject.Model;
using ForCompanyMeetingProject.View.Windows;
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

namespace ForCompanyMeetingProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManWithResumeListPage.xaml
    /// </summary>
    public partial class ManWithResumeListPage : Page
    {
        public IEnumerable<ManWithResume> ManWithResumes
        {
            get { return (IEnumerable<ManWithResume>)GetValue(ManWithResumesProperty); }
            set { SetValue(ManWithResumesProperty, value); }
        }

        public static readonly DependencyProperty ManWithResumesProperty =
            DependencyProperty.Register("ManWithResumes", typeof(IEnumerable<ManWithResume>), typeof(ManWithResumeListPage));



        public ManWithResumeListPage()
        {
            ManWithResumes = App.db.ManWithResume.Local;
            InitializeComponent();


            HintText.MouseDown += (sender, e) => SearchBox.Focus();
            SearchBox.GotFocus += (sender, e) => HintText.Visibility = Visibility.Collapsed;
            UserBtn.MouseDown += (sender, e ) => MainWindow.Instance.MainFrame.Navigate(new CompanyPage());
            SearchBtn.MouseDown += (sender, e) =>
            {
                ManWithResumes = App.db.ManWithResume.Local.Where(m => (m.surname?.ToLower().Trim().StartsWith(SearchBox.Text.ToLower().Trim())) ?? true);
            };
            ListManWithResumes.MouseDoubleClick += (sender, e) =>
            {
                ManWithResume man = ListManWithResumes.SelectedItem as ManWithResume;
                new PortfolioWindow(man).Show();
            };
            SearchBox.LostFocus += (sender, e) => 
            {
                SearchBox.Text = "";
                HintText.Visibility = Visibility.Visible;
            };
        }
    }
}
