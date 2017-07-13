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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Infrastructure;
    
    internal partial class xcuda_PreviousItem_Mapping : EntityTypeConfiguration<xcuda_PreviousItem>
    {
        public xcuda_PreviousItem_Mapping()
        {                        
              this.HasKey(t => t.PreviousItem_Id);        
              this.ToTable("xcuda_PreviousItem");
              this.Property(t => t.Packages_number).HasColumnName("Packages_number").IsUnicode(false);
              this.Property(t => t.Previous_Packages_number).HasColumnName("Previous_Packages_number").IsUnicode(false);
              this.Property(t => t.Hs_code).HasColumnName("Hs_code").IsUnicode(false);
              this.Property(t => t.Commodity_code).HasColumnName("Commodity_code").IsUnicode(false);
              this.Property(t => t.Previous_item_number).HasColumnName("Previous_item_number").IsUnicode(false);
              this.Property(t => t.Goods_origin).HasColumnName("Goods_origin").IsUnicode(false);
              this.Property(t => t.Net_weight).HasColumnName("Net_weight");
              this.Property(t => t.Prev_net_weight).HasColumnName("Prev_net_weight");
              this.Property(t => t.Prev_reg_ser).HasColumnName("Prev_reg_ser").IsUnicode(false);
              this.Property(t => t.Prev_reg_nbr).HasColumnName("Prev_reg_nbr").IsUnicode(false);
              this.Property(t => t.Prev_reg_dat).HasColumnName("Prev_reg_dat").IsUnicode(false);
              this.Property(t => t.Prev_reg_cuo).HasColumnName("Prev_reg_cuo").IsUnicode(false);
              this.Property(t => t.Suplementary_Quantity).HasColumnName("Suplementary_Quantity");
              this.Property(t => t.Preveious_suplementary_quantity).HasColumnName("Preveious_suplementary_quantity");
              this.Property(t => t.Current_value).HasColumnName("Current_value");
              this.Property(t => t.Previous_value).HasColumnName("Previous_value");
              this.Property(t => t.Current_item_number).HasColumnName("Current_item_number").IsUnicode(false);
              this.Property(t => t.PreviousItem_Id).HasColumnName("PreviousItem_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.ASYCUDA_Id).HasColumnName("ASYCUDA_Id");
              this.HasOptional(t => t.xcuda_ASYCUDA).WithMany(t => t.xcuda_PreviousItem).HasForeignKey(d => d.ASYCUDA_Id);
              this.HasRequired(t => t.xcuda_Item).WithOptional(t => t.xcuda_PreviousItem);
         }
    }
}