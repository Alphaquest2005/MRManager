//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OverShortsQS
{
    using System;
    using System.Collections.Generic;
    
    public partial class OversShortEX : OversShort
    {
        public OversShortEX()
        {
            this.OverShortDetailsEXes = new HashSet<OverShortDetailsEX>();
        }
    
        public Nullable<double> ReceivedValue { get; set; }
        public Nullable<double> InvoiceValue { get; set; }
    
        public virtual ICollection<OverShortDetailsEX> OverShortDetailsEXes { get; set; }
        public virtual OverShortSuggestedDocument OverShortSuggestedDocuments { get; set; }
    }
}
