﻿namespace DocumentDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Customs_ProcedureMap : EntityTypeConfiguration<Customs_Procedure>
    {
        public Customs_ProcedureMap()
        {                        
              this.HasKey(t => t.Customs_ProcedureId);        
              this.ToTable("Customs_Procedure");
              this.Property(t => t.Document_TypeId).HasColumnName("Document_TypeId");
              this.Property(t => t.Customs_ProcedureId).HasColumnName("Customs_ProcedureId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.Property(t => t.Extended_customs_procedure).HasColumnName("Extended_customs_procedure").IsUnicode(false);
              this.Property(t => t.National_customs_procedure).HasColumnName("National_customs_procedure").IsUnicode(false);
              this.Property(t => t.IsDefault).HasColumnName("IsDefault");
              this.HasRequired(t => t.Document_Type).WithMany(t => t.Customs_Procedure).HasForeignKey(d => d.Document_TypeId);
              this.HasMany(t => t.AsycudaDocumentSets).WithOptional(t => t.Customs_Procedure).HasForeignKey(d => d.Customs_ProcedureId);
              this.HasMany(t => t.xcuda_ASYCUDA_ExtendedProperties).WithOptional(t => t.Customs_Procedure).HasForeignKey(d => d.Customs_ProcedureId);
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
