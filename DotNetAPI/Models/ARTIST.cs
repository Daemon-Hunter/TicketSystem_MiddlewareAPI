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
    
    public partial class ARTIST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ARTIST()
        {
            this.ARTIST_REVIEW = new HashSet<ARTIST_REVIEW>();
            this.CHILD_EVENT = new HashSet<CHILD_EVENT>();
        }
    
        public int ARTIST_ID { get; set; }
        public string ARTIST_NAME { get; set; }
        public string ARTIST_TAGS { get; set; }
        public int SOCIAL_MEDIA_ID { get; set; }
        public string ARTIST_DESCRIPTION { get; set; }
        public int ARTIST_TYPE_ID { get; set; }
    
        public SOCIAL_MEDIA SOCIAL_MEDIA { get; set; }
        public ARTIST_TYPE ARTIST_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ARTIST_REVIEW> ARTIST_REVIEW { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CHILD_EVENT> CHILD_EVENT { get; set; }
    }
}
