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
    
    internal partial class xcuda_Assessment_notice_Mapping : EntityTypeConfiguration<xcuda_Assessment_notice>
    {
        public xcuda_Assessment_notice_Mapping()
        {                        
              this.HasKey(t => t.Assessment_notice_Id);        
              this.ToTable("xcuda_Assessment_notice");
              this.Property(t => t.Assessment_notice_Id).HasColumnName("Assessment_notice_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.ASYCUDA_Id).HasColumnName("ASYCUDA_Id");
              this.HasOptional(t => t.xcuda_ASYCUDA).WithMany(t => t.xcuda_Assessment_notice).HasForeignKey(d => d.ASYCUDA_Id);
         }
    }
}