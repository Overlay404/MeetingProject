//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeetingProjectTestApplication.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PictureProject
    {
        public int id { get; set; }
        public byte[] codeImage { get; set; }
        public Nullable<int> ProjectId { get; set; }
    
        public virtual Project Project { get; set; }
    }
}
