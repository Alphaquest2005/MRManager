namespace InventoryQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class InventoryItemsExMap : EntityTypeConfiguration<InventoryItemsEx>
    {
        public InventoryItemsExMap()
        {                        
              this.HasKey(t => t.ItemNumber);        
              this.ToTable("InventoryItemsEx");
              this.Property(t => t.ItemNumber).HasColumnName("ItemNumber").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Description).HasColumnName("Description").IsRequired();
              this.Property(t => t.Category).HasColumnName("Category").HasMaxLength(60);
              this.Property(t => t.TariffCode).HasColumnName("TariffCode").IsUnicode(false).HasMaxLength(8);
              this.Property(t => t.EntryTimeStamp).HasColumnName("EntryTimeStamp");
              this.HasOptional(t => t.TariffCodes).WithMany(t => t.InventoryItemsEx).HasForeignKey(d => d.TariffCode);
              this.HasMany(t => t.EntryDataDetailsEx).WithRequired(t => t.InventoryItemsEx);
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
