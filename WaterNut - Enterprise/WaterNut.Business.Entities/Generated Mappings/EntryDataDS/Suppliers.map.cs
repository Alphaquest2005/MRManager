namespace EntryDataDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class SuppliersMap : EntityTypeConfiguration<Suppliers>
    {
        public SuppliersMap()
        {                        
              this.HasKey(t => t.SupplierId);        
              this.ToTable("Suppliers");
              this.Property(t => t.SupplierId).HasColumnName("SupplierId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.Property(t => t.SupplierCode).HasColumnName("SupplierCode").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.SupplierName).HasColumnName("SupplierName").IsUnicode(false).HasMaxLength(255);
              this.Property(t => t.Street).HasColumnName("Street").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.City).HasColumnName("City").IsUnicode(false).HasMaxLength(19);
              this.Property(t => t.ZipCode).HasColumnName("ZipCode").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Country).HasColumnName("Country").IsUnicode(false).HasMaxLength(50);
              this.HasMany(t => t.EntryData).WithOptional(t => t.Suppliers).HasForeignKey(d => d.SupplierId);
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
