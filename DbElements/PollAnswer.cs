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
    
    public partial class PollAnswer
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public Nullable<int> Votes { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<long> PollId { get; set; }
    
        public virtual Poll Poll { get; set; }
    }
}