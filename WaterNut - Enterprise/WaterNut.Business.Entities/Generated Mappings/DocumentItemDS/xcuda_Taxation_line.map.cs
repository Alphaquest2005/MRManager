namespace DocumentItemDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class xcuda_Taxation_lineMap : EntityTypeConfiguration<xcuda_Taxation_line>
    {
        public xcuda_Taxation_lineMap()
        {                        
              this.HasKey(t => t.Taxation_line_Id);        
              this.ToTable("xcuda_Taxation_line");
              this.Property(t => t.Duty_tax_Base).HasColumnName("Duty_tax_Base").IsUnicode(false);
              this.Property(t => t.Duty_tax_rate).HasColumnName("Duty_tax_rate");
              this.Property(t => t.Duty_tax_amount).HasColumnName("Duty_tax_amount");
              this.Property(t => t.Taxation_line_Id).HasColumnName("Taxation_line_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.Property(t => t.Taxation_Id).HasColumnName("Taxation_Id");
              this.Property(t => t.Duty_tax_code).HasColumnName("Duty_tax_code");
              this.Property(t => t.Duty_tax_MP).HasColumnName("Duty_tax_MP");
              this.HasOptional(t => t.xcuda_Taxation).WithMany(t => t.xcuda_Taxation_line).HasForeignKey(d => d.Taxation_Id);
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
