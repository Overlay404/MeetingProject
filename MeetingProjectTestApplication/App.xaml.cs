﻿using MeetingProjectTestApplication.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MeetingProjectEntities db = new MeetingProjectEntities();

        public static ManWithResume user = db.ManWithResume.FirstOrDefault();
    }
}
