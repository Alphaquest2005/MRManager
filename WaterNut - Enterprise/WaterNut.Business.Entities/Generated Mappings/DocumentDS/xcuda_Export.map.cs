namespace DocumentDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class xcuda_ExportMap : EntityTypeConfiguration<xcuda_Export>
    {
        public xcuda_ExportMap()
        {                        
              this.HasKey(t => t.Country_Id);        
              this.ToTable("xcuda_Export");
              this.Property(t => t.Export_country_code).HasColumnName("Export_country_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Export_country_name).HasColumnName("Export_country_name").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Country_Id).HasColumnName("Country_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.Property(t => t.Export_country_region).HasColumnName("Export_country_region").IsUnicode(false).HasMaxLength(50);
              this.HasRequired(t => t.xcuda_Country).WithOptional(t => t.xcuda_Export);
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
