using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllocationDS.Business.Entities;
using Core.Common.Data;
using InventoryDS.Business.Entities;
using Core.Common.UI;
using AllocationDS.Business.Services;
using MoreLinq;
using TrackableEntities;
using TrackableEntities.EF6;
using SubItems = AllocationDS.Business.Entities.SubItems;
using xcuda_Item = AllocationDS.Business.Entities.xcuda_Item;


namespace WaterNut.DataSpace
{
    public partial class AllocationsBaseModel
    {

        private static readonly AllocationsBaseModel instance;
        static AllocationsBaseModel()
        {
            instance = new AllocationsBaseModel();
        }

        private DataCache<InventoryItemAlias> _inventoryAliasCache;

        public static AllocationsBaseModel Instance
        {
            get { return instance; }
        }

        public DataCache<InventoryItemAlias> InventoryAliasCache
        {
            get {
                return _inventoryAliasCache ??
                       (_inventoryAliasCache =
                           new DataCache<InventoryItemAlias>(
                               AllocationDS.DataModels.BaseDataModel.Instance.SearchInventoryItemAlias(
                                   new List<string>() {"All"}, null).Result));
            }
            set { _inventoryAliasCache = value; }
        }

        internal class ItemSales
        {
            public string Key { get; set; }
            public List<EntryDataDetails> SalesList { get; set; }
        }

        internal class InventoryItemSales
        {
            public InventoryItem InventoryItem { get; set; }
            public List<EntryDataDetails> SalesList { get; set; }
        }

        internal class ItemEntries
        {
            public string Key { get; set; }
            public List<xcuda_Item> EntriesList { get; set; }
        }

        internal class ItemSet
        {
            public string Key { get; set; }
            public List<EntryDataDetails> SalesList { get; set; }
            public List<xcuda_Item> EntriesList { get; set; }
        }


        public async Task AllocateSales(bool itemDescriptionContainsAsycudaAttribute)
        {
            try
            {
                StatusModel.Timer("Clear All Existing Allocations");

                using (var ctx = new AllocationDSContext())
                {
                    ctx.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                        "delete from AsycudaSalesAllocations" +
                        "\r\n\r\n" +
                        "update xcuda_Item" + "\r\n" +
                        "set DFQtyAllocated = 0, DPQtyAllocated = 0\r\n\r\n\r\n" +
                        "update EntryDataDetails\r\n" + "set QtyAllocated = 0\r\n\r\n" +
                        "update xcuda_PreviousItem\r\nset QtyAllocated = 0\r\n\r\n" +
                        "update SubItems \r\nset QtyAllocated = 0");
                }

                    StatusModel.Timer("Allocating Sales");
               
                if (itemDescriptionContainsAsycudaAttribute == true)
                {
                    await AllocateSalesWhereItemDescriptionContainsAsycudaAttribute().ConfigureAwait(false);
                }
                else
                {
                    await AllocateSalesByMatchingSalestoAsycudaEntriesOnItemNumber().ConfigureAwait(false);
                }

                await MarkErrors().ConfigureAwait(false);

                StatusModel.StopStatusUpdate();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private async Task MarkErrors()
        {
           // MarkNoAsycudaEntry();
                
                        IEnumerable<xcuda_Item> lst; //"EX"
                        using (var ctx = new xcuda_ItemService())
                        {
                            lst = await ctx.Getxcuda_ItemByExpressionNav(
                                "All",
                                // "xcuda_Tarification.xcuda_HScode.Precision_4 == \"1360\"",
                                new Dictionary<string, string>()
                                {
                                    {
                                        "AsycudaDocument",
                                        (BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate.HasValue
                                            ? string.Format("AssessmentDate >= \"{0}\" && ",
                                                BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate)
                                            : "") +
                                        "(CNumber != null || IsManuallyAssessed == true) && (Extended_customs_procedure == \"7000\" || Extended_customs_procedure == \"9000\") && DoNotAllocate != true"
                                    }
                                }
                                , new List<string>()
                                {
                                    "AsycudaDocument",
                                    "xcuda_Tarification.xcuda_HScode",
                                    "xcuda_Tarification.xcuda_Supplementary_unit",
                                    "SubItems",
                                    "xcuda_Goods_description",
                                }).ConfigureAwait(false);
                        }
                        var imAsycudaEntries = lst as IList<xcuda_Item> ?? lst.ToList();
                        MarkOverAllocatedEntries(imAsycudaEntries);


            





        }

        private async Task AllocateSalesByMatchingSalestoAsycudaEntriesOnItemNumber()
        {
            var itemSets = await MatchSalestoAsycudaEntriesOnItemNumber().ConfigureAwait(false);
          
            StatusModel.StartStatusUpdate("Allocating Item Sales", itemSets.Count());
            var t = 0;
            var exceptions = new ConcurrentQueue<Exception>();
            Parallel.ForEach(itemSets.Values
                                    //.Where(x => "10204972, 3706".Contains(x.Key))
                                     //.Where(x => "FAA/SCPI18X112".Contains(x.ItemNumber))//SND/IVF1010MPSF,BRG/NAVICOTE-GL,
                                     , new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount * 1 }, itm => //.Where(x => x.ItemNumber == "AT18547")
             {
            //     foreach (var itm in itemSets.Values)//.Where(x => "FAA/SCPI18X112".Contains(x.ItemNumber))
            //{
                try
            {
                    StatusModel.StatusUpdate();
                    AllocateSalestoAsycudaByKey(itm.SalesList,
                        //.SalesList.Where(x => x.DoNotAllocate != true).ToList()
                        itm.EntriesList).Wait();
                }
                catch (Exception ex)
                {

                    exceptions.Enqueue(
                                new ApplicationException(
                                    string.Format("Could not Allocate - '{0}. Error:{1} Stacktrace:{2}", itm.Key,
                                        ex.Message, ex.StackTrace)));
                }

              //   };


             });


            var subitms = itemSets.Values.Where(x => x != null && x.EntriesList != null).SelectMany(x => x.EntriesList).SelectMany(x => x.SubItems)
                    .Where(x => x != null && x.ChangeTracker != null)
                    .ToList();

            await SaveSubItems(subitms).ConfigureAwait(false);

            var alst =
                itemSets.Values.Where(x => x != null && x.EntriesList != null).SelectMany(x => x.EntriesList)
                    .Where(x => x != null && x.ChangeTracker != null)
                    .ToList();
           
           await SaveAsycudaEntries(alst).ConfigureAwait(false);

           var slst =
               itemSets.Values.Where(x => x != null && x.SalesList != null).SelectMany(x => x.SalesList)
                   .Where(x => x != null && x.ChangeTracker != null)
                   .ToList();

           await SaveEntryDataDetails(slst).ConfigureAwait(false);

           //await MarkOverAllocatedEntries(alst).ConfigureAwait(false);

           // await MarkNoAsycudaEntry(alst).ConfigureAwait(false);


            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }


