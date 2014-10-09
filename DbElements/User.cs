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
    
    public partial class User
    {
        public User()
        {
            this.Message = new HashSet<Message>();
            this.Message1 = new HashSet<Message>();
        }
    
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> SexId { get; set; }
        public string BirthDate { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> PhotoPreviewsId { get; set; }
        public Nullable<bool> IsOnline { get; set; }
        public string Domain { get; set; }
        public Nullable<int> IsHasMobile { get; set; }
        public string ModiblePhone { get; set; }
        public string HomePhone { get; set; }
        public Nullable<int> ConnectionsId { get; set; }
        public Nullable<bool> Site { get; set; }
        public Nullable<int> EducationId { get; set; }
        public Nullable<bool> IsCanPost { get; set; }
        public Nullable<bool> IsCanSeeAllPosts { get; set; }
        public Nullable<bool> IsCanSeeAudio { get; set; }
        public Nullable<bool> IsCanWritePrivateMessage { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> LastSeen { get; set; }
        public Nullable<int> CommonCount { get; set; }
        public Nullable<int> RelationTypeId { get; set; }
        public Nullable<int> BanInfoId { get; set; }
        public Nullable<int> IsDeactivated { get; set; }
        public string DeactiveReason { get; set; }
        public string NickName { get; set; }
        public Nullable<int> Timezone { get; set; }
        public Nullable<long> Language { get; set; }
        public Nullable<bool> IsOnlineMobile { get; set; }
        public Nullable<long> OnlineApp { get; set; }
        public Nullable<long> RelationPartnerId { get; set; }
        public Nullable<int> StandInLifeId { get; set; }
        public string Interests { get; set; }
        public string Music { get; set; }
        public string Activities { get; set; }
        public string Movies { get; set; }
        public string Tv { get; set; }
        public string Books { get; set; }
        public string Games { get; set; }
        public string About { get; set; }
        public string Quotes { get; set; }
        public Nullable<bool> InvitedBy { get; set; }
        public Nullable<int> MaidenName { get; set; }
        public Nullable<int> BirthdayVisibilityId { get; set; }
        public int HomeTown { get; set; }
        public Nullable<int> ChangeNameRequestId { get; set; }
        public Nullable<System.Guid> PreviewsGuid { get; set; }
    
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<Message> Message1 { get; set; }
        public virtual PhotoPreviews PhotoPreviews { get; set; }
    }
}
