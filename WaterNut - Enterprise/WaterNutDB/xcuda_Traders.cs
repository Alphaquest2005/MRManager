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
    
    public partial class xcuda_Traders
    {
        public int Traders_Id { get; set; }
    
        public virtual xcuda_ASYCUDA xcuda_ASYCUDA { get; set; }
        public virtual xcuda_Consignee xcuda_Consignee { get; set; }
        public virtual xcuda_Exporter xcuda_Exporter { get; set; }
        public virtual xcuda_Traders_Financial xcuda_Traders_Financial { get; set; }
    }
}