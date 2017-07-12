

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AllocationQS.Business.Entities;
using AllocationQS.Business.Services;
using Core.Common.Data;
using CoreEntities.Business.Entities;
using CoreEntities.Business.Services;
using DocumentDS.Business.Entities;
using EntryDataDS.Business.Entities;
using Omu.ValueInjecter;
using TrackableEntities;
using TrackableEntities.EF6;
using System.Data.Entity;
using Asycuda421;
using DocumentItemDS.Business.Entities;
using Core.Common.UI;
using InventoryDS.Business.Entities;
using DocumentDS.Business.Services;

using InventoryDS.Business.Services;
using WaterNut.Business.Entities;
using WaterNut.DataSpace.Asycuda;
using WaterNut.Interfaces;
using AsycudaDocument = CoreEntities.Business.Entities.AsycudaDocument;
using AsycudaDocumentEntryData = DocumentDS.Business.Entities.AsycudaDocumentEntryData;
using Customs_Procedure = DocumentDS.Business.Entities.Customs_Procedure;
using Customs_ProcedureService = DocumentDS.Business.Services.Customs_ProcedureService;
using Document_Type = DocumentDS.Business.Entities.Document_Type;
using Document_TypeService = DocumentDS.Business.Services.Document_TypeService;
using EntryData = EntryDataDS.Business.Entities.EntryData;
using EntryDataDetails = EntryDataDS.Business.Entities.EntryDataDetails;
using EntryDataDetailsEx = EntryDataQS.Business.Entities.EntryDataDetailsEx;
using EntryDataDetailsService = EntryDataDS.Business.Services.EntryDataDetailsService;
using EntryDataService = EntryDataDS.Business.Services.EntryDataService;
using WaterNutDBEntities = WaterNut.DataLayer.WaterNutDBEntities;
using xcuda_Item = DocumentItemDS.Business.Entities.xcuda_Item;
using xcuda_ItemService = DocumentItemDS.Business.Services.xcuda_ItemService;
using xcuda_Item_Invoice = DocumentItemDS.Business.Entities.xcuda_Item_Invoice;
using xcuda_PreviousItem = DocumentItemDS.Business.Entities.xcuda_PreviousItem;
using xcuda_PreviousItemService = DocumentItemDS.Business.Services.xcuda_PreviousItemService;
using xcuda_Supplementary_unit = DocumentItemDS.Business.Entities.xcuda_Supplementary_unit;


namespace WaterNut.DataSpace
{
    public partial  class BaseDataModel
    {
        private static readonly BaseDataModel instance;

        static BaseDataModel()
        {


            instance = new BaseDataModel();
            using (var ctx = new CoreEntitiesContext())
            {
                instance.CurrentApplicationSettings = ctx.ApplicationSettings.FirstOrDefault();
            }

            Initialization = InitializationAsync();
        }

        public static  BaseDataModel Instance
        {
            get { return BaseDataModel.instance; }
        }


        private static async Task InitializationAsync()
        {
            StatusModel.Timer("Loading Data");
            var tasks = new List<Task>();



           // _inventoryCache =
           //     new DataCache<InventoryItem>(
           //         await InventoryDS.DataModels.BaseDataModel.Instance.SearchInventoryItem(new List<string>() {"All"},
           //             new List<string>()
           //             {
           //                 "InventoryItemAlias",
           //                 "TariffCodes.TariffCategory.TariffSupUnitLkps"
           //             }).ConfigureAwait(false));

           //_tariffCodeCache =
           //     new DataCache<TariffCode>(
           //         await
           //             InventoryDS.DataModels.BaseDataModel.Instance.SearchTariffCode(new List<string>() {"All"})
           //                 .ConfigureAwait(false));

            _document_TypeCache =
                new DataCache<Document_Type>(
                    await
                        DocumentDS.DataModels.BaseDataModel.Instance.SearchDocument_Type(new List<string>() {"All"})
                            .ConfigureAwait(false));

            _customs_ProcedureCache =
                new DataCache<Customs_Procedure>(
                    await
                        DocumentDS.DataModels.BaseDataModel.Instance.SearchCustoms_Procedure(new List<string>() {"All"})
                            .ConfigureAwait(false));

           
            StatusModel.StopStatusUpdate();
        }

        public static DataCache<InventoryItem> _inventoryCache;
        public static DataCache<TariffCode> _tariffCodeCache;
        public static DataCache<Customs_Procedure> _customs_ProcedureCache;
        public static DataCache<Document_Type> _document_TypeCache;
     


        //public DataCache<InventoryItem> InventoryCache { get { return BaseDataModel._inventoryCache; } }
        //public DataCache<TariffCode> TariffCodeCache {  get { return BaseDataModel._tariffCodeCache; } }
        public DataCache<Customs_Procedure> Customs_ProcedureCache {  get { return BaseDataModel._customs_ProcedureCache; } }
        public DataCache<Document_Type> Document_TypeCache {  get { return BaseDataModel._document_TypeCache; } }
       

        public bool ValidateInstallation()
        {
            try
            {

           
           //return true;
            using (var ctx = new CoreEntitiesContext())
            {
                
                if (Environment.MachineName.ToLower() == "alphaquest-PC".ToLower())return true;

                if (Environment.ProcessorCount == 4 && Environment.MachineName.ToLower() == "Alister-PC".ToLower()
                    && ctx.Database.Connection.ConnectionString.ToLower().Contains(@"Alister-PC\SQLEXPRESS2012;Initial Catalog=IWWDB-Enterprise".ToLower()))
                {
                    return true;
                }

                if (Environment.MachineName.ToLower() == "linnea-hp".ToLower()
                   && ctx.Database.Connection.ConnectionString.ToLower().Contains(@"linnea-hp\SQLEXPRESS2012;Initial Catalog=NorthYachtDB-Enterprise".ToLower()))
                {
                    return true;
                }

                if (Environment.MachineName.ToLower() == "DESKTOP-VIS2G9B".ToLower())return true;

                return false;
                
            }

            }
            catch (Exception)
            {

                throw;
            }
        }

        internal async Task Clear(int AsycudaDocumentSetId)
        {
            AsycudaDocumentSet docset = null;
            using (var ctx = new AsycudaDocumentSetService())
            {
                docset = await ctx.GetAsycudaDocumentSetByKey(AsycudaDocumentSetId.ToString(), 
                    new List<string>()
                    {
                        "xcuda_ASYCUDA_ExtendedProperties",
                        "xcuda_ASYCUDA_ExtendedProperties.xcuda_ASYCUDA",
                        "xcuda_ASYCUDA_ExtendedProperties.xcuda_ASYCUDA.xcuda_PreviousItem"
                    }).ConfigureAwait(false);
            }
            Clear(docset);
        }
        internal async  void Clear(AsycudaDocumentSet currentAsycudaDocumentSet)
        {
                await ClearAsycudaDocumentSet(currentAsycudaDocumentSet).ConfigureAwait(false);
        }

        public  async Task ClearAsycudaDocumentSet(int AsycudaDocumentSetId)
        {
            var docset = await GetAsycudaDocumentSet(AsycudaDocumentSetId, new List<string>()
            {
                 "xcuda_ASYCUDA_ExtendedProperties",
                 "xcuda_ASYCUDA_ExtendedProperties.xcuda_ASYCUDA",
                 "xcuda_ASYCUDA_ExtendedProperties.xcuda_ASYCUDA.xcuda_Declarant"
            }).ConfigureAwait(false);
            await ClearAsycudaDocumentSet(docset).ConfigureAwait(false);
        }

        public void AttachCustomProcedure(DocumentCT cdoc, Customs_Procedure cp)
        {
            if (cp == null)
            {
                throw new ApplicationException("Default Export Template not configured properly!");
            }
            else
            {
                cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Customs_ProcedureId = cp.Customs_ProcedureId;
                cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Customs_Procedure = cp;
                cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Document_TypeId = cp.Document_TypeId;
                cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Document_Type = cp.Document_Type;
            }
        }

        public async Task ClearAsycudaDocumentSet(AsycudaDocumentSet docset)
        {
            
            StatusModel.StartStatusUpdate("Deleting Documents from '' Document Set", docset.xcuda_ASYCUDA_ExtendedProperties.Count());

            var doclst = docset.xcuda_ASYCUDA_ExtendedProperties.Where(x => x.xcuda_ASYCUDA != null).ToList();
            //foreach (var item in doclst)
            var exceptions = new ConcurrentQueue<Exception>();
            Parallel.ForEach(doclst, new ParallelOptions(){MaxDegreeOfParallelism = Environment.ProcessorCount * 2}, item =>
            {
                StatusModel.StatusUpdate();
                try
                {
                    DeleteAsycudaDocument(item.xcuda_ASYCUDA).Wait();
                }
                catch (Exception ex)
                {

                    exceptions.Enqueue(
                                new ApplicationException(
                                    string.Format("Could not import file - '{0}. Error:{1} Stacktrace:{2}", item.xcuda_ASYCUDA.CNumber + item.xcuda_ASYCUDA.RegistrationDate.ToShortDateString(),
                                        ex.Message, ex.StackTrace)));
                }
                 

            }
                );
          if(exceptions.Count > 0) throw new AggregateException(exceptions);
            StatusModel.StopStatusUpdate();
            
        }

