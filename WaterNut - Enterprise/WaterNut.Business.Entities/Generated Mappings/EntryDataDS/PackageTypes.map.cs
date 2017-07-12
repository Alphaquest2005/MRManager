﻿namespace EntryDataDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class PackageTypesMap : EntityTypeConfiguration<PackageTypes>
    {
        public PackageTypesMap()
        {                        
              this.HasKey(t => t.PackageType);        
              this.ToTable("PackageTypes");
              this.Property(t => t.PackageType).HasColumnName("PackageType").IsRequired().IsUnicode(false).HasMaxLength(10);
              this.Property(t => t.PackageDescription).HasColumnName("PackageDescription").IsUnicode(false).HasMaxLength(50);
              this.HasMany(t => t.Container).WithOptional(t => t.PackageTypes).HasForeignKey(d => d.Packages_type);
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