        //private async Task AllocateSalesByMatchingSalestoAsycudaEntriesOnDescription()
        //{
        //    var itemSets = await MatchSalestoAsycudaEntriesOnDescription().ConfigureAwait(false);

        //    StatusModel.StartStatusUpdate("Allocating Item Sales", itemSets.Count());
        //    var t = 0;
        //    var exceptions = new ConcurrentQueue<Exception>();
        //    Parallel.ForEach(itemSets.Values
        //        // .Where(x => "Paint-B Micron 66 Bl Ga".Contains(x.Key))
        //        //.Where(x => "FAA/SCPI18X112".Contains(x.ItemNumber))//SND/IVF1010MPSF,BRG/NAVICOTE-GL,
        //        , new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount * 1 }, itm => //.Where(x => x.ItemNumber == "AT18547")
        //        {
        //            //     foreach (var itm in itemSets.Values)//.Where(x => "FAA/SCPI18X112".Contains(x.ItemNumber))
        //            //{
        //            try
        //            {
        //                StatusModel.StatusUpdate();
        //                AllocateSalestoAsycudaByKey(itm.SalesList,
        //                    //.SalesList.Where(x => x.DoNotAllocate != true).ToList()
        //                    itm.EntriesList).Wait();
        //            }
        //            catch (Exception ex)
        //            {

        //                exceptions.Enqueue(
        //                    new ApplicationException(
        //                        string.Format("Could not Allocate - '{0}. Error:{1} Stacktrace:{2}", itm.Key,
        //                            ex.Message, ex.StackTrace)));
        //            }

        //            //   };


        //        });


        //    var subitms = itemSets.Values.Where(x => x != null && x.EntriesList != null).SelectMany(x => x.EntriesList).SelectMany(x => x.SubItems)
        //        .Where(x => x != null && x.ChangeTracker != null)
        //        .ToList();

        //    await SaveSubItems(subitms).ConfigureAwait(false);

        //    var alst =
        //        itemSets.Values.Where(x => x != null && x.EntriesList != null).SelectMany(x => x.EntriesList)
        //            .Where(x => x != null && x.ChangeTracker != null)
        //            .ToList();

        //    await SaveAsycudaEntries(alst).ConfigureAwait(false);

        //    var slst =
        //        itemSets.Values.Where(x => x != null && x.SalesList != null).SelectMany(x => x.SalesList)
        //            .Where(x => x != null && x.ChangeTracker != null)
        //            .ToList();

        //    await SaveEntryDataDetails(slst).ConfigureAwait(false);

        //    //await MarkOverAllocatedEntries(alst).ConfigureAwait(false);
        //    //await MarkNoAsycudaEntry(alst).ConfigureAwait(false);

        //    if (exceptions.Count > 0) throw new AggregateException(exceptions);
        //}


        private async Task AllocateSalesWhereItemDescriptionContainsAsycudaAttribute()
        {


            StatusModel.Timer("Loading Sales Data...");
            var sales = (await GetSales().ConfigureAwait(false)).ToList();//.Where(x => x.ItemDescription.Contains("26196-0008"))

            StatusModel.Timer("Loading Asycuda Data...");
            var IMAsycudaEntries = (await GetAllAsycudaEntries().ConfigureAwait(false)).ToList();

           
            StatusModel.StartStatusUpdate("Allocating Sales", sales.Count());
            var t = 0;

            var exceptions = new ConcurrentQueue<Exception>();

            for (int i = 0; i < sales.Count(); i++)
            {
                var g = sales.ElementAtOrDefault(i);

               
                //Parallel.ForEach(salesGrps,
                //    new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount}, g =>
                //    {
                try
                {
                    var salesDescrip = g.ItemNumber + "|" + g.ItemDescription;
                    
                    var strs = salesDescrip.Split('|');
  
                    //string attrib = strs.Length >= 4
                    //    ? strs[3].ToUpper().Replace(" ", "")
                    //    : null;

                    string attrib = strs.LastOrDefault();


                    StatusModel.StatusUpdate();
                    var alst = GetAsycudaEntriesWithItemNumber(IMAsycudaEntries, attrib, salesDescrip,
                        new List<string>(){g.ItemNumber}).ToList(); //

                    var slst = new List<EntryDataDetails>(){g};
                    
                    if (slst.Any())
                        AllocateSalestoAsycudaByKey(slst, alst).Wait();


                    Debug.WriteLine(g.ItemDescription + " " + DateTime.Now.ToShortTimeString());
                }
                catch (Exception ex)
                {

                    exceptions.Enqueue(ex);
                }
            }
            ///  });

            //await MarkOverAllocatedEntries(IMAsycudaEntries).ConfigureAwait(false);
            //await MarkNoAsycudaEntry(IMAsycudaEntries).ConfigureAwait(false);


            await SaveAsycudaEntries(IMAsycudaEntries.Where(x => x.ChangeTracker != null)).ConfigureAwait(false);

            await SaveEntryDataDetails(sales.Where(x => x.ChangeTracker != null)).ConfigureAwait(false);

            if (exceptions.Count > 0) throw new AggregateException(exceptions);
            //  );
        }

