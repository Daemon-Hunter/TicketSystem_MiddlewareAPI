//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DotNetAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SOCIAL_MEDIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOCIAL_MEDIA()
        {
            this.ARTISTs = new HashSet<ARTIST>();
            this.PARENT_EVENT = new HashSet<PARENT_EVENT>();
            this.VENUEs = new HashSet<VENUE>();
        }
    
        public int SOCIAL_MEDIA_ID { get; set; }
        public byte[] IMAGE { get; set; }
        public string FACEBOOK { get; set; }
        public string TWITTER { get; set; }
        public string INSTAGRAM { get; set; }
        public string SOUNDCLOUD { get; set; }
        public string WEBSITE { get; set; }
        public string SPOTIFY { get; set; }
        public byte[] IMAGE2 { get; set; }
        public byte[] IMAGE3 { get; set; }
        public byte[] IMAGE4 { get; set; }
        public byte[] IMAGE5 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public 
            ICollection<ARTIST> ARTISTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<PARENT_EVENT> PARENT_EVENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<VENUE> VENUEs { get; set; }
    }
}
