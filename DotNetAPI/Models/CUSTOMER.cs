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
    
    public partial class CUSTOMER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUSTOMER()
        {
            this.ARTIST_REVIEW = new HashSet<ARTIST_REVIEW>();
            this.EVENT_REVIEW = new HashSet<EVENT_REVIEW>();
            this.ORDERS = new HashSet<ORDER>();
            this.VENUE_REVIEW = new HashSet<VENUE_REVIEW>();
        }
    
        public int CUSTOMER_ID { get; set; }
        public string CUSTOMER_FIRST_NAME { get; set; }
        public string CUSTOMER_LAST_NAME { get; set; }
        public string CUSTOMER_EMAIL { get; set; }
        public string CUSTOMER_ADDRESS { get; set; }
        public string CUSTOMER_POSTCODE { get; set; }
        public string CUSTOMER_PASSWORD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public 
            ICollection<ARTIST_REVIEW> ARTIST_REVIEW { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<EVENT_REVIEW> EVENT_REVIEW { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<ORDER> ORDERS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<VENUE_REVIEW> VENUE_REVIEW { get; set; }
    }
}