        public interface IEntryLineData
        {
            string ItemNumber { get; set; }
             string ItemDescription { get; set; }
             string TariffCode { get; set; }
             double Cost { get; set; }
             string PreviousDocumentItemId { get; set; }
             double Quantity { get; set; }
            
            List<EntryDataDetailSummary> EntryDataDetails { get; set; }
           //  IDocumentItem PreviousDocumentItem { get; set; }
            // IInventoryItem InventoryItem { get; set; }
         

            double Weight { get; set; }

             double InternalFreight { get; set; }

             double Freight { get; set; }
            List<ITariffSupUnitLkp> TariffSupUnitLkps { get; set; }
        }

        public class EntryLineData : IEntryLineData
        {
            string _itemNumber;
            public string ItemNumber 
            {
                get 
                {
                    return _itemNumber;
                }
                set
                {
                    _itemNumber = value;

                    using (var ctx = new InventoryItemService())
                    {
                        if (_itemNumber != null)
                        {
                            InventoryItem = ctx.GetInventoryItemByKey(_itemNumber, new List<string>()
                            {
                                "TariffCodes.TariffCategory.TariffSupUnitLkps"
                            } ).Result;
                        }
                        else
                        {
                            InventoryItem = null;
                        }
                    }
                }
            }
            public string ItemDescription { get; set; }
            public string TariffCode { get; set; }
            public double Cost { get; set; }
            
            string _previousDocumentItemId;
            public string PreviousDocumentItemId
            {
                get
                {
                    return _previousDocumentItemId;
                }
                set
                {
                    _previousDocumentItemId = value;

                    using (var ctx = new xcuda_ItemService())
                    {
                        if (_previousDocumentItemId != null)
                        {
                            PreviousDocumentItem = ctx.Getxcuda_ItemByKey(_previousDocumentItemId).Result;
                        }
                        else
                        {
                            PreviousDocumentItem = null;
                        }
                    }
                }
            }
            public double Quantity { get; set; }
            public List<EntryDataDetailSummary> EntryDataDetails { get; set; }
            public IDocumentItem PreviousDocumentItem { get; set; }
            public IInventoryItem InventoryItem { get; set; }
            public EntryData EntryData { get; set; }

            public double Freight { get; set; }
            public List<ITariffSupUnitLkp> TariffSupUnitLkps { get; set; }

            public double Weight { get; set; }

            public double InternalFreight { get; set; }
        }


        public  void IntCdoc(xcuda_ASYCUDA doc, Document_Type dt, AsycudaDocumentSet ads)
        {
            var cdoc = new DocumentCT {Document = doc};
            IntCdoc(cdoc, dt, ads);
        }

