namespace OversShortQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class OverShortDetailMap : EntityTypeConfiguration<OverShortDetail>
    {
        public OverShortDetailMap()
        {                        
              this.HasKey(t => t.OverShortDetailId);        
              this.ToTable("OverShortDetails");
              this.Property(t => t.OverShortDetailId).HasColumnName("OverShortDetailId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.Property(t => t.OversShortsId).HasColumnName("OversShortsId");
              this.Property(t => t.ReceivedQty).HasColumnName("ReceivedQty");
              this.Property(t => t.ItemNumber).HasColumnName("ItemNumber").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.ItemDescription).HasColumnName("ItemDescription").IsUnicode(false);
              this.Property(t => t.Cost).HasColumnName("Cost");
              this.Property(t => t.InvoiceQty).HasColumnName("InvoiceQty");
              this.Property(t => t.Status).HasColumnName("Status").IsUnicode(false);
              this.HasMany(t => t.OverShortDetailAllocations).WithRequired(t => t.OverShortDetail);
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
