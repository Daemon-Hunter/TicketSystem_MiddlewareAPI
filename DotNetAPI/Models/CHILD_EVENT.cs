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
    
    public partial class CHILD_EVENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHILD_EVENT()
        {
            this.CONTRACTS = new HashSet<CONTRACT>();
            this.TICKETs = new HashSet<TICKET>();
        }
    
        public int CHILD_EVENT_ID { get; set; }
        public Nullable<int> VENUE_ID { get; set; }
        public int PARENT_EVENT_ID { get; set; }
        public string CHILD_EVENT_NAME { get; set; }
        public string CHILD_EVENT_DESCRIPTION { get; set; }
        public Nullable<System.DateTime> START_DATE_TIME { get; set; }
        public Nullable<System.DateTime> END_DATE_TIME { get; set; }
        public bool CHILD_EVENT_CANCELED { get; set; }
    
        public  VENUE VENUE { get; set; }
        public  PARENT_EVENT PARENT_EVENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<CONTRACT> CONTRACTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<TICKET> TICKETs { get; set; }
    }
}
