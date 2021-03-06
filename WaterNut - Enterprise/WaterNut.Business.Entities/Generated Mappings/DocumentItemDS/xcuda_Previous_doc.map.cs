﻿namespace DocumentItemDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class xcuda_Previous_docMap : EntityTypeConfiguration<xcuda_Previous_doc>
    {
        public xcuda_Previous_docMap()
        {                        
              this.HasKey(t => t.Item_Id);        
              this.ToTable("xcuda_Previous_doc");
              this.Property(t => t.Summary_declaration).HasColumnName("Summary_declaration").IsUnicode(false);
              this.Property(t => t.Item_Id).HasColumnName("Item_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.HasRequired(t => t.xcuda_Item).WithOptional(t => t.xcuda_Previous_doc);
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
