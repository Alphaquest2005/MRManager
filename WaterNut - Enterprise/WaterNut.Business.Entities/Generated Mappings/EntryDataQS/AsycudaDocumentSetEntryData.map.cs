﻿namespace EntryDataQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class AsycudaDocumentSetEntryDataMap : EntityTypeConfiguration<AsycudaDocumentSetEntryData>
    {
        public AsycudaDocumentSetEntryDataMap()
        {                        
              this.HasKey(t => t.Id);        
              this.ToTable("AsycudaDocumentSetEntryData");
              this.Property(t => t.AsycudaDocumentSetId).HasColumnName("AsycudaDocumentSetId");
              this.Property(t => t.EntryDataId).HasColumnName("EntryDataId").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.HasRequired(t => t.EntryDataEx).WithMany(t => t.AsycudaDocumentSets).HasForeignKey(d => d.EntryDataId);
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
