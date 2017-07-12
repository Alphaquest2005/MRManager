﻿namespace CoreEntities.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class LicenceSummaryMap : EntityTypeConfiguration<LicenceSummary>
    {
        public LicenceSummaryMap()
        {                        
              this.HasKey(t => t.RowNumber);        
              this.ToTable("LicenceSummary");
              this.Property(t => t.TariffCode).HasColumnName("TariffCode").IsUnicode(false).HasMaxLength(8);
              this.Property(t => t.Quantity).HasColumnName("Quantity");
              this.Property(t => t.Total).HasColumnName("Total");
              this.Property(t => t.TariffCodeDescription).HasColumnName("TariffCodeDescription").IsUnicode(false).HasMaxLength(999);
              this.Property(t => t.AsycudaDocumentSetId).HasColumnName("AsycudaDocumentSetId");
              this.Property(t => t.RowNumber).HasColumnName("RowNumber").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.HasRequired(t => t.AsycudaDocumentSetEx).WithMany(t => t.LicenceSummary).HasForeignKey(d => d.AsycudaDocumentSetId);
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
