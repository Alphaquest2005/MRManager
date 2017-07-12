namespace OversShortQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class OverShortAllocationsEXMap : EntityTypeConfiguration<OverShortAllocationsEX>
    {
        public OverShortAllocationsEXMap()
        {                        
              this.HasKey(t => t.OverShortAllocationId);        
              this.ToTable("OverShortAllocationsEX");
              this.Property(t => t.OverShortDetailId).HasColumnName("OverShortDetailId");
              this.Property(t => t.OversShortsId).HasColumnName("OversShortsId");
              this.Property(t => t.ReceivedQty).HasColumnName("ReceivedQty");
              this.Property(t => t.ItemNumber).HasColumnName("ItemNumber").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.ItemDescription).HasColumnName("ItemDescription").IsUnicode(false);
              this.Property(t => t.Cost).HasColumnName("Cost");
              this.Property(t => t.InvoiceQty).HasColumnName("InvoiceQty");
              this.Property(t => t.InvoiceNo).HasColumnName("InvoiceNo").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.InvoiceDate).HasColumnName("InvoiceDate");
              this.Property(t => t.CNumber).HasColumnName("CNumber").IsUnicode(false);
              this.Property(t => t.RegistrationDate).HasColumnName("RegistrationDate").IsUnicode(false);
              this.Property(t => t.OverShortAllocationId).HasColumnName("OverShortAllocationId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.Duration).HasColumnName("Duration");
              this.Property(t => t.InvoiceMonth).HasColumnName("InvoiceMonth");
              this.Property(t => t.AsycudaMonth).HasColumnName("AsycudaMonth");
              this.Property(t => t.AllocatedValue).HasColumnName("AllocatedValue");
              this.Property(t => t.ReceivedValue).HasColumnName("ReceivedValue");
              this.Property(t => t.InvoiceValue).HasColumnName("InvoiceValue");
              this.Property(t => t.LineNumber).HasColumnName("LineNumber");
              this.Property(t => t.PiQuantity).HasColumnName("PiQuantity");
              this.Property(t => t.Item_Id).HasColumnName("Item_Id");
              this.Property(t => t.OverShortDetailStatus).HasColumnName("OverShortDetailStatus").IsUnicode(false);
              this.Property(t => t.QtyAllocated).HasColumnName("QtyAllocated");
              this.Property(t => t.AllocationStatus).HasColumnName("AllocationStatus").IsUnicode(false);
              this.HasRequired(t => t.OverShortDetailAllocation).WithOptional(t => t.EX);
              this.HasRequired(t => t.OverShortDetailsEX).WithMany(t => t.OverShortAllocationsEXes).HasForeignKey(d => d.OverShortDetailId);
              this.HasRequired(t => t.AsycudaDocumentItem).WithMany(t => t.OverShortAllocationsEXes).HasForeignKey(d => d.Item_Id);
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
