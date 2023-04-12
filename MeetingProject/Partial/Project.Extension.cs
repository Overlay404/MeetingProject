using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingProject.Model
{
    partial class Project
    {
        public string descriptionProject {
            get {
                var textCount = text.Length;

                if(description != null)
                {
                    return description;
                }

                if (textCount > 100)
                {
                    return description ?? text.Substring(0, 100);
                }
                else
                {
                    return description ?? text;
                }
            } 
        }

    }
}
