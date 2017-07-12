namespace InventoryDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class TariffSupUnitLkpMap : EntityTypeConfiguration<TariffSupUnitLkp>
    {
        public TariffSupUnitLkpMap()
        {                        
              this.HasKey(t => t.Id);        
              this.ToTable("TariffSupUnitLkps");
              this.Property(t => t.TariffCategoryCode).HasColumnName("TariffCategoryCode").IsRequired().IsUnicode(false).HasMaxLength(8);
              this.Property(t => t.SuppUnitCode2).HasColumnName("SuppUnitCode2").HasMaxLength(50);
              this.Property(t => t.SuppUnitName2).HasColumnName("SuppUnitName2").HasMaxLength(50);
              this.Property(t => t.SuppQty).HasColumnName("SuppQty");
              this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.HasRequired(t => t.TariffCategory).WithMany(t => t.TariffSupUnitLkps).HasForeignKey(d => d.TariffCategoryCode);
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
