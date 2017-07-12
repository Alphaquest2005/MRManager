namespace OversShortQS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class OverShortSuggestedDocumentMap : EntityTypeConfiguration<OverShortSuggestedDocument>
    {
        public OverShortSuggestedDocumentMap()
        {                        
              this.HasKey(t => t.OversShortsId);        
              this.ToTable("OverShortSuggestedDocuments");
              this.Property(t => t.OversShortsId).HasColumnName("OversShortsId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.CNumber).HasColumnName("CNumber").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber").IsUnicode(false).HasMaxLength(19);
              this.HasRequired(t => t.OversShortEX).WithOptional(t => t.OverShortSuggestedDocuments);
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
