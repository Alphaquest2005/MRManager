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
    
    internal partial class xcuda_Supplementary_unit_Mapping : EntityTypeConfiguration<xcuda_Supplementary_unit>
    {
        public xcuda_Supplementary_unit_Mapping()
        {                        
              this.HasKey(t => t.Supplementary_unit_Id);        
              this.ToTable("xcuda_Supplementary_unit");
              this.Property(t => t.Suppplementary_unit_quantity).HasColumnName("Suppplementary_unit_quantity").IsUnicode(false);
              this.Property(t => t.Supplementary_unit_Id).HasColumnName("Supplementary_unit_Id");
              this.Property(t => t.Tarification_Id).HasColumnName("Tarification_Id");
              this.Property(t => t.Suppplementary_unit_code).HasColumnName("Suppplementary_unit_code");
              this.Property(t => t.Suppplementary_unit_name).HasColumnName("Suppplementary_unit_name");
              this.HasOptional(t => t.xcuda_Tarification).WithMany(t => t.xcuda_Supplementary_unit).HasForeignKey(d => d.Tarification_Id);
         }
    }
}
