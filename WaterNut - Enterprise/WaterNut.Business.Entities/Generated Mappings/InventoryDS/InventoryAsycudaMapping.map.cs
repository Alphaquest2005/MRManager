namespace InventoryDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class InventoryAsycudaMappingMap : EntityTypeConfiguration<InventoryAsycudaMapping>
    {
        public InventoryAsycudaMappingMap()
        {                        
              this.HasKey(t => t.InventoryAsycudaMappingId);        
              this.ToTable("InventoryAsycudaMapping");
              this.Property(t => t.ItemNumber).HasColumnName("ItemNumber").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Item_Id).HasColumnName("Item_Id");
              this.Property(t => t.InventoryAsycudaMappingId).HasColumnName("InventoryAsycudaMappingId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.HasRequired(t => t.InventoryItem).WithMany(t => t.InventoryAsycudaMappings).HasForeignKey(d => d.ItemNumber);
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
