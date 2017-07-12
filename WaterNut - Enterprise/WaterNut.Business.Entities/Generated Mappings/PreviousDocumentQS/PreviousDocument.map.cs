namespace PreviousDocumentQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class PreviousDocumentMap : EntityTypeConfiguration<PreviousDocument>
    {
        public PreviousDocumentMap()
        {                        
              this.HasKey(t => t.ASYCUDA_Id);        
              this.ToTable("PreviousDocument");
              this.Property(t => t.ASYCUDA_Id).HasColumnName("ASYCUDA_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.id).HasColumnName("id").IsUnicode(false);
              this.Property(t => t.CNumber).HasColumnName("CNumber").IsUnicode(false);
              this.Property(t => t.RegistrationDate).HasColumnName("RegistrationDate");
              this.Property(t => t.IsManuallyAssessed).HasColumnName("IsManuallyAssessed");
              this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
              this.Property(t => t.EffectiveRegistrationDate).HasColumnName("EffectiveRegistrationDate");
              this.Property(t => t.TotalValue).HasColumnName("TotalValue");
              this.Property(t => t.AllocatedValue).HasColumnName("AllocatedValue");
              this.Property(t => t.PiValue).HasColumnName("PiValue");
              this.Property(t => t.AsycudaDocumentSetId).HasColumnName("AsycudaDocumentSetId");
              this.Property(t => t.DoNotAllocate).HasColumnName("DoNotAllocate");
              this.Property(t => t.Description).HasColumnName("Description");
              this.Property(t => t.BLNumber).HasColumnName("BLNumber").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Lines).HasColumnName("Lines");
              this.Property(t => t.DocumentType).HasColumnName("DocumentType").IsUnicode(false);
              this.HasMany(t => t.PreviousDocumentItems).WithRequired(t => t.PreviousDocument);
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
