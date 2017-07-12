namespace AllocationDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class xcuda_ItemMap : EntityTypeConfiguration<xcuda_Item>
    {
        public xcuda_ItemMap()
        {                        
              this.HasKey(t => t.Item_Id);        
              this.ToTable("xcuda_Item");
              this.Property(t => t.Amount_deducted_from_licence).HasColumnName("Amount_deducted_from_licence").IsUnicode(false);
              this.Property(t => t.Quantity_deducted_from_licence).HasColumnName("Quantity_deducted_from_licence").IsUnicode(false);
              this.Property(t => t.Item_Id).HasColumnName("Item_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.Property(t => t.ASYCUDA_Id).HasColumnName("ASYCUDA_Id");
              this.Property(t => t.Licence_number).HasColumnName("Licence_number");
              this.Property(t => t.EntryDataDetailsId).HasColumnName("EntryDataDetailsId");
              this.Property(t => t.LineNumber).HasColumnName("LineNumber");
              this.Property(t => t.SalesFactor).HasColumnName("SalesFactor");
              this.Property(t => t.IsAssessed).HasColumnName("IsAssessed");
              this.Property(t => t.DPQtyAllocated).HasColumnName("DPQtyAllocated");
              this.Property(t => t.DFQtyAllocated).HasColumnName("DFQtyAllocated");
              this.Property(t => t.EntryTimeStamp).HasColumnName("EntryTimeStamp");
              this.Property(t => t.AttributeOnlyAllocation).HasColumnName("AttributeOnlyAllocation");
              this.Property(t => t.DoNotAllocate).HasColumnName("DoNotAllocate");
              this.Property(t => t.DoNotEX).HasColumnName("DoNotEX");
              this.Property(t => t.Free_text_1).HasColumnName("Free_text_1");
              this.Property(t => t.Free_text_2).HasColumnName("Free_text_2");
              this.Property(t => t.ImportComplete).HasColumnName("ImportComplete");
              this.Property(t => t.WarehouseError).HasColumnName("WarehouseError").IsUnicode(false).HasMaxLength(50);
              this.HasRequired(t => t.AsycudaDocument).WithMany(t => t.xcuda_Item).HasForeignKey(d => d.ASYCUDA_Id);
              this.HasMany(t => t.AsycudaSalesAllocations).WithOptional(t => t.PreviousDocumentItem).HasForeignKey(d => d.PreviousItem_Id);
              this.HasOptional(t => t.xcuda_PreviousItem).WithRequired(t => t.xcuda_Item);
              this.HasMany(t => t.SubItems).WithRequired(t => t.xcuda_Item);
              this.HasOptional(t => t.xcuda_Goods_description).WithRequired(t => t.xcuda_Item);
              this.HasOptional(t => t.xcuda_Tarification).WithRequired(t => t.xcuda_Item);
              this.HasMany(t => t.xcuda_Taxation).WithOptional(t => t.xcuda_Item).HasForeignKey(d => d.Item_Id);
              this.HasOptional(t => t.xcuda_Valuation_item).WithRequired(t => t.xcuda_Item);
              this.HasMany(t => t.xBondAllocations).WithRequired(t => t.xcuda_Item);
              this.HasMany(t => t.EX9AsycudaSalesAllocations).WithRequired(t => t.PreviousDocumentItem);
              this.HasMany(t => t.EntryPreviousItems).WithRequired(t => t.xcuda_Item);
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
