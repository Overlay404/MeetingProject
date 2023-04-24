using MeetingProject.Model;
using MeetingProject.ModelView;
using MeetingProject.View.UserControls;
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
    /// Логика взаимодействия для EditingPorfolioWondow.xaml
    /// </summary>
    public partial class EditingPorfolioWindow : Window
    {


        public ManWithResume ManWithResume
        {
            get { return (ManWithResume)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("ManWithResume", typeof(ManWithResume), typeof(EditingPorfolioWindow));




        public IEnumerable<Education> Education
        {
            get { return (IEnumerable<Education>)GetValue(EducationProperty); }
            set { SetValue(EducationProperty, value); }
        }

        public static readonly DependencyProperty EducationProperty =
            DependencyProperty.Register("Education", typeof(IEnumerable<Education>), typeof(EditingPorfolioWindow));




        public IEnumerable<Experience> Experience
        {
            get { return (IEnumerable<Experience>)GetValue(ExperienceProperty); }
            set { SetValue(ExperienceProperty, value); }
        }

        public static readonly DependencyProperty ExperienceProperty =
            DependencyProperty.Register("Experience", typeof(IEnumerable<Experience>), typeof(EditingPorfolioWindow));





        public EditingPorfolioWindow()
        {
            RefreshData();

            SaveChangesBtn.MouseDown += (sender, e) =>
            {
                RefreshAndSaveData();
                MessageControl.Instance.StartAnimation();
            };
            AddingEducation.MouseDown += (sender, e) => { AddingEducationInDataBase(); };
            DeletingEducation.MouseDown += (sender, e) => { DeletingEducationInDataBase(); };
            AddingExperience.MouseDown += (sender, e) => { AddingExperienceInDataBase(); };
            DeletingExperience.MouseDown += (sender, e) => { DeletingExperienceInDataBase(); };
        }

        private void RefreshData()
        {
            Education = App.user.Education;
            Experience = App.user.Experience;
            ManWithResume = App.user;
            InitializeComponent();
            EducationList.ItemsSource = App.user.Education;
            ExperienceList.ItemsSource = App.user.Experience;
        }

        private void AddingEducationInDataBase() 
        {
            App.db.Education.Add(new Model.Education { ManWithResume = new List<ManWithResume>(){ App.user }});
            RefreshData();
        }

        private void DeletingEducationInDataBase() 
        { 
            
        }

        private void AddingExperienceInDataBase() 
        {
            App.db.Experience.Add(new Model.Experience { ManWithResume = new List<ManWithResume>() { App.user }});
            RefreshData();
        }
        
        private void DeletingExperienceInDataBase() 
        { 
        
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите покинуть окно редактирования", "Окно редактирования", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            RefreshAndSaveData();
        }

        private void RefreshAndSaveData()
        {
            App.db.SaveChanges();
            App.user = ManWithResume;
            PortfolioWindow.Instance.DataContext = new PortfolioWindowVM();
        }
    }
}
