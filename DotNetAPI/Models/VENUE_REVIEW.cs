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
    
    public partial class VENUE_REVIEW
    {
        public int VENUE_ID { get; set; }
        public int CUSTOMER_ID { get; set; }
        public decimal VENUE_REVIEW_RATING { get; set; }
        public string VENUE_REVIEW_BODY { get; set; }
        public System.DateTime VENUE_REVIEW_DATE_TIME { get; set; }
        public bool VENUE_REVIEW_VERIFIED { get; set; }
    
        public CUSTOMER CUSTOMER { get; set; }
        public VENUE VENUE { get; set; }
    }
}
