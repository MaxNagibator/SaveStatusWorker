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
    
    public partial class Album
    {
        public Album()
        {
            this.MessageAttachment = new HashSet<MessageAttachment>();
        }
    
        public long Id { get; set; }
        public Nullable<long> ThumbId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public int Size { get; set; }
    
        public virtual Photo Photo { get; set; }
        public virtual ICollection<MessageAttachment> MessageAttachment { get; set; }
    }
}