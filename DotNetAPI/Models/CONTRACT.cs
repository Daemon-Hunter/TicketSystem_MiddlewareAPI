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
    
    public partial class CONTRACT
    {
        public int ARTIST_ID { get; set; }
        public int CHILD_EVENT_ID { get; set; }
        public string COLUMN1 { get; set; }
    
        public ARTIST ARTIST { get; set; }
        public  CHILD_EVENT CHILD_EVENT { get; set; }
    }
}