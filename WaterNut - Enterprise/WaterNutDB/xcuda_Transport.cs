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
    
    public partial class xcuda_Transport
    {
        public xcuda_Transport()
        {
            this.xcuda_Border_office = new HashSet<xcuda_Border_office>();
            this.xcuda_Delivery_terms = new HashSet<xcuda_Delivery_terms>();
            this.xcuda_Means_of_transport = new HashSet<xcuda_Means_of_transport>();
            this.xcuda_Place_of_loading = new HashSet<xcuda_Place_of_loading>();
        }
    
        public bool Container_flag { get; set; }
        public bool Single_waybill_flag { get; set; }
        public int Transport_Id { get; set; }
        public Nullable<int> ASYCUDA_Id { get; set; }
        public string Location_of_goods { get; set; }
    
        public virtual xcuda_ASYCUDA xcuda_ASYCUDA { get; set; }
        public virtual ICollection<xcuda_Border_office> xcuda_Border_office { get; set; }
        public virtual ICollection<xcuda_Delivery_terms> xcuda_Delivery_terms { get; set; }
        public virtual ICollection<xcuda_Means_of_transport> xcuda_Means_of_transport { get; set; }
        public virtual ICollection<xcuda_Place_of_loading> xcuda_Place_of_loading { get; set; }
    }
}