        private void MarkOverAllocatedEntries(IEnumerable<xcuda_Item> IMAsycudaEntries)
        {


            try
            {
                if (IMAsycudaEntries == null || !IMAsycudaEntries.Any()) return;
                var alst = IMAsycudaEntries.ToList();
                if (alst.Any())
                    Parallel.ForEach(alst.Where(x => x != null && ((x.DFQtyAllocated + x.DPQtyAllocated) > Convert.ToDouble(x.ItemQuantity)))
                        ,
                        new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount*1}, i =>
                        {
                            using (var ctx = new AllocationDSContext() {StartTracking = true})
                            {


                                if (ctx.AsycudaSalesAllocations == null) return;

                                var lst =
                                    ctx.AsycudaSalesAllocations.Where(
                                            x => x != null && x.PreviousItem_Id == i.Item_Id)
                                        .OrderByDescending(x => x.AllocationId)
                                        .Include(x => x.EntryDataDetails)
                                        .Include(x => x.EntryDataDetails.EntryDataDetailsEx)
                                        .Include(x => x.PreviousDocumentItem).ToList();

                                foreach (var allo in lst)
                                {
                                    var tot = i.QtyAllocated - i.ItemQuantity;
                                    var r = tot > allo.QtyAllocated ? allo.QtyAllocated : tot;
                                    if (i.QtyAllocated > i.ItemQuantity)
                                    {


                                        allo.QtyAllocated -= r;
                                        allo.EntryDataDetails.QtyAllocated -= r;
                                        if (allo.EntryDataDetails.EntryDataDetailsEx.DutyFreePaid == "Duty Free")
                                        {
                                            allo.PreviousDocumentItem.DFQtyAllocated -= r;
                                            i.DFQtyAllocated -= r;
                                        }
                                        else
                                        {
                                            allo.PreviousDocumentItem.DPQtyAllocated -= r;
                                            i.DPQtyAllocated -= r;
                                        }
                                        if (allo.QtyAllocated == 0)
                                        {
                                            allo.QtyAllocated = r; //add back so wont disturb calculations
                                            allo.Status = $"Over Allocated Entry by {r}";
                                        }
                                        else
                                        {
                                            var nallo = new AsycudaSalesAllocations()
                                            {
                                                QtyAllocated = r,
                                                Status = $"Over Allocated Entry by {r}",
                                                EntryDataDetailsId = allo.EntryDataDetailsId,
                                                PreviousItem_Id = allo.PreviousItem_Id,
                                                TrackingState = TrackingState.Added
                                            };
                                            ctx.ApplyChanges(nallo);
                                            break;
                                        }

                                    }


                                }

                                lst.Where(x => x.QtyAllocated == 0 && string.IsNullOrEmpty(x.Status))
                                    .ForEach(x => ctx.AsycudaSalesAllocations.Remove(x));


                                ctx.SaveChanges();
                            }
                        });

            }
            catch (Exception)
            {
                throw;
            }

        
        }

        private void MarkNoAsycudaEntry()
        {
            try
            {

                using (var ctx = new AllocationDSContext() {StartTracking = true})
                {
                    var res = ctx.EntryDataDetails.GroupBy(x => x.ItemNumber).Where(x => !x.SelectMany(z => z.AsycudaSalesAllocations).Any());
                    foreach (var x in res)
                    {
                        foreach (var z in x)
                        {
                            ctx.Database.ExecuteSqlCommand($"INSERT INTO AsycudaSalesAllocations (EntryDataDetailsId, Status, QtyAllocated, EANumber, SANumber) VALUES({z.EntryDataDetailsId},'No Asycuda Entry Found',{z.Quantity},0,0)");
                        }
                    }
                }


            }
            catch (Exception)
            {
                throw;
            }


        }

        private async Task SaveEntryDataDetails(IEnumerable<EntryDataDetails> sales)
        {
            await Task.Run(() => sales.AsParallel().ForAll(itm =>
            {
                using (var ctx = new AllocationDSContext())
                {
                    ctx.ApplyChanges(itm);
                    ctx.SaveChanges();
                }
            })).ConfigureAwait(false);
        }

        private async Task SaveSubItems(IEnumerable<SubItems> itms)
        {
            await Task.Run(() => itms.AsParallel().ForAll(itm =>
            {
                using (var ctx = new AllocationDSContext())
                {
                    ctx.ApplyChanges(itm);
                    ctx.SaveChanges();
                }
            })).ConfigureAwait(false);
        }

        private async Task SaveAsycudaEntries(IEnumerable<xcuda_Item> IMAsycudaEntries)
        {
            await Task.Run(() =>
            {
                IMAsycudaEntries.Where(x => x.ChangeTracker != null).AsParallel().ForAll(itm =>
                {
                   // if(itm.DFQtyAllocated < 10000 && itm.DPQtyAllocated < 10000)
                        using (var ctx = new AllocationDSContext())
                    {
                        ctx.ApplyChanges(itm);
                        ctx.SaveChanges();
                    }
                });
            }).ConfigureAwait(false);
        }


        public List<xcuda_Item> GetAsycudaEntriesWithItemNumber(IEnumerable<xcuda_Item> IMAsycudaEntries, string attrib,
            string salesDescrip, List<string> itemNumber)
        {
            var alst = new List<xcuda_Item>();
            var taskLst = new List<Task>();
            if (attrib != null)
            {
                taskLst.Add(Task.Run(() =>
                {
                    alst.AddRange(IMAsycudaEntries.Where(x => //x.QtyAllocated != x.ItemQuantity &&
                                                                     x.SubItems.Any() == false &&
                                                                     (x.AttributeOnlyAllocation != null &&
                                                                      x.AttributeOnlyAllocation == true)
                                                                     && x.ItemNumber.ToLower().Replace(" ", "").Replace("-", "") == attrib.ToLower().Replace("-", "")));
                }));
            }
            taskLst.Add(Task.Run(() =>
            {
                alst.AddRange(IMAsycudaEntries.Where(x => //x.QtyAllocated != x.ItemQuantity &&
                                                                 x.SubItems.Any() == false &&
                                                                 (x.AttributeOnlyAllocation == null ||
                                                                  x.AttributeOnlyAllocation != true)
                                                                 &&
                                                                 salesDescrip.ToLower().Replace(" ", "").Replace("-", "")
                                                                     .Contains(x.ItemNumber.ToLower().Replace(" ", "").Replace("-", ""))));
            }));

            //item alias
            taskLst.Add(Task.Run(() =>
            {
                var aliasLst = InventoryAliasCache.Data.Where(x => salesDescrip.ToLower().Replace(" ", "").Replace("-", "")
                    .Contains(x.AliasName.ToLower().Replace(" ", "").Replace("-", "")));
                var alias = new StringBuilder();
                foreach (var itm in aliasLst)
                {
                    alias.Append(itm.ItemNumber + ",");
                }
                alst.AddRange(IMAsycudaEntries.Where(x => //x.QtyAllocated != x.ItemQuantity &&
                                                                 x.SubItems.Any() == false &&
                                                                 (x.AttributeOnlyAllocation == null ||
                                                                  x.AttributeOnlyAllocation != true)
                                                                 && alias.ToString().Contains(x.ItemNumber)));
            }));

            taskLst.Add(Task.Run(() =>
            {
                var sublst = IMAsycudaEntries.Where(x => x.SubItems.Any() == true
                                                         &&
                                                         x.SubItems.Any(z => itemNumber.Contains(z.ItemNumber.ToLower()))).ToList();
               if(sublst.Any()) alst.AddRange(sublst);//&& z.QtyAllocated != z.Quantity)
            }));
            Task.WhenAll(taskLst).Wait();
            return alst.Distinct().OrderBy(x => x.AsycudaDocument.RegistrationDate).ToList();
        }



