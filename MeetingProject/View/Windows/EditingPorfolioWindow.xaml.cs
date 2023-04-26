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
            InitializeComponent();
            RefreshData();

            SaveChangesBtn.MouseDown += (sender, e) =>
            {
                RefreshAndSaveData();
            };
            AddingEducation.MouseDown += (sender, e) => { AddingEducationInDataBase(); };
            AddingExperience.MouseDown += (sender, e) => { AddingExperienceInDataBase(); };
        }

        private void RefreshData()
        {
            Education = App.user.Education;
            Experience = App.user.Experience;
            ManWithResume = App.user;
            EducationList.Items.Refresh();
            ExperienceList.Items.Refresh();
        }

        private void AddingEducationInDataBase() 
        {
            App.user.Education.Add(new Model.Education { ManWithResume = new List<ManWithResume>(){ App.user }});
            RefreshData();
        }

        private void AddingExperienceInDataBase()
        {
            App.user.Experience.Add(new Model.Experience { ManWithResume = new List<ManWithResume>() { App.user } });
            RefreshData();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.user.Education.Remove((sender as TextBlock).DataContext as Education);
            RefreshData();
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e) 
        {
            App.user.Experience.Remove((sender as TextBlock).DataContext as Experience);
            RefreshData();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите покинуть окно редактирования, ваши данные могут не сохраниться", "Окно редактирования", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            RefreshAndSaveData();
        }

        private void RefreshAndSaveData()
        {
            var objectError = App.user.Experience.Where(e => e.EndDate <= e.StartDate);
            if (objectError != null)
            {
                MessageBox.Show("Дата окончания работы не может быть меньше даты начала работы");
                return;
            }

            App.db.SaveChanges();
            MessageControl.Instance.StartAnimation();
            App.user = ManWithResume;
            PortfolioWindow.Instance.DataContext = new PortfolioWindowVM();
        }
    }
}
