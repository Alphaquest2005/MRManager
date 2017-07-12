namespace DocumentDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class xcuda_ExporterMap : EntityTypeConfiguration<xcuda_Exporter>
    {
        public xcuda_ExporterMap()
        {                        
              this.HasKey(t => t.Traders_Id);        
              this.ToTable("xcuda_Exporter");
              this.Property(t => t.Exporter_name).HasColumnName("Exporter_name").IsUnicode(false);
              this.Property(t => t.Traders_Id).HasColumnName("Traders_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.Exporter_code).HasColumnName("Exporter_code").IsUnicode(false);
              this.HasRequired(t => t.xcuda_Traders).WithOptional(t => t.xcuda_Exporter);
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
