using ForAdministratorMeetingProject.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ForAdministratorMeetingProject
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MeetingProjectEntities db = new MeetingProjectEntities();

        public App()
        {
            db.Company.Load();
            db.ManWithResume.Load();
        }
    }
}
