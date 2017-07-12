namespace AllocationDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class AsycudaDocumentMap : EntityTypeConfiguration<AsycudaDocument>
    {
        public AsycudaDocumentMap()
        {                        
              this.HasKey(t => t.ASYCUDA_Id);        
              this.ToTable("AsycudaDocument");
              this.Property(t => t.ASYCUDA_Id).HasColumnName("ASYCUDA_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.id).HasColumnName("id").IsUnicode(false);
              this.Property(t => t.CNumber).HasColumnName("CNumber").IsUnicode(false);
              this.Property(t => t.RegistrationDate).HasColumnName("RegistrationDate");
              this.Property(t => t.IsManuallyAssessed).HasColumnName("IsManuallyAssessed");
              this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
              this.Property(t => t.EffectiveRegistrationDate).HasColumnName("EffectiveRegistrationDate");
              this.Property(t => t.AsycudaDocumentSetId).HasColumnName("AsycudaDocumentSetId");
              this.Property(t => t.DoNotAllocate).HasColumnName("DoNotAllocate");
              this.Property(t => t.AutoUpdate).HasColumnName("AutoUpdate");
              this.Property(t => t.BLNumber).HasColumnName("BLNumber").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Description).HasColumnName("Description");
              this.Property(t => t.Type_of_declaration).HasColumnName("Type_of_declaration").IsUnicode(false);
              this.Property(t => t.Declaration_gen_procedure_code).HasColumnName("Declaration_gen_procedure_code").IsUnicode(false);
              this.Property(t => t.Extended_customs_procedure).HasColumnName("Extended_customs_procedure").IsUnicode(false);
              this.Property(t => t.Customs_ProcedureId).HasColumnName("Customs_ProcedureId");
              this.Property(t => t.Country_first_destination).HasColumnName("Country_first_destination").IsUnicode(false);
              this.Property(t => t.Currency_code).HasColumnName("Currency_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Currency_rate).HasColumnName("Currency_rate");
              this.Property(t => t.Manifest_reference_number).HasColumnName("Manifest_reference_number").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Customs_clearance_office_code).HasColumnName("Customs_clearance_office_code").IsUnicode(false);
              this.Property(t => t.DocumentType).HasColumnName("DocumentType").IsUnicode(false);
              this.Property(t => t.Document_TypeId).HasColumnName("Document_TypeId");
              this.Property(t => t.Lines).HasColumnName("Lines");
              this.Property(t => t.ImportComplete).HasColumnName("ImportComplete");
              this.Property(t => t.Cancelled).HasColumnName("Cancelled");
              this.Property(t => t.TotalCIF).HasColumnName("TotalCIF");
              this.Property(t => t.TotalGrossWeight).HasColumnName("TotalGrossWeight");
              this.Property(t => t.AssessmentDate).HasColumnName("AssessmentDate");
              this.HasMany(t => t.xcuda_Item).WithRequired(t => t.AsycudaDocument);
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
