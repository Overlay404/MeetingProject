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
    
    public partial class Company
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string LegalForm { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string telegram { get; set; }
        public string about { get; set; }
        public byte[] BackgroundImage { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string Password { get; set; }
        public string BannedText { get => IsBanned == true ? "Заблокирован" : "Не заблокирован" ; }
        public Nullable<bool> IsBanned { get; set; }
    }
}