namespace InventoryQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class TariffCodesMap : EntityTypeConfiguration<TariffCodes>
    {
        public TariffCodesMap()
        {                        
              this.HasKey(t => t.TariffCode);        
              this.ToTable("TariffCodes");
              this.Property(t => t.TariffCode).HasColumnName("TariffCode").IsRequired().IsUnicode(false).HasMaxLength(8);
              this.Property(t => t.Description).HasColumnName("Description").IsUnicode(false).HasMaxLength(999);
              this.Property(t => t.RateofDuty).HasColumnName("RateofDuty").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.EnvironmentalLevy).HasColumnName("EnvironmentalLevy").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.CustomsServiceCharge).HasColumnName("CustomsServiceCharge").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.ExciseTax).HasColumnName("ExciseTax").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.VatRate).HasColumnName("VatRate").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.PetrolTax).HasColumnName("PetrolTax").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Units).HasColumnName("Units").HasMaxLength(999);
              this.Property(t => t.SiteRev3).HasColumnName("SiteRev3").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.TariffCategoryCode).HasColumnName("TariffCategoryCode").IsUnicode(false).HasMaxLength(8);
              this.Property(t => t.LicenseRequired).HasColumnName("LicenseRequired");
              this.Property(t => t.Invalid).HasColumnName("Invalid");
              this.HasOptional(t => t.TariffCategory).WithMany(t => t.TariffCodes).HasForeignKey(d => d.TariffCategoryCode);
              this.HasMany(t => t.InventoryItemsEx).WithOptional(t => t.TariffCodes).HasForeignKey(d => d.TariffCode);
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
