namespace OversShortQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class OverShortDetailAllocationMap : EntityTypeConfiguration<OverShortDetailAllocation>
    {
        public OverShortDetailAllocationMap()
        {                        
              this.HasKey(t => t.OverShortAllocationId);        
              this.ToTable("OverShortDetailAllocations");
              this.Property(t => t.OverShortDetailId).HasColumnName("OverShortDetailId");
              this.Property(t => t.Item_Id).HasColumnName("Item_Id");
              this.Property(t => t.QtyAllocated).HasColumnName("QtyAllocated");
              this.Property(t => t.Status).HasColumnName("Status").IsUnicode(false);
              this.Property(t => t.OverShortAllocationId).HasColumnName("OverShortAllocationId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.HasRequired(t => t.OverShortDetail).WithMany(t => t.OverShortDetailAllocations).HasForeignKey(d => d.OverShortDetailId);
              this.HasOptional(t => t.EX).WithRequired(t => t.OverShortDetailAllocation);
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
