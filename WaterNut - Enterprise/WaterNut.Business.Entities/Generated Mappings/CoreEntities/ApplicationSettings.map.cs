namespace CoreEntities.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class ApplicationSettingsMap : EntityTypeConfiguration<ApplicationSettings>
    {
        public ApplicationSettingsMap()
        {                        
              this.HasKey(t => t.ApplicationSettingsId);        
              this.ToTable("ApplicationSettings");
              this.Property(t => t.ApplicationSettingsId).HasColumnName("ApplicationSettingsId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.Property(t => t.Description).HasColumnName("Description").IsUnicode(false);
              this.Property(t => t.MaxEntryLines).HasColumnName("MaxEntryLines");
              this.Property(t => t.SoftwareName).HasColumnName("SoftwareName").IsUnicode(false);
              this.Property(t => t.AllowCounterPoint).HasColumnName("AllowCounterPoint").IsUnicode(false);
              this.Property(t => t.GroupEX9).HasColumnName("GroupEX9");
              this.Property(t => t.InvoicePerEntry).HasColumnName("InvoicePerEntry");
              this.Property(t => t.AllowTariffCodes).HasColumnName("AllowTariffCodes").IsUnicode(false);
              this.Property(t => t.AllowWareHouse).HasColumnName("AllowWareHouse").IsUnicode(false);
              this.Property(t => t.AllowXBond).HasColumnName("AllowXBond").IsUnicode(false);
              this.Property(t => t.AllowAsycudaManager).HasColumnName("AllowAsycudaManager").IsUnicode(false);
              this.Property(t => t.AllowQuickBooks).HasColumnName("AllowQuickBooks").IsUnicode(false);
              this.Property(t => t.ItemDescriptionContainsAsycudaAttribute).HasColumnName("ItemDescriptionContainsAsycudaAttribute");
              this.Property(t => t.AllowExportToExcel).HasColumnName("AllowExportToExcel").IsUnicode(false);
              this.Property(t => t.AllowAutoWeightCalculation).HasColumnName("AllowAutoWeightCalculation").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowEntryPerIM7).HasColumnName("AllowEntryPerIM7").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowSalesToPI).HasColumnName("AllowSalesToPI").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowEffectiveAssessmentDate).HasColumnName("AllowEffectiveAssessmentDate").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowAutoFreightCalculation).HasColumnName("AllowAutoFreightCalculation").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowSubItems).HasColumnName("AllowSubItems").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowEntryDoNotAllocate).HasColumnName("AllowEntryDoNotAllocate").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowPreviousItems).HasColumnName("AllowPreviousItems").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowOversShort).HasColumnName("AllowOversShort").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowContainers).HasColumnName("AllowContainers").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowNonXEntries).HasColumnName("AllowNonXEntries").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowValidateTariffCodes).HasColumnName("AllowValidateTariffCodes").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AllowCleanBond).HasColumnName("AllowCleanBond").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.OrderEntriesBy).HasColumnName("OrderEntriesBy").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.OpeningStockDate).HasColumnName("OpeningStockDate");
              this.Property(t => t.AllowWeightEqualQuantity).HasColumnName("AllowWeightEqualQuantity").IsUnicode(false).HasMaxLength(50);
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
