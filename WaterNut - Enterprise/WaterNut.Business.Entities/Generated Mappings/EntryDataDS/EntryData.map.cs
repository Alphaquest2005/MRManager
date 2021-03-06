﻿namespace EntryDataDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class EntryDataMap : EntityTypeConfiguration<EntryData>
    {
        public EntryDataMap()
        {                        
              this.HasKey(t => t.EntryDataId);        
              this.ToTable("EntryData");
              this.Property(t => t.EntryDataId).HasColumnName("EntryDataId").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.EntryDataDate).HasColumnName("EntryDataDate");
              this.Property(t => t.ImportedTotal).HasColumnName("ImportedTotal");
              this.Property(t => t.ImportedLines).HasColumnName("ImportedLines");
              this.Property(t => t.SupplierId).HasColumnName("SupplierId");
              this.Property(t => t.TotalFreight).HasColumnName("TotalFreight");
              this.Property(t => t.TotalInternalFreight).HasColumnName("TotalInternalFreight");
              this.Property(t => t.TotalWeight).HasColumnName("TotalWeight");
              this.HasOptional(t => t.Suppliers).WithMany(t => t.EntryData).HasForeignKey(d => d.SupplierId);
              this.HasMany(t => t.EntryDataDetails).WithRequired(t => t.EntryData);
              this.HasMany(t => t.AsycudaDocuments).WithRequired(t => t.EntryData);
              this.HasMany(t => t.AsycudaDocumentSets).WithRequired(t => t.EntryData);
              this.HasMany(t => t.ContainerEntryData).WithRequired(t => t.EntryData);
             // Tracking Properties
    			this.Ignore(t => t.TrackingState);
    			this.Ignore(t => t.ModifiedProperties);
    
    
             // IIdentifibleEntity
                this.Ignore(t => t.EntityId);
                this.Ignore(t => t.EntityName); 
    
                this.Ignore(t => t.EntityKey);
             // Nav Property Names
                  
    
    
              
    
         }
    }
}
