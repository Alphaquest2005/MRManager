//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntryDataQS
{
    using System;
    using System.Collections.Generic;
    
    public partial class InventoryItemsEx
    {
        public InventoryItemsEx()
        {
            this.EntryDataDetailsExs = new HashSet<EntryDataDetailsEx>();
        }
    
        public string ItemNumber { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string TariffCode { get; set; }
        public Nullable<System.DateTime> EntryTimeStamp { get; set; }
    
        public virtual ICollection<EntryDataDetailsEx> EntryDataDetailsExs { get; set; }
    }
}