namespace PreviousDocumentDS.Business.Entities.Mapping
{
    //#pragma warning disable 1573
    using Entities;
    using System.Data.Entity.ModelConfiguration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class xcuda_HScodeMap : EntityTypeConfiguration<xcuda_HScode>
    {
        public xcuda_HScodeMap()
        {                        
              this.HasKey(t => t.Item_Id);        
              this.ToTable("xcuda_HScode");
              this.Property(t => t.Commodity_code).HasColumnName("Commodity_code").IsRequired().IsUnicode(false).HasMaxLength(8);
              this.Property(t => t.Precision_1).HasColumnName("Precision_1").IsUnicode(false);
              this.Property(t => t.Precision_4).HasColumnName("Precision_4").IsUnicode(false).HasMaxLength(50);
              this.Property(t => t.Item_Id).HasColumnName("Item_Id").HasDatabaseGeneratedOption(new Nullable<DatabaseGeneratedOption>(DatabaseGeneratedOption.None));
              this.HasRequired(t => t.xcuda_Tarification).WithOptional(t => t.xcuda_HScode);
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