        private async Task<List<xcuda_Item>> GetAllAsycudaEntries()
        {
            var alst = new List<xcuda_Item>();
            using (var ctx = new xcuda_ItemService())
            {
                alst.AddRange(ctx.Getxcuda_ItemByExpressionLst(
                    new List<string>()
                    {
                        (BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate.HasValue ? string.Format("AsycudaDocument.RegistrationDate >= \"{0}\"", BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate) : "AsycudaDocument.RegistrationDate >= \"1/1/2010\"") ,
                        "DoNotAllocate == null || DoNotAllocate != true",
                        "(AsycudaDocument.Extended_customs_procedure == \"7000\" || AsycudaDocument.Extended_customs_procedure == \"9000\")",
                       //"SubItems.Count > 0",
                       // "AttributeOnlyAllocation == true"
                        //string.Format("EX.Precision_4.ToUpper() == \"{0}\"", attrib)
                    },
                    new List<string>() { "SubItems",
                        "AsycudaDocument",
                        "xcuda_Tarification.xcuda_HScode",
                        "xcuda_Tarification.xcuda_Supplementary_unit"    
                    }).Result.Distinct());//, "EX"

            }

            return alst;
        }

        private static async Task<List<EntryDataDetails>> GetSales()
        {
            List<EntryDataDetails> sales = null;
            using (var ctx = new EntryDataDetailsService())
            {
                sales = (await ctx.GetEntryDataDetailsByExpressionLst(new List<string>()
                {
                  // "EntryDataDetailsId == 85371",
                  (BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate.HasValue ? string.Format("Sales.EntryDataDate >= \"{0}\"", BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate) : "Sales.EntryDataDate >= \"1/1/2010\""),
                    "Sales.INVNumber != null",
                    "QtyAllocated != Quantity",
                    "DoNotAllocate != true"
                },
                    new List<string>() { "Sales", "AsycudaSalesAllocations" }).ConfigureAwait(false))
                    //.Where(x => Convert.ToDouble(x.QtyAllocated) != Convert.ToDouble(x.Quantity) && x.DoNotAllocate != true)
                    .OrderBy(x => x.Sales.EntryDataDate).ToList();//.Take(100)
            }
            return sales;
        }

        private static void GetSubItems(List<xcuda_Item> alst, EntryDataDetails salesDetails)
        {
            using (var ctx = new SubItemsService())
            {
                alst.AddRange(
                    ctx.GetSubItemsByExpressionLst(
                        new List<string>() {string.Format("ItemNumber == \"{0}\"", salesDetails.ItemNumber)}
                        , new List<string>() { "xcuda_Item", "xcuda_Item.AsycudaDocument", "xcuda_Item.xcuda_Tarification.xcuda_HScode" }).Result//"xcuda_Item.EX",
                        //.Where(y => y.ItemNumber == salesDetails.ItemNumber)
                        .Select(x => x.xcuda_Item).ToList());
            }
        }

        private static List<xcuda_Item> GetAsycudaEntriesWithItemNumber(string attrib, string salesDescrip)
        {
            var alst = new List<xcuda_Item>();


            // alst.AddRange(db.xcuda_Item.Where(x => x.PreviousDocumentItem.SubItems.Any(y => y.ItemNumber == salesDetails.InventoryItems.ItemNumber)));
            //match by attribute
            //alst = db.xcuda_Item.AsEnumerable().Where(x => salesDetails.ItemDescription.ToUpper().Split('|')[2] == x.ItemNumber.ToUpper()).ToList();
            using (var ctx = new xcuda_ItemService())
            {
                if (attrib != null)
                {
                    alst =
                        (ctx.Getxcuda_ItemByExpressionLst(
                            new List<string>()
                            {
                                "DoNotAllocate == null || DoNotAllocate != true",
                                "(AsycudaDocument.Extended_customs_procedure == \"7000\" || AsycudaDocument.Extended_customs_procedure == \"9000\")",
                                "SubItems.Count == 0",
                                "AttributeOnlyAllocation == true",
                                string.Format("xcuda_Tarification.xcuda_HScode.Precision_4.ToUpper() == \"{0}\"", attrib)
                            },
                            new List<string>() {"SubItems", "AsycudaDocument","xcuda_Tarification.xcuda_HScode" }).Result)//"EX"
                            .ToList();
                    //.Where(x => x.SubItems.Any() == false && (x.AttributeOnlyAllocation == true) && string.IsNullOrEmpty(salesDetails.ItemDescription.ToUpper().Split('|')[2]) == false && salesDetails.ItemDescription.ToUpper().Split('|')[2].ToUpper().Replace(" ", "") == x.ItemNumber.ToUpper().Replace(" ", "")).ToList();
                }
                alst.AddRange((ctx.Getxcuda_ItemByExpressionLst(new List<string>()
                {
                    "DoNotAllocate == null || DoNotAllocate != true",
                    "(AsycudaDocument.Extended_customs_procedure == \"7000\" || AsycudaDocument.Extended_customs_procedure == \"9000\")",
                   "SubItems.Count == 0",
                    "AttributeOnlyAllocation == null || AttributeOnlyAllocation != true",
                    string.Format("\"{0}\".Contains(xcuda_Tarification.xcuda_HScode.Precision_4.ToUpper())", salesDescrip)
                },
                    new List<string>() { "SubItems", "AsycudaDocument", "xcuda_Tarification.xcuda_HScode" })).Result);//"EX"
                //.Where(x => x.SubItems.Any() == false 
                //            && (x.AttributeOnlyAllocation == null || x.AttributeOnlyAllocation != true) 
                //            && salesDetails.ItemDescription.ToUpper().Replace(" ", "").Contains(x.ItemNumber.ToUpper().Replace(" ", ""))));
            }
            return alst;
        }

        private async Task SaveItemLst(ItemSet itm)
        {
                foreach (var item in itm.SalesList)
                {
                    await SaveEntryDataDetails(item).ConfigureAwait(false);
                }
           
           
                foreach (var item in itm.EntriesList)
                {
                   await SaveXcuda_Item(item).ConfigureAwait(false);
                }
        }

