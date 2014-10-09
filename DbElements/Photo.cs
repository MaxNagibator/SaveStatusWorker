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
    
    public partial class Photo
    {
        public Photo()
        {
            this.Album = new HashSet<Album>();
            this.MessageAttachment = new HashSet<MessageAttachment>();
        }
    
        public long Id { get; set; }
        public Nullable<long> OwnerId { get; set; }
        public Nullable<long> AlbumId { get; set; }
        public string Photo75 { get; set; }
        public string Photo130 { get; set; }
        public string Photo604 { get; set; }
        public string Photo807 { get; set; }
        public string Photo1280 { get; set; }
        public string Photo2560 { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public string Text { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string AccessKey { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<long> PostId { get; set; }
        public Nullable<long> PlacerId { get; set; }
        public Nullable<System.DateTime> TagCreated { get; set; }
        public Nullable<long> TagId { get; set; }
        public Nullable<bool> CanComment { get; set; }
        public string PhotoSrc { get; set; }
        public string PhotoHash { get; set; }
    
        public virtual ICollection<Album> Album { get; set; }
        public virtual ICollection<MessageAttachment> MessageAttachment { get; set; }
    }
}