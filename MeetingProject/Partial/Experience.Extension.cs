using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingProject.Model
{
    partial class Experience
    {

        public string EndDateFormat => EndDate?.ToString("D")  ?? "Работает до сих пор";
    }
}