        private  async Task<ConcurrentDictionary<string,ItemSet>> MatchSalestoAsycudaEntriesOnItemNumber()
        {
            var asycudaEntries = await GetAsycudaEntriesWithItemNumber().ConfigureAwait(false);

            var saleslst = await GetSaleslstWithItemNumber().ConfigureAwait(false);

            var itmLst = CreateItemSetsWithItemNumbers(saleslst, asycudaEntries);

            return itmLst; //.Where(x => x.ItemNumber == "OC1719907");
        }

        private async Task<ConcurrentDictionary<string, ItemSet>> MatchSalestoAsycudaEntriesOnDescription()
        {
            List<ItemEntries> asycudaEntries = new List<ItemEntries>();
            asycudaEntries.AddRange(await GetAsycudaEntriesWithDescription().ConfigureAwait(false));
            asycudaEntries.AddRange(await GetAsycudaEntriesWithItemNumber().ConfigureAwait(false));

            List<ItemSales> saleslst = new List<ItemSales>();
            saleslst.AddRange(await GetSaleslstWithDescription().ConfigureAwait(false));
            saleslst.AddRange(await GetSaleslstWithItemNumber().ConfigureAwait(false));

            var itmLst = CreateItemSetsWithDescription(saleslst, asycudaEntries);

            return itmLst; //.Where(x => x.ItemNumber == "OC1719907");
        }

        private static ConcurrentDictionary<string,ItemSet> CreateItemSetsWithItemNumbers(IEnumerable<ItemSales> saleslst, IEnumerable<ItemEntries> asycudaEntries)
        {

            var itmLst = from s in saleslst
                         join a in asycudaEntries on s.Key equals a.Key into j
                         from a in j.DefaultIfEmpty()
                         select new ItemSet
                         {

                             Key = s.Key,
                             SalesList = s.SalesList,
                             EntriesList = a?.EntriesList ?? new List<xcuda_Item>()
                         };

            
            var res = new ConcurrentDictionary<string, ItemSet>();
            foreach (var itm in itmLst)
            {

                res.AddOrUpdate(itm.Key, itm,(key,value) => itm);
            }

            
            foreach (var r in res)
            {
                var alias = Instance.InventoryAliasCache.Data.Where(x => x.ItemNumber == r.Key).Select(y => y.AliasName).ToList();
                if (!alias.Any()) continue;
                var ae = asycudaEntries.Where(x => alias.Contains(x.Key)).SelectMany(y => y.EntriesList).ToList();
                if (ae.Any()) r.Value.EntriesList.AddRange(ae);
            }
            return res;
        }

        private static ConcurrentDictionary<string, ItemSet> CreateItemSetsWithDescription(IEnumerable<ItemSales> saleslst, IEnumerable<ItemEntries> asycudaEntries)
        {

            var itmLst = from s in saleslst
                from a in asycudaEntries
                         where s.Key == a.Key || (s.Key.Contains(a.Key) || a.Key.Contains(s.Key))
                select new ItemSet
                {

                    Key = s.Key,
                    SalesList = s.SalesList,
                    EntriesList = a?.EntriesList
                };


            var res = new ConcurrentDictionary<string, ItemSet>();
            foreach (var itm in itmLst)
            {

                res.AddOrUpdate(itm.Key, itm, (key, value) => itm);
            }


            foreach (var r in res.Values.Where(x => x.EntriesList == null))
            {
                //var r = res.FirstOrDefault(x => x.Key == alias.AliasName);
                var alias = Instance.InventoryAliasCache.Data.Where(x => x.ItemNumber == r.Key).Select(y => y.AliasName).ToList();
                var ae = asycudaEntries.Where(x => alias.Contains(x.Key)).SelectMany(y => y.EntriesList).ToList();
                if (ae.Any()) r.EntriesList = ae;
            }
            return res;
        }


        private static async Task<IEnumerable<ItemEntries>> GetAsycudaEntriesWithItemNumber()
        {
            StatusModel.Timer("Getting Data - Asycuda Entries...");
            //string itmnumber = "WMHP24-72";
            IEnumerable<ItemEntries> asycudaEntries = null;
            using (var ctx = new xcuda_ItemService())
            {
                var lst = await ctx.Getxcuda_ItemByExpressionNav(
                     "All",
                   // "xcuda_Tarification.xcuda_HScode.Precision_4 == \"1360\"",
                    new Dictionary<string, string>() { { "AsycudaDocument", (BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate.HasValue ? string.Format("AssessmentDate >= \"{0}\" && ", BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate) : "") + "(CNumber != null || IsManuallyAssessed == true) && (Extended_customs_procedure == \"7000\" || Extended_customs_procedure == \"9000\") && DoNotAllocate != true" } }
                    , new List<string>() { "AsycudaDocument",
                        "xcuda_Tarification.xcuda_HScode", "xcuda_Tarification.xcuda_Supplementary_unit","SubItems" 
                                         }).ConfigureAwait(false);//"EX"


                asycudaEntries = from s in lst.Where(x => x.ItemNumber != null)
                   // .Where(x => x.ItemNumber == itmnumber)
                    //       .Where(x => x.AsycudaDocument.CNumber != null).AsEnumerable()
                    group s by s.ItemNumber.Trim()
                    into g
                    select
                        new ItemEntries
                        {
                            Key = g.Key.Trim(),
                            EntriesList =
                                g.AsEnumerable()
                                    .OrderBy(
                                        x =>
                                            x.AsycudaDocument.EffectiveRegistrationDate == null
                                                ? Convert.ToDateTime(x.AsycudaDocument.RegistrationDate)
                                                : x.AsycudaDocument.EffectiveRegistrationDate)
                                    .ToList()
                        };
            }
            return asycudaEntries;
        }

