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
    
    public partial class xcuda_Goods_description
    {
        public string Country_of_origin_code { get; set; }
        public string Description_of_goods { get; set; }
        public string Commercial_Description { get; set; }
        public int Item_Id { get; set; }
    
        public virtual xcuda_Item xcuda_Item { get; set; }
    }
}
