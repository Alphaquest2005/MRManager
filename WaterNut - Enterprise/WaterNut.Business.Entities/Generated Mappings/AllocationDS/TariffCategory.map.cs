namespace AllocationDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class TariffCategoryMap : EntityTypeConfiguration<TariffCategory>
    {
        public TariffCategoryMap()
        {                        
              this.HasKey(t => t.TariffCategoryCode);        
              this.ToTable("TariffCategory");
              this.Property(t => t.TariffCategoryCode).HasColumnName("TariffCategoryCode").IsRequired().IsUnicode(false).HasMaxLength(8);
              this.Property(t => t.Description).HasColumnName("Description").IsUnicode(false).HasMaxLength(999);
              this.Property(t => t.ParentTariffCategoryCode).HasColumnName("ParentTariffCategoryCode").IsUnicode(false).HasMaxLength(5);
              this.Property(t => t.LicenseRequired).HasColumnName("LicenseRequired");
              this.HasMany(t => t.TariffSupUnitLkps).WithRequired(t => t.TariffCategory);
              this.HasMany(t => t.TariffCodes).WithOptional(t => t.TariffCategory).HasForeignKey(d => d.TariffCategoryCode);
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
