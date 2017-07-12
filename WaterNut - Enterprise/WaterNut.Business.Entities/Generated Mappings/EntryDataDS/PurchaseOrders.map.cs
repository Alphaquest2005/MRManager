namespace EntryDataDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class PurchaseOrdersMap : EntityTypeConfiguration<PurchaseOrders>
    {
        public PurchaseOrdersMap()
        {                        
              this.HasKey(t => t.EntryDataId);        
              this.ToTable("EntryData_PurchaseOrders");
              this.Property(t => t.EntryDataId).HasColumnName("EntryDataId").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.PONumber).HasColumnName("PONumber").IsRequired();
             // Nav Property Names
                  
    
    
              
    
         }
    }
}
