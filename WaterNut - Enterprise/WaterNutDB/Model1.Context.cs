﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 1573
namespace WaterNutDB
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class WaterNutDBEntities : DbContext
    {
        static WaterNutDBEntities()
    	{ 
    		Database.SetInitializer<WaterNutDBEntities>(null);
    	}
    	
    	public WaterNutDBEntities() : base("name=WaterNutDBEntities")
        {
        }
    	
    	public WaterNutDBEntities(string nameOrConnectionString) : base(nameOrConnectionString)
    	{	
    	}
    
    	public WaterNutDBEntities(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
    	{
    	}
    
    	public WaterNutDBEntities(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
    	{
    	}
    
    	public WaterNutDBEntities(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
    	{
    	}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {		
    		modelBuilder.Configurations.Add(new ApplicationSettings_Mapping());
    		modelBuilder.Configurations.Add(new AsycudaDocumentSet_Mapping());
    		modelBuilder.Configurations.Add(new AsycudaDocumentSetPreviousDocuments_Mapping());
    		modelBuilder.Configurations.Add(new AsycudaDocumentSetPreviousEntries_Mapping());
    		modelBuilder.Configurations.Add(new AsycudaEntries_Mapping());
    		modelBuilder.Configurations.Add(new AsycudaSalesAllocations_Mapping());
    		modelBuilder.Configurations.Add(new CounterPointPODetails_Mapping());
    		modelBuilder.Configurations.Add(new CounterPointPOs_Mapping());
    		modelBuilder.Configurations.Add(new CounterPointSales_Mapping());
    		modelBuilder.Configurations.Add(new CounterPointSalesDetails_Mapping());
    		modelBuilder.Configurations.Add(new Customs_Procedure_Mapping());
    		modelBuilder.Configurations.Add(new Document_Type_Mapping());
    		modelBuilder.Configurations.Add(new EntryData_Mapping());
    		modelBuilder.Configurations.Add(new EntryDataDetails_Mapping());
    		modelBuilder.Configurations.Add(new ExportTemplate_Mapping());
    		modelBuilder.Configurations.Add(new InventoryItems_Mapping());
    		modelBuilder.Configurations.Add(new Licences_Mapping());
    		modelBuilder.Configurations.Add(new OpeningStock_Mapping());
    		modelBuilder.Configurations.Add(new PurchaseOrders_Mapping());
    		modelBuilder.Configurations.Add(new Sales_Mapping());
    		modelBuilder.Configurations.Add(new SubItems_Mapping());
    		modelBuilder.Configurations.Add(new sysdiagrams_Mapping());
    		modelBuilder.Configurations.Add(new TariffCategory_Mapping());
    		modelBuilder.Configurations.Add(new TariffCodes_Mapping());
    		modelBuilder.Configurations.Add(new TariffSupUnitLkps_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Assessment_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Assessment_notice_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_ASYCUDA_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_ASYCUDA_ExtendedProperties_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Attached_documents_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Border_information_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Border_office_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Consignee_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Container_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Country_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Declarant_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Delivery_terms_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Departure_arrival_information_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Destination_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Export_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Export_release_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Exporter_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Financial_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Financial_Amounts_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Financial_Guarantee_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Forms_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_General_information_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Global_taxes_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Goods_description_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Gs_deduction_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Gs_external_freight_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Gs_insurance_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Gs_internal_freight_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Gs_Invoice_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Gs_other_cost_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_HScode_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Identification_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Item_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_item_deduction_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_item_external_freight_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_item_insurance_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_item_internal_freight_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Item_Invoice_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_item_other_cost_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Market_valuer_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Means_of_transport_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Nbers_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Office_segment_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Packages_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Place_of_loading_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Previous_doc_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_PreviousItem_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Principal_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Property_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_receipt_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Registration_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Seals_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Signature_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Supplementary_unit_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Suppliers_documents_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Suppliers_link_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Tarification_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Taxation_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Taxation_line_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Total_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Traders_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Traders_Financial_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Transit_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Transit_Destination_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Transport_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Type_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Valuation_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Valuation_item_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Warehouse_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Weight_Mapping());
    		modelBuilder.Configurations.Add(new xcuda_Weight_itm_Mapping());
        }
    	
        public DbSet<AsycudaDocumentSet> AsycudaDocumentSet { get; set; }
        public DbSet<Customs_Procedure> Customs_Procedure { get; set; }
        public DbSet<Document_Type> Document_Type { get; set; }
        public DbSet<EntryData> EntryData { get; set; }
        public DbSet<EntryDataDetails> EntryDataDetails { get; set; }
        public DbSet<ExportTemplate> ExportTemplate { get; set; }
        public DbSet<InventoryItems> InventoryItems { get; set; }
        public DbSet<Licences> Licences { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<TariffCategory> TariffCategory { get; set; }
        public DbSet<TariffCodes> TariffCodes { get; set; }
        public DbSet<TariffSupUnitLkps> TariffSupUnitLkps { get; set; }
        public DbSet<xcuda_Assessment> xcuda_Assessment { get; set; }
        public DbSet<xcuda_Assessment_notice> xcuda_Assessment_notice { get; set; }
        public DbSet<xcuda_ASYCUDA> xcuda_ASYCUDA { get; set; }
        public DbSet<xcuda_Attached_documents> xcuda_Attached_documents { get; set; }
        public DbSet<xcuda_Border_information> xcuda_Border_information { get; set; }
        public DbSet<xcuda_Border_office> xcuda_Border_office { get; set; }
        public DbSet<xcuda_Container> xcuda_Container { get; set; }
        public DbSet<xcuda_Declarant> xcuda_Declarant { get; set; }
        public DbSet<xcuda_Delivery_terms> xcuda_Delivery_terms { get; set; }
        public DbSet<xcuda_Departure_arrival_information> xcuda_Departure_arrival_information { get; set; }
        public DbSet<xcuda_Export_release> xcuda_Export_release { get; set; }
        public DbSet<xcuda_Financial> xcuda_Financial { get; set; }
        public DbSet<xcuda_Financial_Amounts> xcuda_Financial_Amounts { get; set; }
        public DbSet<xcuda_Financial_Guarantee> xcuda_Financial_Guarantee { get; set; }
        public DbSet<xcuda_Global_taxes> xcuda_Global_taxes { get; set; }
        public DbSet<xcuda_Goods_description> xcuda_Goods_description { get; set; }
        public DbSet<xcuda_HScode> xcuda_HScode { get; set; }
        public DbSet<xcuda_Identification> xcuda_Identification { get; set; }
        public DbSet<xcuda_Item> xcuda_Item { get; set; }
        public DbSet<xcuda_Means_of_transport> xcuda_Means_of_transport { get; set; }
        public DbSet<xcuda_Nbers> xcuda_Nbers { get; set; }
        public DbSet<xcuda_Office_segment> xcuda_Office_segment { get; set; }
        public DbSet<xcuda_Packages> xcuda_Packages { get; set; }
        public DbSet<xcuda_Place_of_loading> xcuda_Place_of_loading { get; set; }
        public DbSet<xcuda_Previous_doc> xcuda_Previous_doc { get; set; }
        public DbSet<xcuda_Principal> xcuda_Principal { get; set; }
        public DbSet<xcuda_Property> xcuda_Property { get; set; }
        public DbSet<xcuda_receipt> xcuda_receipt { get; set; }
        public DbSet<xcuda_Registration> xcuda_Registration { get; set; }
        public DbSet<xcuda_Seals> xcuda_Seals { get; set; }
        public DbSet<xcuda_Signature> xcuda_Signature { get; set; }
        public DbSet<xcuda_Supplementary_unit> xcuda_Supplementary_unit { get; set; }
        public DbSet<xcuda_Suppliers_documents> xcuda_Suppliers_documents { get; set; }
        public DbSet<xcuda_Suppliers_link> xcuda_Suppliers_link { get; set; }
        public DbSet<xcuda_Tarification> xcuda_Tarification { get; set; }
        public DbSet<xcuda_Taxation> xcuda_Taxation { get; set; }
        public DbSet<xcuda_Taxation_line> xcuda_Taxation_line { get; set; }
        public DbSet<xcuda_Transit> xcuda_Transit { get; set; }
        public DbSet<xcuda_Transport> xcuda_Transport { get; set; }
        public DbSet<xcuda_Type> xcuda_Type { get; set; }
        public DbSet<xcuda_Warehouse> xcuda_Warehouse { get; set; }
        public DbSet<CounterPointPODetails> CounterPointPODetails { get; set; }
        public DbSet<CounterPointPOs> CounterPointPOs { get; set; }
        public DbSet<xcuda_Transit_Destination> xcuda_Transit_Destination { get; set; }
        public DbSet<CounterPointSalesDetails> CounterPointSalesDetails { get; set; }
        public DbSet<CounterPointSales> CounterPointSales { get; set; }
        public DbSet<xcuda_ASYCUDA_ExtendedProperties> xcuda_ASYCUDA_ExtendedProperties { get; set; }
        public DbSet<AsycudaEntries> AsycudaEntries { get; set; }
        public DbSet<AsycudaDocumentSetPreviousDocuments> AsycudaDocumentSetPreviousDocuments { get; set; }
        public DbSet<AsycudaDocumentSetPreviousEntries> AsycudaDocumentSetPreviousEntries { get; set; }
        public DbSet<xcuda_item_deduction> xcuda_item_deduction { get; set; }
        public DbSet<xcuda_item_external_freight> xcuda_item_external_freight { get; set; }
        public DbSet<xcuda_item_insurance> xcuda_item_insurance { get; set; }
        public DbSet<xcuda_item_internal_freight> xcuda_item_internal_freight { get; set; }
        public DbSet<xcuda_Item_Invoice> xcuda_Item_Invoice { get; set; }
        public DbSet<xcuda_item_other_cost> xcuda_item_other_cost { get; set; }
        public DbSet<xcuda_Market_valuer> xcuda_Market_valuer { get; set; }
        public DbSet<xcuda_Valuation_item> xcuda_Valuation_item { get; set; }
        public DbSet<xcuda_Weight_itm> xcuda_Weight_itm { get; set; }
        public DbSet<xcuda_Gs_deduction> xcuda_Gs_deduction { get; set; }
        public DbSet<xcuda_Gs_external_freight> xcuda_Gs_external_freight { get; set; }
        public DbSet<xcuda_Gs_insurance> xcuda_Gs_insurance { get; set; }
        public DbSet<xcuda_Gs_internal_freight> xcuda_Gs_internal_freight { get; set; }
        public DbSet<xcuda_Gs_Invoice> xcuda_Gs_Invoice { get; set; }
        public DbSet<xcuda_Gs_other_cost> xcuda_Gs_other_cost { get; set; }
        public DbSet<xcuda_Total> xcuda_Total { get; set; }
        public DbSet<xcuda_Valuation> xcuda_Valuation { get; set; }
        public DbSet<xcuda_Weight> xcuda_Weight { get; set; }
        public DbSet<xcuda_Country> xcuda_Country { get; set; }
        public DbSet<xcuda_Destination> xcuda_Destination { get; set; }
        public DbSet<xcuda_Export> xcuda_Export { get; set; }
        public DbSet<xcuda_General_information> xcuda_General_information { get; set; }
        public DbSet<AsycudaSalesAllocations> AsycudaSalesAllocations { get; set; }
        public DbSet<xcuda_Consignee> xcuda_Consignee { get; set; }
        public DbSet<xcuda_Exporter> xcuda_Exporter { get; set; }
        public DbSet<xcuda_Traders> xcuda_Traders { get; set; }
        public DbSet<xcuda_Traders_Financial> xcuda_Traders_Financial { get; set; }
        public DbSet<xcuda_Forms> xcuda_Forms { get; set; }
        public DbSet<ApplicationSettings> ApplicationSettings { get; set; }
        public DbSet<xcuda_PreviousItem> xcuda_PreviousItem { get; set; }
        public DbSet<SubItems> SubItems { get; set; }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int sp_alterdiagram1(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram1", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram1(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram1", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram1(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram1", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition1_Result> sp_helpdiagramdefinition1(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition1_Result>("sp_helpdiagramdefinition1", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams1_Result> sp_helpdiagrams1(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams1_Result>("sp_helpdiagrams1", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram1(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram1", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams1");
        }
    
        public virtual int sp_alterdiagram2(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram2", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram2(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram2", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram2(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram2", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition2_Result> sp_helpdiagramdefinition2(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition2_Result>("sp_helpdiagramdefinition2", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams2_Result> sp_helpdiagrams2(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams2_Result>("sp_helpdiagrams2", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram2(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram2", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams2()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams2");
        }
    
        public virtual int UpdateAsycudaEntry(Nullable<int> item_Id, Nullable<double> dFQtyAllocated, Nullable<double> dPQtyAllocated)
        {
            var item_IdParameter = item_Id.HasValue ?
                new ObjectParameter("Item_Id", item_Id) :
                new ObjectParameter("Item_Id", typeof(int));
    
            var dFQtyAllocatedParameter = dFQtyAllocated.HasValue ?
                new ObjectParameter("DFQtyAllocated", dFQtyAllocated) :
                new ObjectParameter("DFQtyAllocated", typeof(double));
    
            var dPQtyAllocatedParameter = dPQtyAllocated.HasValue ?
                new ObjectParameter("DPQtyAllocated", dPQtyAllocated) :
                new ObjectParameter("DPQtyAllocated", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateAsycudaEntry", item_IdParameter, dFQtyAllocatedParameter, dPQtyAllocatedParameter);
        }
    }
}
