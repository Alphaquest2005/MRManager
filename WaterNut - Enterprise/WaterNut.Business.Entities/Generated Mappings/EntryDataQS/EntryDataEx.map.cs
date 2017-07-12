namespace EntryDataQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class EntryDataExMap : EntityTypeConfiguration<EntryDataEx>
    {
        public EntryDataExMap()
        {                        
              this.HasKey(t => t.InvoiceNo);        
              this.ToTable("EntryDataEx");
              this.Property(t => t.Type).HasColumnName("Type").IsUnicode(false).HasMaxLength(5);
              this.Property(t => t.DutyFreePaid).HasColumnName("DutyFreePaid").IsRequired().IsUnicode(false).HasMaxLength(9);
              this.Property(t => t.Total).HasColumnName("Total");
              this.Property(t => t.InvoiceDate).HasColumnName("InvoiceDate");
              this.Property(t => t.InvoiceNo).HasColumnName("InvoiceNo").IsRequired().IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.ImportedTotal).HasColumnName("ImportedTotal");
              this.Property(t => t.ImportedLines).HasColumnName("ImportedLines");
              this.Property(t => t.TotalLines).HasColumnName("TotalLines");
              this.HasMany(t => t.AsycudaDocumentSets).WithRequired(t => t.EntryDataEx);
              this.HasMany(t => t.AsycudaDocuments).WithRequired(t => t.EntryDataEx);
              this.HasMany(t => t.EntryDataDetailsExs).WithRequired(t => t.EntryDataEx);
              this.HasMany(t => t.ContainerEntryDatas).WithRequired(t => t.EntryDataEx);
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
