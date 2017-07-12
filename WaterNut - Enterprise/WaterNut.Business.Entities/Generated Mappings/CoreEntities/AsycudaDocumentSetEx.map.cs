namespace CoreEntities.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class AsycudaDocumentSetExMap : EntityTypeConfiguration<AsycudaDocumentSetEx>
    {
        public AsycudaDocumentSetExMap()
        {                        
              this.HasKey(t => t.AsycudaDocumentSetId);        
              this.ToTable("AsycudaDocumentSetEx");
              this.Property(t => t.AsycudaDocumentSetId).HasColumnName("AsycudaDocumentSetId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.Declarant_Reference_Number).HasColumnName("Declarant_Reference_Number").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Exchange_Rate).HasColumnName("Exchange_Rate");
              this.Property(t => t.Customs_ProcedureId).HasColumnName("Customs_ProcedureId");
              this.Property(t => t.Country_of_origin_code).HasColumnName("Country_of_origin_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Currency_Code).HasColumnName("Currency_Code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Document_TypeId).HasColumnName("Document_TypeId");
              this.Property(t => t.Description).HasColumnName("Description");
              this.Property(t => t.Manifest_Number).HasColumnName("Manifest_Number").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.BLNumber).HasColumnName("BLNumber").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.EntryTimeStamp).HasColumnName("EntryTimeStamp");
              this.Property(t => t.StartingFileCount).HasColumnName("StartingFileCount");
              this.Property(t => t.DocumentsCount).HasColumnName("DocumentsCount");
              this.Property(t => t.ApportionMethod).HasColumnName("ApportionMethod");
              this.Property(t => t.TotalCIF).HasColumnName("TotalCIF");
              this.Property(t => t.TotalGrossWeight).HasColumnName("TotalGrossWeight");
            this.Property(t => t.TotalFreight).HasColumnName("TotalFreight");
              this.HasMany(t => t.AsycudaDocuments).WithOptional(t => t.AsycudaDocumentSetEx).HasForeignKey(d => d.AsycudaDocumentSetId);
              this.HasMany(t => t.LicenceSummary).WithRequired(t => t.AsycudaDocumentSetEx);
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
