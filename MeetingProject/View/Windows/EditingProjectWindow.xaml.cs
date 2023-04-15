﻿using MeetingProject.Model;
using MeetingProject.View.Pages;
using static MeetingProject.SupportiveClasses.LocationWindows;
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
        public static EditingProjectWindow Instance;

        public EditingProjectWindow(Project project = null)
        {
            InitializeComponent();
            FramePageEditing.Navigate(new EditingProjectPage(project));
            Instance = this;

            SettingBtn.MouseDown += (sender, e) => { FramePageEditing.Navigate(new SettingProjectPage(project)); };
            ExitBtn.MouseDown += (sender, e) =>
            {
                ClosingWindow(false);
            };
            HelpBtn.MouseEnter += (sender, e) => {  };
        }

        private void ClosingWindow(bool IsClosing)
        {

            EditingProjectPage.Instance.AcceptChanges();
            App.db.SaveChanges();
            PortfolioWindow.Instance.Visibility = Visibility.Visible;
            if(!IsClosing) Close();
            PortfolioWindow.Instance.FrameDisplayingContent.Navigate(new ProjectPage());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClosingWindow(true);
        }
    }
}