        private static async Task<IEnumerable<ItemEntries>> GetAsycudaEntriesWithDescription()
        {
            StatusModel.Timer("Getting Data - Asycuda Entries...");
            //string itmnumber = "WMHP24-72";
            IEnumerable<ItemEntries> asycudaEntries = null;
            using (var ctx = new xcuda_ItemService())
            {
                var lst = await ctx.Getxcuda_ItemByExpressionNav(
                    "All",
                    // "xcuda_Tarification.xcuda_HScode.Precision_4 == \"1360\"",
                    new Dictionary<string, string>() { { "AsycudaDocument", (BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate.HasValue ? string.Format("AssessmentDate >= \"{0}\" && ", BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate) : "") + "(CNumber != null || IsManuallyAssessed == true) && (Extended_customs_procedure == \"7000\" || Extended_customs_procedure == \"9000\") && DoNotAllocate != true" } }
                    , new List<string>() { "AsycudaDocument",
                        "xcuda_Tarification.xcuda_HScode", "xcuda_Tarification.xcuda_Supplementary_unit","SubItems", "xcuda_Goods_description",
                    }).ConfigureAwait(false);//"EX"


                asycudaEntries = from s in lst.Where(x => x.ItemDescription != null)
                     //.Where(x => x.ItemDescription == "Hardener-Resin 'A' Slow .44Pt")
                    //       .Where(x => x.AsycudaDocument.CNumber != null).AsEnumerable()
                    group s by s.ItemDescription.Trim()
                    into g
                    select
                    new ItemEntries
                    {
                        Key = g.Key.Trim(),
                        EntriesList =
                            g.AsEnumerable()
                                .OrderBy(
                                    x =>
                                        x.AsycudaDocument.EffectiveRegistrationDate == null
                                            ? Convert.ToDateTime(x.AsycudaDocument.RegistrationDate)
                                            : x.AsycudaDocument.EffectiveRegistrationDate)
                                .ToList()
                    };
            }
            return asycudaEntries;
        }

        private static async Task<IEnumerable<ItemSales>> GetSaleslstWithItemNumber()
        {
            StatusModel.Timer("Getting Data - Sales Entries...");

            IEnumerable<ItemSales> saleslst = null;
            using (var ctx = new EntryDataDetailsService())
            {
                var salesData =

                await
                        ctx.GetEntryDataDetailsByExpressionNav(//"ItemNumber == \"FAA/SCPI18X112\" &&" +
                                                                (BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate.HasValue ? string.Format("Sales.EntryDataDate >= \"{0}\" && ", BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate) : "") +
                                                               "QtyAllocated != Quantity " +
                                                               "&& Cost > 0 " +
                                                               "&& DoNotAllocate != true", new Dictionary<string, string>()
                                                               {
                                                                   { "Sales", "INVNumber != null" }
                                                               }, new List<string>() { "Sales", "AsycudaSalesAllocations" })
                            .ConfigureAwait(false);
                saleslst = from d in salesData
                    //.Where(x => x.EntryData == "GB-0009053")                                       
                    //.SelectMany(x => x.EntryDataDetails.Select(ed => ed))
                    //.Where(x => x.QtyAllocated == null || x.QtyAllocated != ((Double) x.Quantity))
                    //.Where(x => x.ItemNumber == itmnumber)
                    // .AsEnumerable()
                    group d by d.ItemNumber.Trim()
                    into g
                    select
                        new ItemSales
                        {
                            Key = g.Key,
                            SalesList = g.Where(xy => xy != null & xy.Sales != null).OrderBy(x => x.Sales.EntryDataDate).ThenBy(x => x.EntryDataId).ToList()
                        };
            }
            return saleslst;
        }

        private static async Task<IEnumerable<ItemSales>> GetSaleslstWithDescription()
        {
            StatusModel.Timer("Getting Data - Sales Entries...");

            IEnumerable<ItemSales> saleslst = null;
            using (var ctx = new EntryDataDetailsService())
            {
                var salesData =

                    await
                        ctx.GetEntryDataDetailsByExpressionNav(//"ItemNumber == \"FAA/SCPI18X112\" &&" +
                                (BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate.HasValue ? string.Format("Sales.EntryDataDate >= \"{0}\" && ", BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate) : "") +
                                "QtyAllocated != Quantity " +
                                "&& Cost > 0 " +
                                "&& DoNotAllocate != true", new Dictionary<string, string>()
                                {
                                    { "Sales", "INVNumber != null" }
                                }, new List<string>() { "Sales", "AsycudaSalesAllocations" })
                            .ConfigureAwait(false);
                saleslst = from d in salesData
                    //.Where(x => x.EntryData == "GB-0009053")                                       
                    //.SelectMany(x => x.EntryDataDetails.Select(ed => ed))
                    //.Where(x => x.QtyAllocated == null || x.QtyAllocated != ((Double) x.Quantity))
                    //.Where(x => x.ItemNumber == itmnumber)
                    // .AsEnumerable()
                    group d by d.ItemDescription.Trim()
                    into g
                    select
                    new ItemSales
                    {
                        Key = g.Key,
                        SalesList = g.Where(xy => xy != null & xy.Sales != null).OrderBy(x => x.Sales.EntryDataDate).ThenBy(x => x.EntryDataId).ToList()
                    };
            }
            return saleslst;
        }




