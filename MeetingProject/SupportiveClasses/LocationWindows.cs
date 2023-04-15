using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingProject.SupportiveClasses
{
    internal class LocationWindows
    {
        public static double TopWindow { get; set; }
        public static double LeftWindow { get; set; }

        public static void SaveLocationWindow(double top, double left)
        {
            TopWindow = top;
            LeftWindow = left;
        }
    }
}
