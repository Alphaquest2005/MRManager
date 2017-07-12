namespace EntryDataQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class ContainerExMap : EntityTypeConfiguration<ContainerEx>
    {
        public ContainerExMap()
        {                        
              this.HasKey(t => t.Container_Id);        
              this.ToTable("ContainerEx");
              this.Property(t => t.Container_identity).HasColumnName("Container_identity").IsUnicode(false);
              this.Property(t => t.Container_type).HasColumnName("Container_type").IsUnicode(false).HasMaxLength(10);
              this.Property(t => t.Empty_full_indicator).HasColumnName("Empty_full_indicator").IsUnicode(false);
              this.Property(t => t.Gross_weight).HasColumnName("Gross_weight");
              this.Property(t => t.Goods_description).HasColumnName("Goods_description").IsUnicode(false);
              this.Property(t => t.Packages_number).HasColumnName("Packages_number").IsUnicode(false);
              this.Property(t => t.Packages_type).HasColumnName("Packages_type").IsUnicode(false).HasMaxLength(10);
              this.Property(t => t.Packages_weight).HasColumnName("Packages_weight");
              this.Property(t => t.Container_Id).HasColumnName("Container_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.TotalValue).HasColumnName("TotalValue");
              this.Property(t => t.ShipDate).HasColumnName("ShipDate");
              this.Property(t => t.DeliveryDate).HasColumnName("DeliveryDate");
              this.Property(t => t.Seal).HasColumnName("Seal").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.AsycudaDocumentSetId).HasColumnName("AsycudaDocumentSetId");
              this.Property(t => t.EntryDataId).HasColumnName("EntryDataId").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.PackageDescription).HasColumnName("PackageDescription").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.ContainerTypeDescription).HasColumnName("ContainerTypeDescription").IsUnicode(false).HasMaxLength(50);
              this.HasMany(t => t.ContainerEntryDatas).WithRequired(t => t.ContainerEx);
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
