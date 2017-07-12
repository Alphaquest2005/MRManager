namespace DocumentDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class ExportTemplateMap : EntityTypeConfiguration<ExportTemplate>
    {
        public ExportTemplateMap()
        {                        
              this.HasKey(t => t.ExportTemplateId);        
              this.ToTable("ExportTemplate");
              this.Property(t => t.ExportTemplateId).HasColumnName("ExportTemplateId").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.Identity));
              this.Property(t => t.Description).HasColumnName("Description").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Exporter_code).HasColumnName("Exporter_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Exporter_name).HasColumnName("Exporter_name").IsUnicode(false);
              this.Property(t => t.Consignee_code).HasColumnName("Consignee_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Consignee_name).HasColumnName("Consignee_name").IsUnicode(false);
              this.Property(t => t.Financial_code).HasColumnName("Financial_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Customs_clearance_office_code).HasColumnName("Customs_clearance_office_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Customs_Procedure).HasColumnName("Customs_Procedure").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Declarant_code).HasColumnName("Declarant_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Country_first_destination).HasColumnName("Country_first_destination").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Trading_country).HasColumnName("Trading_country").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Export_country_code).HasColumnName("Export_country_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Destination_country_code).HasColumnName("Destination_country_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.TransportName).HasColumnName("TransportName").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.TransportNationality).HasColumnName("TransportNationality").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Location_of_goods).HasColumnName("Location_of_goods").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Border_information_Mode).HasColumnName("Border_information_Mode").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Delivery_terms_Code).HasColumnName("Delivery_terms_Code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Border_office_Code).HasColumnName("Border_office_Code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Gs_Invoice_Currency_code).HasColumnName("Gs_Invoice_Currency_code").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Warehouse_Identification).HasColumnName("Warehouse_Identification").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Warehouse_Delay).HasColumnName("Warehouse_Delay").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Number_of_packages).HasColumnName("Number_of_packages").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Total_number_of_packages).HasColumnName("Total_number_of_packages").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Deffered_payment_reference).HasColumnName("Deffered_payment_reference").IsUnicode(false).HasMaxLength(50);
              this.HasMany(t => t.xcuda_ASYCUDA_ExtendedProperties).WithOptional(t => t.ExportTemplate).HasForeignKey(d => d.ExportTemplateId);
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
