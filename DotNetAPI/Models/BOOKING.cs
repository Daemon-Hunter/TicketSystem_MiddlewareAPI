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
    
    public partial class BOOKING
    {
        public int BOOKING_ID { get; set; }
        public int TICKET_ID { get; set; }
        public int ORDER_ID { get; set; }
        public short BOOKING_QUANTITY { get; set; }
        public System.DateTime BOOKING_DATE_TIME { get; set; }
    
        public  TICKET TICKET { get; set; }
        public  ORDER ORDER { get; set; }
    }
}
