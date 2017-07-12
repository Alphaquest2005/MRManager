namespace DocumentItemDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class xcuda_Attached_documentsMap : EntityTypeConfiguration<xcuda_Attached_documents>
    {
        public xcuda_Attached_documentsMap()
        {                        
              this.HasKey(t => t.Attached_documents_Id);        
              this.ToTable("xcuda_Attached_documents");
              this.Property(t => t.Attached_document_date).HasColumnName("Attached_document_date");
              this.Property(t => t.Attached_documents_Id).HasColumnName("Attached_documents_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.Property(t => t.Item_Id).HasColumnName("Item_Id");
              this.Property(t => t.Attached_document_code).HasColumnName("Attached_document_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Attached_document_name).HasColumnName("Attached_document_name").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Attached_document_reference).HasColumnName("Attached_document_reference").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Attached_document_from_rule).HasColumnName("Attached_document_from_rule");
              this.HasOptional(t => t.xcuda_Item).WithMany(t => t.xcuda_Attached_documents).HasForeignKey(d => d.Item_Id);
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