        private async Task AllocateSalestoAsycudaByKey(List<EntryDataDetails> saleslst, List<xcuda_Item> asycudaEntries)
        {
            try
            {


                if (asycudaEntries == null || !asycudaEntries.Any())
                {
                    foreach (var item in saleslst)
                    {
                        if (item.AsycudaSalesAllocations.FirstOrDefault(x => x.Status == "No Asycuda Entries Found") == null)
                        {
                            await AddExceptionAllocation(item, "No Asycuda Entries Found").ConfigureAwait(false);

                        }

                    }

                    return;
                }

                var CurrentAsycudaItemIndex = 0;
                var CurrentSalesItemIndex = 0;
                var cAsycudaItm = GetAsycudaEntriesWithItemNumber(asycudaEntries, CurrentAsycudaItemIndex);
                var saleitm = GetSaleEntries(saleslst, CurrentSalesItemIndex);

                
                while (cAsycudaItm.QtyAllocated == Convert.ToDouble(cAsycudaItm.ItemQuantity))
                {
                    if (CurrentAsycudaItemIndex + 1 < asycudaEntries.Count())
                    {
                        CurrentAsycudaItemIndex += 1;
                        cAsycudaItm = GetAsycudaEntriesWithItemNumber(asycudaEntries, CurrentAsycudaItemIndex);
                    }
                    else
                    {
                        
                        break;
                    }
                }

                for (var s = CurrentSalesItemIndex; s < saleslst.Count(); s++)
                {
                    StatusModel.Refresh();

                    
                    if (CurrentSalesItemIndex != s)
                    {
                        CurrentSalesItemIndex = s;
                        saleitm = GetSaleEntries(saleslst, CurrentSalesItemIndex);
                    }
                    
                                      

                   var saleitmQtyToallocate = saleitm.Quantity - saleitm.QtyAllocated;

                    for (var i = CurrentAsycudaItemIndex; i < asycudaEntries.Count(); i++)
                    {
                        // reset in event earlier dat

                       
                        if (CurrentAsycudaItemIndex != i)
                        {
                            CurrentAsycudaItemIndex = i;
                            cAsycudaItm = GetAsycudaEntriesWithItemNumber(asycudaEntries, CurrentAsycudaItemIndex);
                        }

                        
                        // 

                        if ((cAsycudaItm.AsycudaDocument.EffectiveRegistrationDate ??
                             cAsycudaItm.AsycudaDocument.RegistrationDate) > saleitm.Sales.EntryDataDate)
                        {
                            var diff = checkRemainingQty(saleslst, asycudaEntries, CurrentAsycudaItemIndex, CurrentSalesItemIndex);
                            if (diff > 0)//More Sales than Entries
                            {
                                await AddExceptionAllocation(saleitm, "Early Sales").ConfigureAwait(false);
                                break;
                            }
                            else //less sales than entries
                            {
                                if (CurrentAsycudaItemIndex > 0)
                                {

                                    var pAsycudaItm = GetAsycudaEntriesWithItemNumber(asycudaEntries, CurrentAsycudaItemIndex - 1);
                                    if (pAsycudaItm.ItemQuantity - pAsycudaItm.QtyAllocated > 0)
                                    {
                                        CurrentAsycudaItemIndex -= 1;
                                        cAsycudaItm = GetAsycudaEntriesWithItemNumber(asycudaEntries, CurrentAsycudaItemIndex);
                                        i = CurrentAsycudaItemIndex;
                                    }
                                    else
                                    {
                                        await AddExceptionAllocation(saleitm, "Out Of Stock").ConfigureAwait(false);
                                        break;
                                    }

                                }
                                else
                                {
                                    await AddExceptionAllocation(saleitm, "Early Sales").ConfigureAwait(false);
                                    break;
                                }
                                
                            }
                        }


                        SubItems subitm = null;
                        var asycudaItmQtyToAllocate = GetAsycudaItmQtyToAllocate(cAsycudaItm, saleitm, out subitm);


                            if (asycudaItmQtyToAllocate >= saleitmQtyToallocate || 
                            CurrentAsycudaItemIndex == asycudaEntries.Count - 1)
                        {
                            var ramt = await AllocateSaleItem(cAsycudaItm, saleitm, saleitmQtyToallocate, subitm).ConfigureAwait(false);
                            saleitmQtyToallocate = ramt;
                            if (ramt == 0) break;
                        }
                        else
                        {
                            var ramt = await AllocateSaleItem(cAsycudaItm, saleitm, asycudaItmQtyToAllocate, subitm).ConfigureAwait(false);
                            saleitmQtyToallocate -= asycudaItmQtyToAllocate;
                            if (saleitmQtyToallocate < 0)
                            {
                                throw new ApplicationException("saleitmQtyToallocate < 0 check this out");
                            }
                        }
                        
                    }

                    
                    

                }
                    
            }


            catch (Exception e)
            {
                throw e;
            }
        }

        private double checkRemainingQty(List<EntryDataDetails> saleslst, List<xcuda_Item> asycudaEntries, int CurrentAsycudaItemIndex,
            int CurrentSalesItemIndex)
        {
            double asyRemainingQty = asycudaEntries.Skip(CurrentAsycudaItemIndex + 1).Sum(x => x.ItemQuantity - x.QtyAllocated);
            double salesRemainingQty = saleslst.Skip(CurrentSalesItemIndex + 1).Sum(x => x.Quantity - x.QtyAllocated);
            return salesRemainingQty - asyRemainingQty;
        }

     
        private async Task AddExceptionAllocation(EntryDataDetails saleitm, string error)
        {
            if (saleitm.AsycudaSalesAllocations.FirstOrDefault(x => x.Status == error) == null)
            {
                var ssa = new AsycudaSalesAllocations(true)
                {
                    EntryDataDetailsId = saleitm.EntryDataDetailsId,
                    //EntryDataDetails = saleitm,
                    QtyAllocated = saleitm.Quantity - saleitm.QtyAllocated,
                    Status = error,
                    TrackingState = TrackingState.Added
                };
                await SaveAllocation(ssa).ConfigureAwait(false);
                saleitm.AsycudaSalesAllocations.Add(ssa);
            }
        }



        private static async Task SaveEntryDataDetails(EntryDataDetails item)
        {
            if (item == null) return;
            using (var ctx = new EntryDataDetailsService())
            {
                await ctx.UpdateEntryDataDetails(item).ConfigureAwait(false);
            }
        }


        private double GetAsycudaItmQtyToAllocate(xcuda_Item cAsycudaItm, EntryDataDetails saleitm, out SubItems subitm)
        {
            double asycudaItmQtyToAllocate;
            if (cAsycudaItm.SubItems.Any())
            {
                subitm = cAsycudaItm.SubItems.FirstOrDefault(x => x.ItemNumber == saleitm.ItemNumber);
                if (subitm != null)
                {
                    asycudaItmQtyToAllocate = subitm.Quantity - subitm.QtyAllocated;
                    //if (Convert.ToDouble(asycudaItmQtyToAllocate) > (Convert.ToDouble(cAsycudaItm.ItemQuantity) - cAsycudaItm.QtyAllocated))
                    //{
                    //    asycudaItmQtyToAllocate = cAsycudaItm.ItemQuantity - cAsycudaItm.QtyAllocated;
                    //}
                }
                else
                {
                    asycudaItmQtyToAllocate = 0;
                }
            }
            else
            {
                asycudaItmQtyToAllocate = (cAsycudaItm.ItemQuantity * cAsycudaItm.SalesFactor) - (cAsycudaItm.QtyAllocated * cAsycudaItm.SalesFactor);
                subitm = null;
            }

            return asycudaItmQtyToAllocate;
        }

        private  xcuda_Item GetAsycudaEntriesWithItemNumber(IList<xcuda_Item> asycudaEntries, int CurrentAsycudaItemIndex)
        {
            var cAsycudaItm = asycudaEntries.ElementAtOrDefault<xcuda_Item>(CurrentAsycudaItemIndex);
            if (cAsycudaItm.QtyAllocated == 0)
            {
                cAsycudaItm.DFQtyAllocated = 0;
                cAsycudaItm.DPQtyAllocated = 0;
            }

            return cAsycudaItm;
        }

        private  EntryDataDetails GetSaleEntries(IList<EntryDataDetails> SaleEntries, int CurrentSaleItemIndex)
        {
            return SaleEntries.ElementAtOrDefault<EntryDataDetails>(CurrentSaleItemIndex);
        }

