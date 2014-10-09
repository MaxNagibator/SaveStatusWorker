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
    
    public partial class Audio
    {
        public Audio()
        {
            this.MessageAttachment = new HashSet<MessageAttachment>();
        }
    
        public long Id { get; set; }
        public Nullable<long> OwnerId { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Url { get; set; }
        public Nullable<long> LyricsId { get; set; }
        public Nullable<long> AlbumId { get; set; }
        public Nullable<int> GenreId { get; set; }
    
        public virtual AudioGenre AudioGenre { get; set; }
        public virtual ICollection<MessageAttachment> MessageAttachment { get; set; }
    }
}
