using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForCompanyMeetingProject.Model
{
    partial class ManWithResume
    {
        public string Fullname => $"{surname} {name} {patronomic}";

        public string JobTitleList => String.Join(", ", JobTitle.Select(j => j.name));
    }
}
