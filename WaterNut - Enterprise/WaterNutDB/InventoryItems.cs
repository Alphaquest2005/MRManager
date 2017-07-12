//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WaterNutDB
{
    #pragma warning disable 1573
    using System;
    using System.Collections.Generic;
    
    public partial class InventoryItems
    {
        public InventoryItems()
        {
            this.EntryDataDetails = new HashSet<EntryDataDetails>();
            this.xcuda_HScode = new HashSet<xcuda_HScode>();
            this.xcuda_HScode1 = new HashSet<xcuda_HScode>();
        }
    
        public string ItemNumber { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string TariffCode { get; set; }
        public Nullable<System.DateTime> EntryTimeStamp { get; set; }
    
        public virtual ICollection<EntryDataDetails> EntryDataDetails { get; set; }
        public virtual TariffCodes TariffCodes { get; set; }
        public virtual ICollection<xcuda_HScode> xcuda_HScode { get; set; }
        public virtual ICollection<xcuda_HScode> xcuda_HScode1 { get; set; }
    }
}