        public async Task<DocumentCT> CreateDocumentCt(AsycudaDocumentSet currentAsycudaDocumentSet)
        {
            try
            {
                var cdoc = new DocumentCT();
                //using (var ctx = new xcuda_ASYCUDAService())
                //{
                    cdoc.Document = CreateNewAsycudaDocument(currentAsycudaDocumentSet);
                    //d.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet = null;// this is to save with out 2 extended properties with new entityid = 0
                    //cdoc.Document = await ctx.Createxcuda_ASYCUDA(d).ConfigureAwait(false);
                //}
                cdoc.DocumentItems = new List<xcuda_Item>();
                return cdoc;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal IInventoryItem GetInventoryItem(Func<IInventoryItem, bool> p)
        {
            using (var ctx = new InventoryDSContext())
            {
                return ctx.InventoryItems.FirstOrDefault(p);
            }
        }

        public xcuda_ASYCUDA CreateNewAsycudaDocument(AsycudaDocumentSet CurrentAsycudaDocumentSet)
        {
            var ndoc = new xcuda_ASYCUDA(true) { TrackingState = TrackingState.Added };// 
            //ndoc.SetupProperties();

            if (CurrentAsycudaDocumentSet != null)
            {
                CurrentAsycudaDocumentSet.xcuda_ASYCUDA_ExtendedProperties.Add(ndoc.xcuda_ASYCUDA_ExtendedProperties);
                ndoc.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet = CurrentAsycudaDocumentSet;
                ndoc.xcuda_ASYCUDA_ExtendedProperties.FileNumber = CurrentAsycudaDocumentSet.xcuda_ASYCUDA_ExtendedProperties.Count();
            }
            return ndoc;
        }

        public  void IntCdoc(DocumentCT cdoc, Document_Type dt, AsycudaDocumentSet ads)
        {

            cdoc.Document.xcuda_Declarant.Number = ads.Declarant_Reference_Number + "-F" + cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.FileNumber.ToString();
            cdoc.Document.xcuda_Identification.Manifest_reference_number = ads.Manifest_Number;
            cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSetId = ads.AsycudaDocumentSetId;

            cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Document_Type = dt;
            cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Document_TypeId = dt.Document_TypeId;
            cdoc.Document.xcuda_Identification.xcuda_Type.Declaration_gen_procedure_code = dt.Declaration_gen_procedure_code;
            cdoc.Document.xcuda_Identification.xcuda_Type.Type_of_declaration = dt.Type_of_declaration;
            cdoc.Document.xcuda_General_information.xcuda_Country.Country_first_destination = ads.Country_of_origin_code;
            cdoc.Document.xcuda_Valuation.xcuda_Gs_Invoice.Currency_rate = Convert.ToSingle(ads.Exchange_Rate);
            cdoc.Document.xcuda_Valuation.xcuda_Gs_Invoice.Currency_code = ads.Currency_Code;

            if (cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Customs_ProcedureId != ads.Customs_Procedure.Customs_ProcedureId)
            {
                if (cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Customs_ProcedureId  != 0)
                {
                    var c = cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Customs_Procedure;
                    foreach (var item in cdoc.DocumentItems.Where(x => x.xcuda_Tarification.Extended_customs_procedure == c.Extended_customs_procedure && x.xcuda_Tarification.National_customs_procedure == c.National_customs_procedure).ToList())
                    {
                        item.xcuda_Tarification.Extended_customs_procedure = ads.Customs_Procedure.Extended_customs_procedure;
                        item.xcuda_Tarification.National_customs_procedure = ads.Customs_Procedure.National_customs_procedure;
                    }
                }
                cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Customs_ProcedureId = ads.Customs_ProcedureId;
                cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Customs_Procedure = ads.Customs_Procedure;
            }


            if (cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.BLNumber != ads.BLNumber)
            {
                if (cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.BLNumber != null)
                {
                    var b = cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.BLNumber;
                    foreach (var item in cdoc.DocumentItems.Where(x => x.xcuda_Previous_doc.Summary_declaration == b).ToList())
                    {
                        item.xcuda_Previous_doc.Summary_declaration = ads.BLNumber;
                    }
                }
                cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.BLNumber = ads.BLNumber;
            }



            cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Description = ads.Description;

            //    cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AutoUpdate = true;


        }

        public async Task AddToEntry(IEnumerable<string> entryDatalst, AsycudaDocumentSet currentAsycudaDocumentSet, bool perInvoice)
        {
            try
            {
                if (!IsValidDocument(currentAsycudaDocumentSet)) return;

                var slstSource =
                    (from s in await GetSelectedPODetails(entryDatalst).ConfigureAwait(false)
                        //.Where(p => p.Downloaded == false)
                        select s).ToList();
                ;
                if (!IsValidEntryData(slstSource)) return;

                await CreateEntryItems(slstSource, currentAsycudaDocumentSet, perInvoice).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ValidateExistingTariffCodes(AsycudaDocumentSet currentAsycudaDocumentSet)
        {
           
        }



        public async Task AddToEntry(IEnumerable<EntryDataDetailsEx> entryDataDetailslst,
            AsycudaDocumentSet currentAsycudaDocumentSet)
        {
            try
            {
                if (!IsValidDocument(currentAsycudaDocumentSet)) return;

                var slstSource =
                    (from s in await GetSelectedPODetails(entryDataDetailslst).ConfigureAwait(false)
                        //.Where(p => p.Downloaded == false)
                        select s).ToList();
                
                if (!IsValidEntryData(slstSource)) return;

                await CreateEntryItems(slstSource, currentAsycudaDocumentSet).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsValidEntryData(List<EntryDataDetails> slstSource)
        {
            
            if (!slstSource.Any())
            {
               throw new ApplicationException("Please Select Entry Data before proceeding");
                
            }
            return true;
        }

        private bool IsValidDocument(AsycudaDocumentSet currentAsycudaDocumentSet)
        {
            if (currentAsycudaDocumentSet == null)
            {
                throw new ApplicationException("Please Select a Asycuda Document Set before proceeding");
            }

            if (currentAsycudaDocumentSet.Document_Type == null ||
                currentAsycudaDocumentSet.Customs_Procedure == null)
            {
                throw new ApplicationException(
                    "Please Select Document Type & Customs Procedure for selected Asycuda Document Set before proceeding");
                
            }
            return true;
        }

        private async Task CreateEntryItems(List<EntryDataDetails> slstSource,
            AsycudaDocumentSet currentAsycudaDocumentSet, bool perInvoice = false)
        {
            var itmcount = 0;
            var slst = CreateEntryLineData(slstSource);

            var cdoc = new DocumentCT {Document = CreateNewAsycudaDocument(currentAsycudaDocumentSet)};

            //BaseDataModel.Instance.CurrentAsycudaDocumentSet.xcuda_ASYCUDA_ExtendedProperties.Add(cdoc.xcuda_ASYCUDA_ExtendedProperties);
            IntCdoc(cdoc, currentAsycudaDocumentSet.Document_Type,
                currentAsycudaDocumentSet);
            cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AutoUpdate = true;

            var entryLineDatas = slst as IList<BaseDataModel.EntryLineData> ?? slst.ToList();
            StatusModel.StartStatusUpdate("Adding Entries to New Asycuda Document", entryLineDatas.Count());
            EntryData entryData = null;
            if (perInvoice)
            {
                entryLineDatas = entryLineDatas.OrderBy(p => p.EntryData.EntryDataId).ToList();
            }
            else
            {
                switch (CurrentApplicationSettings.OrderEntriesBy)
                {
                    case "TariffCode":
                        entryLineDatas = entryLineDatas.OrderBy(p => p.InventoryItem.TariffCode).ToList();
                        break;
                    default:
                        break;
                }
            }


            var oldentryData = "";
            foreach (var pod in entryLineDatas)//
            {
                if (perInvoice )
                {
                    if (oldentryData == "") oldentryData = pod.EntryData.EntryDataId;
                    if (cdoc.DocumentItems.Any() && oldentryData != pod.EntryData.EntryDataId)
                    {
                        await SaveDocumentCT(cdoc).ConfigureAwait(false);
                        cdoc = new DocumentCT {Document = CreateNewAsycudaDocument(currentAsycudaDocumentSet)};

                        //BaseDataModel.Instance.CurrentAsycudaDocumentSet.xcuda_ASYCUDA_ExtendedProperties.Add(cdoc.xcuda_ASYCUDA_ExtendedProperties);
                        IntCdoc(cdoc, currentAsycudaDocumentSet.Document_Type,
                            currentAsycudaDocumentSet);
                        cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AutoUpdate = true;
                        oldentryData = pod.EntryData.EntryDataId;
                        itmcount = 0;
                    }
                    
                }

                if (entryData == null || entryData.EntryDataId != pod.EntryData.EntryDataId)
                {
                    // do container
                    if (entryData == null || !pod.EntryData.ContainerEntryData.OrderBy(x => x.Container_Id).SequenceEqual(entryData.ContainerEntryData.OrderBy(z => z.Container_Id)))
                    {
                        foreach (var c in pod.EntryData.ContainerEntryData)
                        {
                            var cnt =(await EntryDataDS.DataModels.BaseDataModel.Instance.SearchContainer(new List<string>()
                            {
                                string.Format("Container_Id == {0}", c.Container_Id)
                            }).ConfigureAwait(false)).FirstOrDefault();
                            //c.Container = cnt;
                                var xcnt =
                                    cdoc.Document.xcuda_Container.FirstOrDefault(
                                        x => x.Container_identity == cnt.Container_identity);
                            if (xcnt == null)
                            {
                                xcnt = new xcuda_Container(true)
                                {
                                    ASYCUDA_Id = cdoc.Document.ASYCUDA_Id,
                                    Container_identity =  cnt.Container_identity,
                                    Container_type = cnt.Container_type,
                                    Packages_number = cnt.Packages_number,
                                    Packages_type = cnt.Packages_type,
                                    Packages_weight = cnt.Packages_weight,
                                    Item_Number = (cdoc.DocumentItems.Count + 1).ToString(),
                                    TrackingState = TrackingState.Added
                                };
                                cdoc.Document.xcuda_Container.Add(xcnt);
                            }
                            
                            
                        }
                    }
                    entryData = pod.EntryData;
                }
                
                var itm = CreateItemFromEntryDataDetail(pod, cdoc);

                if (!cdoc.Document.AsycudaDocumentEntryDatas.Any(x => x.EntryDataId == pod.EntryData.EntryDataId))
                {
                    cdoc.Document.AsycudaDocumentEntryDatas.Add(new AsycudaDocumentEntryData(true)
                    {
                        AsycudaDocumentId = cdoc.Document.ASYCUDA_Id,
                        EntryDataId = pod.EntryData.EntryDataId,
                        TrackingState = TrackingState.Added
                    });
                }

                if (itm == null) continue;

                itmcount += 1;
                if (itmcount%CurrentApplicationSettings.MaxEntryLines == 0)
                {
                    if (cdoc.DocumentItems.Any())
                    {
                        await SaveDocumentCT(cdoc).ConfigureAwait(false);
                        cdoc = new DocumentCT {Document = CreateNewAsycudaDocument(currentAsycudaDocumentSet)};

                        //BaseDataModel.Instance.CurrentAsycudaDocumentSet.xcuda_ASYCUDA_ExtendedProperties.Add(cdoc.xcuda_ASYCUDA_ExtendedProperties);
                        IntCdoc(cdoc, currentAsycudaDocumentSet.Document_Type,
                            currentAsycudaDocumentSet);
                        cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AutoUpdate = true;
                        itmcount = 0;
                    }
                }


                StatusModel.StatusUpdate();
                //System.Windows.Forms.MessageBox.
            }
            StatusModel.Timer("Saving To Database");

            await SaveDocumentCT(cdoc).ConfigureAwait(false);
            StatusModel.StopStatusUpdate();


          
        }

        

        private  IEnumerable<BaseDataModel.EntryLineData> CreateEntryLineData(IEnumerable<EntryDataDetails> slstSource)
        {
            var slst = from s in slstSource.AsEnumerable()//.Where(p => p.Downloaded == false)
                group s by new {s.ItemNumber, s.ItemDescription, s.TariffCode, s.Cost, s.EntryData}
                into g
                select new BaseDataModel.EntryLineData
                {
                    ItemNumber = g.Key.ItemNumber.Trim(),
                    ItemDescription = g.Key.ItemDescription.Trim(),
                    TariffCode = g.Key.TariffCode,
                    Cost = g.Key.Cost,
                    Quantity = g.Sum(x => x.Quantity),
                    EntryDataDetails = g.Select(x => new EntryDataDetailSummary()
                    {
                        EntryDataDetailsId = x.EntryDataDetailsId,
                        EntryDataId = x.EntryDataId
                    }).ToList(),
                    EntryData = g.Key.EntryData,
                    Freight = Convert.ToDouble(g.Sum(x => x.Freight)),
                    Weight = Convert.ToDouble(g.Sum(x => x.Weight)),
                    InternalFreight = Convert.ToDouble(g.Sum(x => x.InternalFreight)),
                };
            return slst;
        }

        public async Task<xcuda_ASYCUDA> GetDocument(int ASYCUDA_Id, List<string> includeLst = null )
        {
            using (var ctx = new xcuda_ASYCUDAService(){StartTracking = true})
            {
                return await ctx.Getxcuda_ASYCUDAByKey(ASYCUDA_Id.ToString(), includeLst).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<AsycudaDocumentItem>> GetAllDocumentItems(List<string> includeLst = null)
        {
            using (var ctx = new AsycudaDocumentItemService())
            {
                return await ctx.GetAsycudaDocumentItems(includeLst).ConfigureAwait(false);
            }
        }

        public  async Task<AsycudaDocumentItem> Getxcuda_Item(int p)
        {
            using (var ctx = new AsycudaDocumentItemService())
            {
                return await ctx.GetAsycudaDocumentItemByKey(p.ToString()).ConfigureAwait(false);
            }
        }

        public async Task SaveDocumentCT(DocumentCT cdoc)
        {
            try
            {
                
                if (cdoc == null) return;
                //var docset = cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet;
                 using (var ctx = new xcuda_ASYCUDAService())
                {
                    cdoc.Document = await ctx.CleanAndUpdateXcuda_ASYCUDA(cdoc.Document).ConfigureAwait(false);
                }

                //cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet =
                //    await GetAsycudaDocumentSet(docset.AsycudaDocumentSetId, new List<string>()
                //    {
                //        "xcuda_ASYCUDA_ExtendedProperties"
                //    }).ConfigureAwait(false);
               
               

                if (cdoc.Document.ASYCUDA_Id == 0) return;
               
                    // prepare items for parrallel import
                    foreach (var item in cdoc.DocumentItems)
                    {
                        item.ASYCUDA_Id = cdoc.Document.ASYCUDA_Id;
                        item.LineNumber = cdoc.DocumentItems.IndexOf(item) + 1;
                        if (item.xcuda_PreviousItem != null)
                        {
                            item.xcuda_PreviousItem.ASYCUDA_Id = cdoc.Document.ASYCUDA_Id;
                            item.xcuda_PreviousItem.Current_item_number = item.LineNumber.ToString();
                        }
                    }

                    //Parallel.ForEach(cdoc.DocumentItems, new ParallelOptions(){MaxDegreeOfParallelism = Environment.ProcessorCount * 2}, item =>
                    //{
                    
                        using (var ctx = new xcuda_ItemService())
                        {
                            //foreach (var item in cdoc.DocumentItems)
                            //{
                            var exceptions = new ConcurrentQueue<Exception>();
                            cdoc.DocumentItems.AsParallel().ForAll((t) =>
                            {
                                try
                                {
                                    if(t.ChangeTracker != null)
                                    ctx.Updatexcuda_Item(t).Wait();//.ChangeTracker.GetChanges().FirstOrDefault()
                                }
                                catch (Exception ex)
                                {
                                    
                                    exceptions.Enqueue(ex);
                                }
                                
                            });
                            if(exceptions.Count > 0) throw new AggregateException(exceptions);
                            //    await ctx.Updatexcuda_Item(cdoc.DocumentItems).ConfigureAwait(false);
                            //}
                        }
                //});
              //  await CalculateDocumentSetFreight(cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSetId.GetValueOrDefault()).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task CalculateDocumentSetFreight(int asycudaDocumentSetId)
        {
            try
            {

                string currency = "";
                double totalfob = 0;
                double totalFreight = 0;
                List<int> doclst = null;
                Dictionary<int, double> docValues = new Dictionary<int, double>();
                using (var ctx = new DocumentDSContext())
                {
                    var asycudaDocumentSet = ctx.AsycudaDocumentSets.FirstOrDefault(x => x.AsycudaDocumentSetId == asycudaDocumentSetId);
                    if (asycudaDocumentSet != null)
                    {
                        if (asycudaDocumentSet
                            .TotalFreight != null)
                            totalFreight =
                                asycudaDocumentSet
                                    .TotalFreight.Value;
                        currency = asycudaDocumentSet.Currency_Code;
                    }
                    if (totalFreight == 0) return;
                    doclst =
                        ctx.xcuda_ASYCUDA_ExtendedProperties.Where(x => x.AsycudaDocumentSetId == asycudaDocumentSetId)
                            .Select(x => x.ASYCUDA_Id)
                            .ToList();
                    if (!doclst.Any()) return;
                }
                using (var ctx = new EntryDataDSContext())
                {
                    
                    foreach (var doc in doclst)
                    {
                        var t = ctx.AsycudaDocumentEntryData.Where(x => x.AsycudaDocumentId == doc)
                            .SelectMany(y => y.EntryData.EntryDataDetails)
                            .Sum(z => z.Cost*z.Quantity);
                        docValues.Add(doc, t);
                        totalfob += t;
                    }
                    
                }
                double rate = totalFreight/totalfob;
                using (var ctx = new DocumentDSContext() {StartTracking = true})
                {
                    foreach (var doc in docValues)
                    {
                        var val = ctx.xcuda_Valuation.Include(x => x.xcuda_Gs_external_freight).First(x => x.ASYCUDA_Id == doc.Key);
                        if (val == null) continue;
                        var xcuda_Gs_external_freight = val.xcuda_Gs_external_freight;
                        if (xcuda_Gs_external_freight == null)
                        {
                            xcuda_Gs_external_freight = new xcuda_Gs_external_freight(true) { Valuation_Id = doc.Key, xcuda_Valuation = val, TrackingState = TrackingState.Added};
                            val.xcuda_Gs_external_freight = xcuda_Gs_external_freight;
                        }
                        xcuda_Gs_external_freight.Amount_foreign_currency = doc.Value * rate;
                        xcuda_Gs_external_freight.Currency_code = currency;
                        if (xcuda_Gs_external_freight.TrackingState != TrackingState.Added )xcuda_Gs_external_freight.TrackingState = TrackingState.Modified;
                        ctx.ApplyChanges(xcuda_Gs_external_freight);
                        
                        
                        
                    }
                    ctx.SaveChanges();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal TariffCode GetTariffCode(Func<TariffCode, bool> p)
        {
            using (var ctx = new InventoryDSContext())
            {
                return ctx.TariffCodes.FirstOrDefault(p);
            }
        }

        internal  xcuda_Item CreateItemFromEntryDataDetail(BaseDataModel.IEntryLineData pod, DocumentCT cdoc)
        {
           // if (pod.TariffCode != null)
           // {
            try
            {

               var itm = CreateNewDocumentItem();
                cdoc.DocumentItems.Add(itm);
                //itm.SetupProperties();
               
                itm.xcuda_Goods_description.Commercial_Description = CleanText(pod.ItemDescription);
                if (cdoc.Document.xcuda_General_information != null)
                    itm.xcuda_Goods_description.Country_of_origin_code = cdoc.Document.xcuda_General_information.xcuda_Country.Country_first_destination;
                itm.xcuda_Tarification.Item_price = Convert.ToSingle(pod.Cost * pod.Quantity);
                itm.xcuda_Tarification.National_customs_procedure = cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Customs_Procedure.National_customs_procedure; //cdoc.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet.Customs_Procedure.National_customs_procedure;
                itm.xcuda_Tarification.Extended_customs_procedure = cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Customs_Procedure.Extended_customs_procedure;//cdoc.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet.Customs_Procedure.Extended_customs_procedure;

                itm.xcuda_Tarification.xcuda_HScode.Commodity_code = pod.TariffCode ?? "NULL";
                itm.xcuda_Tarification.xcuda_HScode.Precision_4 = pod.ItemNumber; //pod.PreviousDocumentItem == null ? pod.ItemNumber : pod.PreviousDocumentItem.ItemNumber;
                //itm.xcuda_Tarification.xcuda_HScode.InventoryItems = pod.InventoryItem ;

                if (cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.BLNumber != null)
                    itm.xcuda_Previous_doc.Summary_declaration = cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.BLNumber;

                itm.xcuda_Valuation_item.Total_CIF_itm = Convert.ToSingle(Math.Round(Convert.ToDecimal(pod.Quantity) * Convert.ToDecimal(pod.Cost), 4));
                itm.xcuda_Valuation_item.Statistical_value = Convert.ToSingle(Math.Round(Convert.ToDecimal(pod.Quantity) * Convert.ToDecimal(pod.Cost), 4));



                var ivc = new xcuda_Item_Invoice(true) { TrackingState = TrackingState.Added };

                ivc.Amount_national_currency = Convert.ToSingle(Math.Round(Convert.ToDecimal(pod.Quantity) * Convert.ToDecimal(pod.Cost) * Convert.ToDecimal(cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet.Exchange_Rate), 4));
                ivc.Amount_foreign_currency = Convert.ToSingle(Math.Round(Convert.ToDecimal(pod.Quantity) * Convert.ToDecimal(pod.Cost), 4));


                if (cdoc.Document.xcuda_Valuation != null && cdoc.Document.xcuda_Valuation.xcuda_Gs_Invoice != null)
                {
                    //;

                    ivc.Currency_code = cdoc.Document.xcuda_Valuation.xcuda_Gs_Invoice.Currency_code;//xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet.Currency_Code;
                    ivc.Currency_rate = cdoc.Document.xcuda_Valuation.xcuda_Gs_Invoice.Currency_rate;//Convert.ToSingle(cdoc.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet.Exchange_Rate);
                }

                itm.xcuda_Valuation_item.xcuda_Item_Invoice = ivc;

                if (CurrentApplicationSettings.AllowWeightEqualQuantity != "Visible")
                {
                    if ((Single) pod.Quantity > 99)
                    {
                        itm.xcuda_Valuation_item.xcuda_Weight_itm = new xcuda_Weight_itm(true)
                        {
                            TrackingState = TrackingState.Added
                        };
                        itm.xcuda_Valuation_item.xcuda_Weight_itm.Gross_weight_itm = (Single) pod.Quantity*
                                                                                     Convert.ToSingle(.1);
                        //(Decimal)ops.Quantity;
                        itm.xcuda_Valuation_item.xcuda_Weight_itm.Net_weight_itm = (Single) pod.Quantity*
                                                                                   Convert.ToSingle(.1);
                        //(Decimal)ops.Quantity;
                    }

                    if (pod.Weight != 0)
                        if (itm.xcuda_Valuation_item.xcuda_Weight_itm != null)
                            itm.xcuda_Valuation_item.xcuda_Weight_itm.Gross_weight_itm =
                                Convert.ToSingle(
                                    Math.Round(pod.Weight, 4));
                    if (pod.Weight != 0)
                        if (itm.xcuda_Valuation_item.xcuda_Weight_itm != null)
                            itm.xcuda_Valuation_item.xcuda_Weight_itm.Net_weight_itm =
                                Convert.ToSingle(
                                    Math.Round(pod.Weight, 4));
                }
                else
                {
                    if (itm.xcuda_Valuation_item.xcuda_Weight_itm != null)
                    {
                        itm.xcuda_Valuation_item.xcuda_Weight_itm.Net_weight_itm =
                            Convert.ToSingle(
                                Math.Round(pod.Quantity, 4));

                        itm.xcuda_Valuation_item.xcuda_Weight_itm.Gross_weight_itm =
                            Convert.ToSingle(
                                Math.Round(pod.Quantity, 4));
                    }
                }

                if (pod.InternalFreight != 0) itm.xcuda_Valuation_item.xcuda_item_internal_freight.Amount_foreign_currency =
                    Convert.ToSingle(pod.InternalFreight);
               

                if (pod.Freight != 0) itm.xcuda_Valuation_item.xcuda_item_external_freight.Amount_foreign_currency =
                    Convert.ToSingle(pod.Freight);

                if (cdoc.DocumentItems.Count() == 1)
                {
                    var fr = pod.EntryDataDetails.FirstOrDefault();
                    if (fr != null)
                        itm.Free_text_1 = fr.EntryDataId;
                }
                foreach (var ed in pod.EntryDataDetails)
                {
                    if (!itm.EntryDataDetails.Any(x => x.EntryDataDetailsId == ed.EntryDataDetailsId))
                        itm.EntryDataDetails.Add(new xcuda_ItemEntryDataDetails(true) { Item_Id = itm.Item_Id, EntryDataDetailsId = ed.EntryDataDetailsId, TrackingState = TrackingState.Added });

                   

                }


                itm.xcuda_Tarification.Unordered_xcuda_Supplementary_unit.Add(new xcuda_Supplementary_unit(true) { Tarification_Id = itm.xcuda_Tarification.Item_Id, Suppplementary_unit_code = "NMB", Suppplementary_unit_quantity = pod.Quantity, IsFirstRow = true, TrackingState = TrackingState.Added });

                ProcessItemTariff(pod, cdoc.Document, itm);

                return itm;
            }
            catch (Exception)
            {

                throw;
            }
                // }
           // return null;
        }

        private string CleanText(string p)
        {
            return p.Replace(",", "");
        }

        private  xcuda_Item CreateNewDocumentItem()
        {
            return new xcuda_Item(true) { TrackingState = TrackingState.Added };//
        }

        private  void ProcessItemTariff(BaseDataModel.IEntryLineData pod, xcuda_ASYCUDA cdoc, xcuda_Item itm)
        {

            if (pod.TariffCode != null)
            {

               
                    var tariffSupUnitLkps = pod.TariffSupUnitLkps;
                    if (tariffSupUnitLkps != null)
                        foreach (var item in tariffSupUnitLkps.ToList())
                        {
                            itm.xcuda_Tarification.Unordered_xcuda_Supplementary_unit.Add(new xcuda_Supplementary_unit(true) { Suppplementary_unit_code = item.SuppUnitCode2, Suppplementary_unit_quantity = pod.Quantity * item.SuppQty, TrackingState = TrackingState.Added });
                        }
               

                //if (pod.InventoryItem.TariffCodes.TariffCategory != null && pod.InventoryItem.TariffCodes.TariffCategory.LicenseRequired == true)
                //{
                //    Licences lic = cdoc.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet.Licences.Where(l => l.TariffCateoryCode == pod.InventoryItem.TariffCodes.TariffCategoryCode).FirstOrDefault();
                //    if (lic != null)
                //    {
                        
                //        itm.xcuda_Attached_documents.Add(new xcuda_Attached_documents() { Attached_document_code = "LC02", Attached_document_from_rule = 1, Attached_document_name = "IMPORT LICENCE", Attached_document_reference = lic.LicenceNumber + " " + lic.Year, Attached_document_date = DateTime.Now.Date.ToShortDateString() });
                //    }
                //}
            }
            
        }

        public async Task RemoveEntryData(string po)
        {
            using (var ctx = new EntryDataService())
            {
                if (po != null) await ctx.DeleteEntryData(po).ConfigureAwait(false);
            }
            
        }


        public  async Task RemoveItem(int id)
        {
            //if (CurrentAsycudaItemEntryId == null) return;
            xcuda_Item r;
            using (var ctx = new xcuda_ItemService())
            {
                r = await ctx.Getxcuda_ItemByKey(id.ToString()).ConfigureAwait(false);
               await ctx.Deletexcuda_Item(id.ToString()).ConfigureAwait(false);                
            }

            await ReDoDocumentLineNumbers(r.ASYCUDA_Id).ConfigureAwait(false);

            
        }

        public  async Task ReDoDocumentLineNumbers(int ASYCUDA_Id)
        {
            using (var ctx = new xcuda_ItemService())
            {
                var lst = (await ctx.Getxcuda_ItemByASYCUDA_Id(ASYCUDA_Id.ToString()).ConfigureAwait(false)).OrderBy(x => x.LineNumber);

                for (var i = 0; i < lst.Count(); i++)
                {
                    var itm = lst.ElementAt(i);
                    itm.LineNumber = i + 1;
                    await ctx.Updatexcuda_Item(itm).ConfigureAwait(false);
                }                
            }
        }


        internal  async Task RemoveSelectedItems(List<xcuda_Item> lst)
        {
          
               
                StatusModel.StartStatusUpdate("Removing selected items", lst.Count());

                var docs = lst.Select(x => x.ASYCUDA_Id).ToList();

                foreach (var item in lst.ToList())
                {
                    await DeleteItem(item.Item_Id).ConfigureAwait(false);
                    StatusModel.StatusUpdate();
                }
                foreach (var docId in docs)
                {
                    await ReDoDocumentLineNumbers(docId).ConfigureAwait(false);
                }
                
                StatusModel.StopStatusUpdate();


           
        }

        public async Task DeleteItem(int p)
        {
            //xcuda_Item res;
            using (var ctx = new xcuda_ItemService())
            {
                //res = await ctx.Getxcuda_Item(p.ToString()).ConfigureAwait(false);
                await ctx.Deletexcuda_Item(p.ToString()).ConfigureAwait(false);
            }
           // await DeleteItem(res).ConfigureAwait(false);
        }





        public  async Task<AsycudaDocumentSet> CreateAsycudaDocumentSet()
        {
            using (var ctx = new AsycudaDocumentSetService())
            {
                var doc = await ctx.CreateAsycudaDocumentSet(new AsycudaDocumentSet()).ConfigureAwait(false);
                return doc ;
            }
        }






        public async Task DeleteAsycudaDocument(int ASYCUDA_Id)
        {
            xcuda_ASYCUDA doc = null;
            using (var ctx = new xcuda_ASYCUDAService())
            {
                doc = await ctx.Getxcuda_ASYCUDAByKey(ASYCUDA_Id.ToString()).ConfigureAwait(false);
            }
            await DeleteAsycudaDocument(doc).ConfigureAwait(false);
        }

        public async Task Save_xcuda_PreviousItem(xcuda_PreviousItem pi)
        {
            if (pi == null) return;
            using (var ctx = new xcuda_PreviousItemService())
            {
                await ctx.Updatexcuda_PreviousItem(pi).ConfigureAwait(false);
            }
        }

        public async Task Save_xcuda_Item(xcuda_Item Originalitm)
        {
            if (Originalitm == null) return;
            using (var ctx = new xcuda_ItemService())
            {
                await ctx.Updatexcuda_Item(Originalitm).ConfigureAwait(false);
            }
        }

        public async Task SaveDocument(xcuda_ASYCUDA doc)
        {
            if (doc == null) return;
            using (var ctx = new xcuda_ASYCUDAService())
            {
                await ctx.CleanAndUpdateXcuda_ASYCUDA(doc).ConfigureAwait(false);
            }
        }

        public async Task SaveInventoryItem(InventoryItem item)
        {
            if (item == null) return;
            using (var ctx = new InventoryItemService())
            {
                await ctx.UpdateInventoryItem(item).ConfigureAwait(false);
            }
        }

        public async Task DeleteAsycudaDocument(xcuda_ASYCUDA doc)
        {
            if (doc == null) return;
            await DeleteDocumentSalesAllocations(doc).ConfigureAwait(false);
            await DeleteDocumentPreviousItems(doc).ConfigureAwait(false);
            await DeleteItem(doc).ConfigureAwait(false);
            await DeleteDocument(doc).ConfigureAwait(false);
        }

        private async Task DeleteDocumentSalesAllocations(xcuda_ASYCUDA doc)
        {
            var lst = new List<AsycudaSalesAllocationsEx>();
            using (var ctx = new AsycudaSalesAllocationsExService())
            {
                lst =
                    (await
                        ctx.GetAsycudaSalesAllocationsExBypASYCUDA_Id(doc.ASYCUDA_Id.ToString()).ConfigureAwait(false))
                        .ToList();
            }
            //using (var ctx = new AsycudaSalesAllocationsService())
            //{
            //    lst.AsParallel(new ParallelLinqOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount*2})
            //        .ForAll(itm =>
            //        {
            //            ctx.DeleteAsycudaSalesAllocations(itm.AllocationId.ToString()).Wait();
            //        });
            //}
            if(lst.Count > 0) throw new ApplicationException("Please Remove Sales Allocations before Deleting this IM7");
        }

        private async Task DeleteDocument(xcuda_ASYCUDA doc)
        {
            var docid = doc.ASYCUDA_Id;
            using (var ctx = new xcuda_ASYCUDAService())
            {
                await ctx.Deletexcuda_ASYCUDA(docid.ToString()).ConfigureAwait(false);
            }
            await CalculateDocumentSetFreight(docid).ConfigureAwait(false);
        }

        private  async Task DeleteItem(xcuda_ASYCUDA doc)
        {
            using (var ctx = new xcuda_ItemService())
            {
                foreach (var item in await ctx.Getxcuda_ItemByASYCUDA_Id(doc.ASYCUDA_Id.ToString()).ConfigureAwait(false))
                {
                    await ctx.Deletexcuda_Item(item.Item_Id.ToString()).ConfigureAwait(false);
                }
            }
        }
        //private async Task DeleteItem(xcuda_Item item)
        //{
        //    using (var ctx = new xcuda_ItemService())
        //    {
        //        item.xBondAllocations.Clear();
        //        item.xcuda_PreviousItems.Clear();
        //        await ctx.Updatexcuda_Item(item).ConfigureAwait(false);
        //        await ctx.Deletexcuda_Item(item.Item_Id.ToString()).ConfigureAwait(false);
        //        MessageBus.Default.BeginNotify(DocumentItemDS.MessageToken.xcuda_ItemDeleted, null,
        //                                              new NotificationEventArgs<xcuda_Item>(DocumentItemDS.MessageToken.xcuda_ItemDeleted, item));
        //    }
        //}

        private  async Task DeleteDocumentPreviousItems(xcuda_ASYCUDA doc)
        {
            using (var ctx = new global::DocumentItemDS.Business.Services.xcuda_PreviousItemService())
            {
                //TODO: replace with deletebyAsycuda_id command
                foreach (
                    var item in await ctx.Getxcuda_PreviousItemByASYCUDA_Id(doc.ASYCUDA_Id.ToString()).ConfigureAwait(false))
                {
                    //item.xcuda_Items.Clear();
                    await ctx.Deletexcuda_PreviousItem(item.PreviousItem_Id.ToString()).ConfigureAwait(false);
                    //MessageBus.Default.BeginNotify(DocumentDS.MessageToken.xcuda_PreviousItemDeleted, null,
                    //    new NotificationEventArgs<global::DocumentDS.Business.Entities.xcuda_PreviousItem>(
                    //        DocumentDS.MessageToken.xcuda_PreviousItemDeleted, item));
                }
            }
        }



        //internal  void ExportDocSet(string dir)
        //{
        //    if (dir == null) return;
        //    var d = new DirectoryInfo(Path.GetDirectoryName(dir));
        //    if (d.Exists)
        //    {
        //        foreach (var doc in Instance.CurrentAsycudaDocumentSet.xcuda_ASYCUDA_ExtendedProperties.Select(c => c.xcuda_ASYCUDA).ToList())
        //        {
        //            var a = new ASYCUDA();
        //            a.LoadFromDataBase(doc.ASYCUDA_Id, a);
        //            a.SaveToFile(Path.Combine(d.FullName, doc.xcuda_Declarant.Number + ".xml"));
        //        }

        //    }
        //}

        internal  void ExporttoXML(string f, xcuda_ASYCUDA currentDocument)
        {

            if (currentDocument != null)
            {
                DocToXML(currentDocument, f);
            }
            else
            {
                throw new ApplicationException("Please Select Asycuda Document to Export");
            }
        }

        internal  void DocToXML(xcuda_ASYCUDA doc, string f)
        {

            var a = new Asycuda421.ASYCUDA();
            a.LoadFromDataBase(doc.ASYCUDA_Id, a);
            a.SaveToFile(f);
        }

        public async Task ImportDocuments(AsycudaDocumentSet docSet, IEnumerable<string> fileNames, bool importOnlyRegisteredDocument, bool importTariffCodes, bool noMessages, bool overwriteExisting ,bool linkPi)
        {
                await Task.Run(() =>
                    ImportDocuments(docSet, importOnlyRegisteredDocument, importTariffCodes, noMessages, overwriteExisting,linkPi, fileNames))
                    .ConfigureAwait(false);
        }

        private void ImportDocuments(AsycudaDocumentSet docSet, bool importOnlyRegisteredDocument,
            bool importTariffCodes, bool noMessages,
            bool overwriteExisting, bool linkPi,  IEnumerable<string> fileNames)
        {
            //Asycuda.ASYCUDA.NewAsycudaDocumentSet()
            //StatusModel.RefreshNow();
            var exceptions = new ConcurrentQueue<Exception>();
            Parallel.ForEach(fileNames, new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount*1},
                f => //
                {
                    try
                    {

                        if (ASYCUDA.CanLoadFromFile(f))
                        {
                            LoadAsycuda421(docSet, importOnlyRegisteredDocument, importTariffCodes, noMessages,
                                overwriteExisting, linkPi, f, exceptions);
                        }
                        else
                        {
                            if (!noMessages)
                                throw new ApplicationException(string.Format("Can not Load file '{0}'", f));
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Enqueue(ex);
                    }

                }
                );

            if (exceptions.Count > 0) throw new ApplicationException(exceptions.FirstOrDefault().Message + "|" + exceptions.FirstOrDefault().StackTrace);
        }



        private void LoadAsycuda421(AsycudaDocumentSet docSet, bool importOnlyRegisteredDocument, bool importTariffCodes,
           bool noMessages, bool overwriteExisting, bool linkPi, string f, ConcurrentQueue<Exception> exceptions)
        {
            StatusModel.StatusUpdate();
            try
            {

                var a = Asycuda421.ASYCUDA.LoadFromFile(f);

                if (a != null)
                {
                    var importer = new AsycudaToDataBase421();
                    importer.UpdateItemsTariffCode = importTariffCodes;
                    importer.ImportOnlyRegisteredDocuments = importOnlyRegisteredDocument;
                    importer.OverwriteExisting = overwriteExisting;
                    importer.NoMessages = noMessages;
                    importer.LinkPi = linkPi;
                    importer.SaveToDatabase(a, docSet).Wait();
                }
                //await a.SaveToDatabase(a).ConfigureAwait(false);

                Debug.WriteLine(f);
            }

            catch (Exception Ex)
            {
                if (!noMessages)
                    exceptions.Enqueue(
                        new ApplicationException(
                            string.Format("Could not import file - '{0}. Error:{1} Stacktrace:{2}", f,
                                Ex.InnerException.Message, Ex.InnerException.StackTrace)));
            }
        }

        public async Task ExportDocument(string filename, xcuda_ASYCUDA doc )
        {
            Instance.ExporttoXML(filename, doc);
           
        }

        public  void IM72Ex9Document(string filename)
        {
            try
            {
                var zeroitems = "";
                // create blank asycuda document
                dynamic olddoc;
                if (ASYCUDA.CanLoadFromFile(filename))
                {
                    olddoc = ASYCUDA.LoadFromFile(filename);

                }
                else if (Asycuda421.ASYCUDA.CanLoadFromFile(filename))
                {
                    olddoc = Asycuda421.ASYCUDA.LoadFromFile(filename);
                }
                else
                {
                    
                        throw new ApplicationException(string.Format("Can not Load file '{0}'", filename));
                }


                
                var newdoc = Asycuda421.ASYCUDA.LoadFromFile(filename);

                newdoc.Container = null;

                if (olddoc.Identification.Registration.Date == null)
                {
                    throw new ApplicationException("Document is not Assesed! Convert Assessed Documents only");
                }


                newdoc.Item.Clear();




                var linenumber = 0;
                foreach (var olditem in olddoc.Item)
                {

                    linenumber += 1;


                    // create new entry
                    var i = olditem.Clone();


                    var extemp = ExportTemplates.Where(x => x.Description.ToUpper() == "IM9").FirstOrDefault();
                    if (extemp != null)
                    {
                        var customsProcedure = extemp.Customs_Procedure;
                        if (customsProcedure != null)
                        {
                            i.Tarification.Extended_customs_procedure = customsProcedure.Split('-')[0];
                            i.Tarification.National_customs_procedure = customsProcedure.Split('-')[1];
                        }
                    }

                    i.Previous_doc.Summary_declaration.Text.Clear();
                    i.Previous_doc.Summary_declaration.Text.Add(String.Format("{0} {1} C {2} art. {3}", olddoc.Identification.Office_segment.Customs_clearance_office_code.Text[0],
                                                                                                  DateTime.Parse(olddoc.Identification.Registration.Date).Year.ToString(),
                                                                                                  olddoc.Identification.Registration.Number, linenumber));


                    // create previous item


                    var pitm = new ASYCUDAPrev_decl();
                    pitm.Prev_decl_HS_code = i.Tarification.HScode.Commodity_code;
                    pitm.Prev_decl_HS_prec = "00";
                    pitm.Prev_decl_current_item = linenumber.ToString(); // piggy back the previous item count
                    pitm.Prev_decl_item_number = linenumber.ToString();

                    pitm.Prev_decl_weight = olditem.Valuation_item.Weight_itm.Net_weight_itm.ToString(); //System.Convert.ToDecimal(pline.Net_weight_itm) / System.Convert.ToDecimal(pline.ItemQuantity) * System.Convert.ToDecimal(fa.DutyFreeQuantity);
                    pitm.Prev_decl_weight_written_off = olditem.Valuation_item.Weight_itm.Net_weight_itm.ToString();




                    pitm.Prev_decl_number_packages_written_off = Math.Round(Convert.ToDouble(olditem.Packages.Number_of_packages), 0).ToString();
                    pitm.Prev_decl_number_packages = Math.Round(Convert.ToDouble(olditem.Packages.Number_of_packages), 0).ToString();


                    pitm.Prev_decl_supp_quantity = olditem.Tarification.Supplementary_unit[0].Suppplementary_unit_quantity.ToString();
                    pitm.Prev_decl_supp_quantity_written_off = olditem.Tarification.Supplementary_unit[0].Suppplementary_unit_quantity.ToString();


                    pitm.Prev_decl_country_origin = olditem.Goods_description.Country_of_origin_code;

                    var oq = "";

                    if (string.IsNullOrEmpty(olditem.Tarification.Supplementary_unit[0].Suppplementary_unit_quantity) || olditem.Tarification.Supplementary_unit[0].Suppplementary_unit_quantity == "0")
                    {
                        oq = "1";
                        zeroitems = "ZeroItems";
                    }
                    else
                    {
                        oq = olditem.Tarification.Supplementary_unit[0].Suppplementary_unit_quantity.ToString();
                    }


                    pitm.Prev_decl_ref_value_written_off = (Convert.ToDecimal(olditem.Valuation_item.Total_CIF_itm) / Convert.ToDecimal(oq)).ToString();
                    pitm.Prev_decl_ref_value = (Convert.ToDecimal(olditem.Valuation_item.Total_CIF_itm) / Convert.ToDecimal(oq)).ToString();// * System.Convert.ToDecimal(fa.QUANTITY);
                    pitm.Prev_decl_reg_serial = "C";
                    pitm.Prev_decl_reg_number = olddoc.Identification.Registration.Number;
                    pitm.Prev_decl_reg_year = DateTime.Parse(olddoc.Identification.Registration.Date).Year.ToString();
                    pitm.Prev_decl_office_code = olddoc.Identification.Office_segment.Customs_clearance_office_code.Text[0];

                    newdoc.Prev_decl.Add(pitm);



                    i.Valuation_item.Item_Invoice.Currency_code = "XCD";
                    i.Valuation_item.Item_Invoice.Amount_foreign_currency = olditem.Valuation_item.Total_CIF_itm;
                    i.Valuation_item.Item_Invoice.Amount_national_currency = olditem.Valuation_item.Total_CIF_itm;
                    i.Valuation_item.Statistical_value = olditem.Valuation_item.Total_CIF_itm;

                    newdoc.Item.Add(i);



                }

                newdoc.Identification.Manifest_reference_number = null;
                newdoc.Identification.Type.Type_of_declaration = "Ex";
                newdoc.Identification.Type.Declaration_gen_procedure_code = "9";
                newdoc.Declarant.Reference.Number.Text.Add("Ex9For" + newdoc.Identification.Registration.Number); 
                
                newdoc.Valuation.Gs_Invoice.Currency_code.Text.Add("XCD");
                newdoc.Valuation.Gs_Invoice.Amount_foreign_currency = Math.Round(newdoc.Item.Sum(i => Convert.ToDouble(i.Valuation_item.Total_CIF_itm)), 2).ToString();
                newdoc.Valuation.Gs_Invoice.Amount_national_currency = Math.Round(newdoc.Item.Sum(i => Convert.ToDouble(i.Valuation_item.Total_CIF_itm)), 2).ToString();

                var oldfile = new FileInfo(filename);
                newdoc.SaveToFile(Path.Combine(oldfile.DirectoryName, oldfile.Name.Replace(oldfile.Extension, "") + "-Ex9" + zeroitems + oldfile.Extension));
            }
            catch (Exception Ex)
            {
                throw;
            }

        }

        internal async Task ExportDocSet(int AsycudaDocumentSetId, string directoryName)
        {
           var docset = await GetAsycudaDocumentSet(AsycudaDocumentSetId,  new List<string>()
                   {
                       "xcuda_ASYCUDA_ExtendedProperties",
                       "xcuda_ASYCUDA_ExtendedProperties.xcuda_ASYCUDA",
                       "xcuda_ASYCUDA_ExtendedProperties.xcuda_ASYCUDA.xcuda_Declarant"
                       }).ConfigureAwait(false);
           ExportDocSet(docset, directoryName);
        }

        public  async Task<AsycudaDocumentSet> GetAsycudaDocumentSet(int AsycudaDocumentSetId, List<string> includesLst )
        {
            
            using (var ctx = new AsycudaDocumentSetService())
            {
               return await ctx.GetAsycudaDocumentSetByKey(AsycudaDocumentSetId.ToString(),
                   new List<string>()
                   {
                       "xcuda_ASYCUDA_ExtendedProperties",
                       "xcuda_ASYCUDA_ExtendedProperties.xcuda_ASYCUDA",
                       "xcuda_ASYCUDA_ExtendedProperties.xcuda_ASYCUDA.xcuda_Declarant",
                       
                        "Customs_Procedure",
                        "Document_Type"
                   }).ConfigureAwait(false);
            }
        }

        internal  void ExportDocSet(AsycudaDocumentSet docSet, string directoryName)
        {
            
                            StatusModel.StartStatusUpdate("Exporting Files", docSet.Documents.Count());
                            Parallel.ForEach(docSet.Documents, doc =>
                            {
                                //if (doc.xcuda_Item.Any() == true)
                                //{
                                try
                                {
                                     Instance.DocToXML(doc, Path.Combine(directoryName, doc.ReferenceNumber + ".xml"));
                                                                    StatusModel.StatusUpdate();
                                }
                                catch (Exception)
                                {
                                    
                                    //throw;
                                }
                               
                                ////}
                            });
              
        }

         AsycudaDocumentSet _currentAsycudaDocumentSet = null;
        //public  AsycudaDocumentSet CurrentAsycudaDocumentSet
        //{
        //    get
        //    {
        //        if (
        //            QuerySpace.CoreEntities.DataModels.BaseDataModel.Instance.CurrentAsycudaDocumentSetEx != null &&
        //            (_currentAsycudaDocumentSet == null ||
        //                _currentAsycudaDocumentSet.AsycudaDocumentSetId != QuerySpace.CoreEntities.DataModels.BaseDataModel.Instance.CurrentAsycudaDocumentSetEx.AsycudaDocumentSetId))
        //        {
        //            _currentAsycudaDocumentSet = GetAsycudaDocumentSet(QuerySpace.CoreEntities.DataModels.BaseDataModel.Instance.CurrentAsycudaDocumentSetEx.AsycudaDocumentSetId, new List<string>()
        //            {
        //                "xcuda_ASYCUDA_ExtendedProperties",
        //                "Customs_Procedure",
        //                "Document_Type"
        //            }).Result;
        //            return _currentAsycudaDocumentSet;
        //        }
        //        else
        //        {
        //            return _currentAsycudaDocumentSet;
        //        }
        //    }
        //    set
        //    {
        //       _currentAsycudaDocumentSet = value;
               
        //       MessageBus.Default.BeginNotify<string>(MessageToken.CurrentAsycudaDocumentSetExIDChanged, null,
        //           new NotificationEventArgs<string>(MessageToken.CurrentAsycudaDocumentSetExIDChanged,
        //                                _currentAsycudaDocumentSet != null ? _currentAsycudaDocumentSet.AsycudaDocumentSetId.ToString() : "0"));

        //       MessageBus.Default.BeginNotify(MessageToken.AsycudaDocumentsChanged, null,
        //               new NotificationEventArgs(MessageToken.AsycudaDocumentsChanged));

        //       MessageBus.Default.BeginNotify(MessageToken.AsycudaDocumentSetExsChanged, null,
        //               new NotificationEventArgs(MessageToken.AsycudaDocumentSetExsChanged));
              
        //    }
        //}




        //public  xcuda_ASYCUDA CurrentAsycudaDocument
        //{
        //    get
        //    {
        //        if (QuerySpace.CoreEntities.DataModels.BaseDataModel.Instance.CurrentAsycudaDocument != null)
        //        {
        //            using (var ctx = new xcuda_ASYCUDAService())
        //            {
        //                return ctx.Getxcuda_ASYCUDA(QuerySpace.CoreEntities.DataModels.BaseDataModel.Instance.CurrentAsycudaDocument.ASYCUDA_Id.ToString(),
        //                    new List<string>()
        //                    {
        //                        "xcuda_Declarant"
        //                    }).Result;
        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public  InventoryItem CurrentInventoryItem
        //{
        //    get
        //    {
        //        if (QuerySpace.InventoryQS.DataModels.BaseDataModel.Instance.CurrentInventoryItemsEx != null)
        //        {
        //            using (var ctx = new InventoryItemService())
        //            {
        //                return ctx.GetInventoryItem(QuerySpace.InventoryQS.DataModels.BaseDataModel.Instance.CurrentInventoryItemsEx.ItemNumber.ToString()).Result;
        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public  EntryDataDetails CurrentEntryDataDetail
        //{
        //    get
        //    {
        //        using (var ctx = new EntryDataDetailsService())
        //        {
        //            return ctx.GetEntryDataDetails(QuerySpace.EntryDataQS.DataModels.BaseDataModel.Instance.CurrentEntryDataDetailsEx.EntryDataDetailsId.ToString()).Result;
        //        }
        //    }            
        //}

        //public  AsycudaSalesAllocations CurrentAsycudaSalesAllocation
        //{
        //    get
        //    {
        //        if (QuerySpace.AllocationQS.DataModels.BaseDataModel.Instance.CurrentAsycudaSalesAllocationsEx != null)
        //        {
        //            using (var ctx = new AsycudaSalesAllocationsService())
        //            {
        //                return ctx.GetAsycudaSalesAllocations(QuerySpace.AllocationQS.DataModels.BaseDataModel.Instance.CurrentAsycudaSalesAllocationsEx.AllocationId.ToString()).Result;
        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public  TariffCode CurrentTariffCode
        //{
        //    get
        //    {
        //        if (QuerySpace.InventoryQS.DataModels.BaseDataModel.Instance.CurrentTariffCodes != null)
        //        {
        //            using (var ctx = new TariffCodeService())
        //            {
        //                return ctx.GetTariffCode(QuerySpace.InventoryQS.DataModels.BaseDataModel.Instance.CurrentTariffCodes.TariffCode.ToString()).Result;
        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        
         public ApplicationSettings CurrentApplicationSettings { get; set; }
        //{
        //    get
        //    {
        //        return QuerySpace.CoreEntities.DataModels.BaseDataModel.Instance.CurrentApplicationSettings;
        //    }
        //}
         IEnumerable<ExportTemplate> _exportTemplates = null;
        public  IEnumerable<ExportTemplate> ExportTemplates
        {
            get
            {
                if (_exportTemplates == null)
                {
                    using (var ctx = new ExportTemplateService())
                    {
                        _exportTemplates =  ctx.GetExportTemplates().Result;
                    }
                }
                return _exportTemplates;
            }
        }
         IEnumerable<Customs_Procedure> _customs_Procedures = null;
        public  IEnumerable<Customs_Procedure> Customs_Procedures
        {
            get
            {
                if (_customs_Procedures == null)
                {
                    using (var ctx = new Customs_ProcedureService())
                    {
                       _customs_Procedures = ctx.GetCustoms_Procedure().Result;
                    }
                }
                return _customs_Procedures;
            }
        }

         IEnumerable<Document_Type> _document_Types = null;
        private readonly CreateIM9 _createIm9;

        public  IEnumerable<Document_Type> Document_Types
        {
            get
            {
                if (_document_Types == null)
                {
                    using (var ctx = new Document_TypeService())
                    {
                        _document_Types = ctx.GetDocument_Type().Result;
                    }
                }
                return _document_Types;
            }
        }

        public async Task<IEnumerable<EntryDataDetails>> GetSelectedPODetails(
            IEnumerable<EntryDataDetailsEx> lst)
        {
            var res = new List<EntryDataDetails>();
            if (lst.Any())
            {
                using (var ctx = new EntryDataDetailsService())
                {
                    foreach (var item in lst.Where(x => x != null))
                    {

                        res.Add(await ctx.GetEntryDataDetailsByKey(item.EntryDataDetailsId.ToString(),
                            new List<string>()
                            {
                                "EntryDataDetailsEx",
                                "InventoryItems",
                                "EntryData.ContainerEntryData"
                               //, "InventoryItem.TariffCodes.TariffCategory.TariffSupUnitLkps"
                            }).ConfigureAwait(false));
                    }
                }
               
            }
             return res.OrderBy(x => x.EntryDataDetailsId);
        }

        public async Task<IEnumerable<EntryDataDetails>> GetSelectedPODetails(IEnumerable<string> elst )
        {
                
                var res = new List<EntryDataDetails>();
               if (elst.Any())
                    {
                        using (var ctx = new EntryDataDetailsService())
                        {
                            foreach (var item in elst.Where(x => x != null))
                            {

                                res.AddRange(await ctx.GetEntryDataDetailsByEntryDataId(item, new List<string>()
                                {
                                    "EntryDataDetailsEx",
                                    "InventoryItems",
                                    "EntryData.ContainerEntryData"
                                   // ,"InventoryItems.TariffCodes.TariffCategory.TariffSupUnitLkps"
                                }).ConfigureAwait(false));
                            }
                        }
                    }
               return res.OrderBy(x => x.EntryDataDetailsId);
        }



        //public  string CurrentAsycudaItemEntryId
        //{
        //    get
        //    {
        //        return QuerySpace.CoreEntities.DataModels.BaseDataModel.Instance.CurrentAsycudaDocumentItemID ?? null;
        //    }

        //}

        public async Task SaveAsycudaDocumentItem(AsycudaDocumentItem asycudaDocumentItem)
        {
            if (asycudaDocumentItem == null) return;
            //get the original item
          var i =  await GetDocumentItem(asycudaDocumentItem.Item_Id, new List<string>()
          {
              "xcuda_Tarification.xcuda_HScode",
              "xcuda_Valuation_item.xcuda_Weight_itm",
              "xcuda_PreviousItem"
          }).ConfigureAwait(false);
            if (i.xcuda_Goods_description.TrackingState == TrackingState.Added) i.xcuda_Goods_description = null;
            if (i.xcuda_Previous_doc.TrackingState == TrackingState.Added) i.xcuda_Previous_doc = null;
            i.StartTracking();
    
           asycudaDocumentItem.ModifiedProperties = null;
            // null for now cuz there are no navigation properties involved.
            
            i.InjectFrom(asycudaDocumentItem);

            if (i.xcuda_PreviousItem != null)
            {
                i.xcuda_PreviousItem.Net_weight = i.Net_weight;
            }
       
            await Save_xcuda_Item(i).ConfigureAwait(false);

        }

        private async Task<xcuda_Item> GetDocumentItem(int item_Id, List<string> includeLst)
        {
            using (var ctx = new xcuda_ItemService(){StartTracking = true})
            {
                xcuda_Item i = null;
                i = await ctx.Getxcuda_ItemByKey(item_Id.ToString(), includeLst).ConfigureAwait(false);
                return i;
            }
        }

        internal async Task SaveEntryDataDetailsEx(EntryDataDetailsEx entryDataDetailsEx)
        {
            using (var ctx = new EntryDataDetailsService())
            {
                var i =
                   await ctx.GetEntryDataDetailsByKey(entryDataDetailsEx.EntryDataDetailsId.ToString()).ConfigureAwait(false);
                i.InjectFrom(entryDataDetailsEx);
                await ctx.UpdateEntryDataDetails(i).ConfigureAwait(false);

               
                //MessageBus.Default.BeginNotify(QuerySpace.EntryDataQS.MessageToken.CurrentEntryDataDetailsExChanged, this, new NotificationEventArgs<EntryDataDetailsEx>(QuerySpace.EntryDataQS.MessageToken.CurrentEntryDataDetailsExChanged, null));
            } 
        }

        public async Task SaveAsycudaDocument(AsycudaDocument asycudaDocument)
        {
            asycudaDocument.ModifiedProperties = null;
            if (asycudaDocument == null) return;
            //get the original item
            var i = await GetDocument(asycudaDocument.ASYCUDA_Id, new List<string>()
          {
              "xcuda_ASYCUDA_ExtendedProperties",
                "xcuda_Identification",
                "xcuda_Valuation.xcuda_Gs_Invoice",
                "xcuda_Declarant",
                "xcuda_General_information.xcuda_Country",
                "xcuda_Property"
          }).ConfigureAwait(false);
            i.StartTracking();
            // null for now cuz there are no navigation properties involved.
            i.InjectFrom(asycudaDocument);
            
            await Save_xcuda_ASYCUDA(i).ConfigureAwait(false);
        }

        private async Task Save_xcuda_ASYCUDA(xcuda_ASYCUDA i)
        {
            if (i == null) return;
            using (var ctx = new xcuda_ASYCUDAService())
            {
                await ctx.CleanAndUpdateXcuda_ASYCUDA(i).ConfigureAwait(false);
            }
        }

        
        public async Task DeleteDocumentCt(DocumentCT da)
        {
            if (da.Document.TrackingState == TrackingState.Added) return;
            using (var ctx = new WaterNutDBEntities())
            {
               await ctx.ExecuteStoreCommandAsync(@"delete from xcuda_Item
                                               where ASYCUDA_Id = @ASYCUDA_Id

                                               delete from xcuda_ASYCUDA
                                               where ASYCUDA_Id = @ASYCUDA_Id", new SqlParameter("@ASYCUDA_Id", SqlDbType.Int).Value = da.Document.ASYCUDA_Id).ConfigureAwait(false);
            }
            
        }

        public async Task SaveEntryPreviousItems(List<CoreEntities.Business.Entities.EntryPreviousItems> epi)
        {
            using (var ctx = new CoreEntitiesContext())
            {
                ctx.ApplyChanges(epi);
                ctx.SaveChanges();
            }
        }

        #region IAsyncInitialization Members

        public static Task Initialization { get; private set; }

     
        #endregion



     
    }

    public class EntryDataDetailSummary
    {
        public string EntryDataId { get; set; }
        public int EntryDataDetailsId { get; set; }
        public DateTime EntryDataDate { get; set; }
    }
}
