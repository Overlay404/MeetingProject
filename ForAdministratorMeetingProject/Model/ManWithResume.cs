//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ForAdministratorMeetingProject.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ManWithResume
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronomic { get; set; }
        public string github { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string telegram { get; set; }
        public string about { get; set; }
        public byte[] BackgroundImage { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