        private  async Task<double> AllocateSaleItem(xcuda_Item cAsycudaItm, EntryDataDetails saleitm,
                                             double saleitmQtyToallocate, SubItems subitm)
        {
            try
            {
                cAsycudaItm.StartTracking();
                saleitm.StartTracking();

                var dfp = ((Sales) saleitm.Sales).DutyFreePaid;
                // allocate Sale item
                var ssa = new AsycudaSalesAllocations()
                {
                    EntryDataDetailsId = saleitm.EntryDataDetailsId,
                    PreviousItem_Id = cAsycudaItm.Item_Id,
                    QtyAllocated = 0,
                    TrackingState = TrackingState.Added
                };




                if (saleitmQtyToallocate != 0)//&& removed because of previous return//cAsycudaItm.QtyAllocated >= 0 && 
                   // cAsycudaItm.QtyAllocated <= Convert.ToDouble(cAsycudaItm.ItemQuantity)
                {


                    if (saleitmQtyToallocate > 0)
                    {

                        if (subitm != null)
                        {
                            subitm.StartTracking();
                            subitm.QtyAllocated = subitm.QtyAllocated + saleitmQtyToallocate;
                        }

                        if (dfp == "Duty Free")
                        {
                            cAsycudaItm.DFQtyAllocated += saleitmQtyToallocate / cAsycudaItm.SalesFactor;
                        }
                        else
                        {
                            cAsycudaItm.DPQtyAllocated += saleitmQtyToallocate / cAsycudaItm.SalesFactor;
                        }

                        if (BaseDataModel.Instance.CurrentApplicationSettings.AllowEntryDoNotAllocate == "Visible")
                        {
                            SetPreviousItemXbond(ssa, cAsycudaItm, dfp, saleitmQtyToallocate / cAsycudaItm.SalesFactor);
                        }

                        saleitm.QtyAllocated += saleitmQtyToallocate;

                        ssa.QtyAllocated += saleitmQtyToallocate;

                        saleitmQtyToallocate = 0;
                    }
                    else
                    {
                        
                        double mqty = saleitmQtyToallocate * -1;

                       
                            if (subitm != null)
                            {
                                subitm.StartTracking();
                                subitm.QtyAllocated = subitm.QtyAllocated - mqty;
                            }
                            if (dfp == "Duty Free")
                            {
                                cAsycudaItm.DFQtyAllocated -= mqty / cAsycudaItm.SalesFactor;
                            }
                            else
                            {
                                cAsycudaItm.DPQtyAllocated -= mqty / cAsycudaItm.SalesFactor;
                            }

                            if (BaseDataModel.Instance.CurrentApplicationSettings.AllowEntryDoNotAllocate == "Visible")
                            {
                                SetPreviousItemXbond(ssa, cAsycudaItm, dfp, -mqty / cAsycudaItm.SalesFactor);
                            }
                            saleitmQtyToallocate += mqty;

                            saleitm.QtyAllocated -= mqty;
                           

                            ssa.QtyAllocated -= mqty; 

                        //}
                    }
                }
                //saleitm.AsycudaSalesAllocations = new ObservableCollection<AsycudaSalesAllocations>(saleitm.AsycudaSalesAllocations){ssa};
              
                await SaveAllocation(ssa).ConfigureAwait(false);
                //if (subitm != null && subitm.ChangeTracker!= null) await SaveSubItem(subitm.ChangeTracker.GetChanges().FirstOrDefault()).ConfigureAwait(false);
               
                // await SaveXcuda_Item(cAsycudaItm.ChangeTracker.GetChanges().FirstOrDefault()).ConfigureAwait(false);
               // await SaveEntryDataDetails(saleitm.ChangeTracker.GetChanges().FirstOrDefault()).ConfigureAwait(false);

               // saleitm.AsycudaSalesAllocations.Add(ssa);
                return saleitmQtyToallocate;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task SaveAllocation(AsycudaSalesAllocations ssa)
        {
            using (var ctx = new AsycudaSalesAllocationsService())
            {
                await ctx.UpdateAsycudaSalesAllocations(ssa).ConfigureAwait(false);
            }
        }

        private static async Task SaveXcuda_Item(xcuda_Item cAsycudaItm)
        {
            using (var ctx = new xcuda_ItemService())
            {
                await ctx.Updatexcuda_Item(cAsycudaItm).ConfigureAwait(false);
            }
        }

        private static async Task SaveSubItem(SubItems subitm)
        {
            if (subitm == null) return;
            using (var ctx = new SubItemsService())
            {
                await ctx.UpdateSubItems(subitm).ConfigureAwait(false);
            }
        }

        private void SetPreviousItemXbond(AsycudaSalesAllocations ssa, xcuda_Item cAsycudaItm, string dfp, double amt)
        {
            try
            {
                if (BaseDataModel.Instance.CurrentApplicationSettings.AllowEntryDoNotAllocate != "Visible") return;


                var alst = cAsycudaItm.EntryPreviousItems.Select(p => p.xcuda_PreviousItem)
                            .Where(x => x.DutyFreePaid == dfp && x.QtyAllocated <= x.Suplementary_Quantity)
                            .Where(x => x.xcuda_Item != null && x.xcuda_Item.AsycudaDocument != null)
                            .OrderBy(
                                    x =>
                                    x.xcuda_Item.AsycudaDocument.EffectiveRegistrationDate ?? Convert.ToDateTime(x.xcuda_Item.AsycudaDocument.RegistrationDate)).ToList();
                foreach (var pitm in alst)
                {
                    if (pitm.QtyAllocated == null) pitm.QtyAllocated = 0;
                    var atot = pitm.Suplementary_Quantity - Convert.ToSingle(pitm.QtyAllocated);
                    if (atot == 0) continue;
                    if (amt <= atot)
                    {
                        pitm.QtyAllocated += amt;
                        var xbond = new xBondAllocations(true)
                        {
                            AllocationId = ssa.AllocationId,
                            xEntryItem_Id = pitm.xcuda_Item.Item_Id,
                            TrackingState = TrackingState.Added
                        };

                        ssa.xBondAllocations.Add(xbond);
                        pitm.xcuda_Item.xBondAllocations.Add(xbond);
                        break;
                    }
                    else
                    {
                        pitm.QtyAllocated += atot;
                        var xbond = new xBondAllocations(true)
                        {
                            AllocationId = ssa.AllocationId,
                            xEntryItem_Id = pitm.xcuda_Item.Item_Id,
                            TrackingState = TrackingState.Added
                        };
                        ssa.xBondAllocations.Add(xbond);
                        pitm.xcuda_Item.xBondAllocations.Add(xbond);
                        amt -= atot;
                    }

                }

            }
            catch (Exception Ex)
            {
                throw;
            }
        }


      
    }
}
