//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DbElements
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhotoPreviews
    {
        public PhotoPreviews()
        {
            this.User = new HashSet<User>();
        }
    
        public System.Guid Guid { get; set; }
        public string Photo50 { get; set; }
        public string Photo100 { get; set; }
        public string Photo200 { get; set; }
        public string Photo400 { get; set; }
        public string PhotoMax { get; set; }
    
        public virtual ICollection<User> User { get; set; }
    }
}
