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
    
    public partial class Link
    {
        public Link()
        {
            this.MessageAttachment = new HashSet<MessageAttachment>();
        }
    
        public long Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string PreviewPage { get; set; }
    
        public virtual ICollection<MessageAttachment> MessageAttachment { get; set; }
    }
}
