﻿namespace AllocationDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class SalesMap : EntityTypeConfiguration<Sales>
    {
        public SalesMap()
        {                        
              this.HasKey(t => t.EntryDataId);        
              this.ToTable("EntryData_Sales");
              this.Property(t => t.EntryDataId).HasColumnName("EntryDataId").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.INVNumber).HasColumnName("INVNumber").IsRequired();
              this.Property(t => t.TaxAmount).HasColumnName("TaxAmount");
              this.Property(t => t.CustomerName).HasColumnName("CustomerName").IsUnicode(false);
              this.HasMany(t => t.EntryDataDetails).WithRequired(t => t.Sales);
             // Nav Property Names
                  
    
    
              
    
         }
    }
}
