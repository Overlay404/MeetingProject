﻿using ForCompanyMeetingProject.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ForCompanyMeetingProject
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MeetingProjectEntities db = new MeetingProjectEntities();

        public static Company user = db.Company.FirstOrDefault();
        public static ManWithResume man;

        public App()
        {
            db.ManWithResume.Load();
            db.Project.Load();
            db.PictureProject.Load();
            db.Company.Load();
            db.Education.Load();
            db.Experience.Load();
            db.JobTitle.Load();
        }
    }
}
