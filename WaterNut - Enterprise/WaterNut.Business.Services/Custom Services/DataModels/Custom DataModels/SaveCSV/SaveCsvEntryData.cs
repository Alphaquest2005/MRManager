using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.CSV;
using DocumentDS.Business.Entities;
using EntryDataDS.Business.Entities;
using EntryDataDS.Business.Services;
using InventoryDS.Business.Entities;
using InventoryDS.Business.Services;
using TrackableEntities;
using AsycudaDocumentSetEntryData = EntryDataDS.Business.Entities.AsycudaDocumentSetEntryData;


namespace WaterNut.DataSpace
{
    public class SaveCsvEntryData
    {
        private static readonly SaveCsvEntryData instance;
        static SaveCsvEntryData()
        {
            instance = new SaveCsvEntryData();
        }

        public static SaveCsvEntryData Instance
        {
            get { return instance; }
        }

        public async Task<bool> ExtractEntryData(string fileType, string[] lines, string[] headings, string csvType, AsycudaDocumentSet docSet, bool overWriteExisting)
        {
            if (docSet == null)
            {
               throw new ApplicationException("Please select Document Set before proceding!");
               
            }
            var mapping = new Dictionary<string, int>();
             GetMappings(mapping, headings);
            var eslst = GetCSVDataSummayList(lines, mapping);

            if (eslst == null) return true;


            if (csvType == "QB9")
            {
                foreach (var item in eslst)
                {
                    item.ItemNumber = item.ItemNumber.Split(':').Last();
                }
            }

            await ImportInventory(eslst).ConfigureAwait(false);

            if (await ImportEntryData(fileType, eslst,docSet.AsycudaDocumentSetId, overWriteExisting).ConfigureAwait(false)) return true;
            return false;
        }



        private async Task<bool> ImportEntryData(string fileType, List<CSVDataSummary> eslst, int docSetId, bool overWriteExisting)
        {
            try
            {

           
            var ed = (from es in eslst
                     group es by new { es.EntryDataId, es.EntryDataDate, es.CustomerName, es.SupplierCode }
                         into g
                         let supplier = EntryDataDS.DataModels.BaseDataModel.Instance.SearchSuppliers(new List<string>()
                         {
                             string.Format("SupplierCode == \"{0}\"",g.Key.SupplierCode)
                         })

                         //where supplier != null
                         select new
                         {
                             e =
                                 new
                                 {
                                     EntryDataId = g.Key.EntryDataId,
                                     EntryDataDate = g.Key.EntryDataDate,
                                     AsycudaDocumentSetId = docSetId,
                                     CustomerName = g.Key.CustomerName,
                                     Tax = g.Sum(x => x.Tax),
                                     Supplier = supplier
                                 },
                             d = g.Select(x => new  EntryDataDetails()
                             {
                                 EntryDataId = x.EntryDataId,
                                 ItemNumber = x.ItemNumber.ToUpper(),
                                 ItemDescription = x.ItemDescription,
                                 Cost = Convert.ToDouble(x.Cost),
                                 Quantity = Convert.ToDouble(x.Quantity),
                                 Units = x.Units,
                                 Freight = x.Freight,
                                 Weight = x.Weight,
                                 InternalFreight = x.InternalFreight
                             }),
                             f = g.Select(x => new
                             {
                                 TotalWeight = x.TotalWeight,
                                 TotalFreight = x.TotalFreight,
                                 TotalInternalFreight = x.TotalInternalFreight
                             })
                         }).ToList();

            if (ed == null) return true;


            List<EntryData> eLst = null;
            
            foreach (var item in ed)
            {
                // check Existing items
                var olded = await GetEntryData(item.e.EntryDataId, item.e.EntryDataDate).ConfigureAwait(false);
                    
                if (olded != null)
                {
                  

                    switch (overWriteExisting)
                    {
                        case true:
                            await ClearEntryDataDetails(olded).ConfigureAwait(false);
                            await DeleteEntryData(olded).ConfigureAwait(false);

                            break;
                        case false:
                            continue;
                
                    }
                }


                switch (fileType)
                {
                    case "Sales":

                        var EDsale = new Sales(true)
                        {
                            EntryDataId = item.e.EntryDataId,
                            EntryDataDate = item.e.EntryDataDate,
                            INVNumber = item.e.EntryDataId,
                            TaxAmount = item.e.Tax,
                            CustomerName = item.e.CustomerName,
                            TrackingState = TrackingState.Added
                        };
                        EDsale.AsycudaDocumentSets.Add(new AsycudaDocumentSetEntryData(true)
                        {
                            AsycudaDocumentSetId = item.e.AsycudaDocumentSetId,
                            EntryDataId = item.e.EntryDataId,
                            TrackingState = TrackingState.Added
                        });
                        await CreateSales(EDsale).ConfigureAwait(false);
                        break;
                    case "PO":
                        var EDpo = new PurchaseOrders(true)
                        {
                            EntryDataId = item.e.EntryDataId,
                            EntryDataDate = item.e.EntryDataDate,
                            PONumber = item.e.EntryDataId,
                            TrackingState = TrackingState.Added,
                            TotalFreight = item.f.Sum(x => x.TotalFreight),
                            TotalInternalFreight = item.f.Sum(x => x.TotalInternalFreight),
                            TotalWeight = item.f.Sum(x => x.TotalWeight)
                        };
                        EDpo.AsycudaDocumentSets.Add(new AsycudaDocumentSetEntryData(true)
                        {
                            AsycudaDocumentSetId = item.e.AsycudaDocumentSetId,
                            EntryDataId = item.e.EntryDataId,
                            TrackingState = TrackingState.Added
                        });
                        await CreatePurchaseOrders(EDpo).ConfigureAwait(false);
                        break;
                    case "OPS":
                        var EDops = new OpeningStock(true)
                        {
                            EntryDataId = item.e.EntryDataId,
                            EntryDataDate = item.e.EntryDataDate,
                            OPSNumber = item.e.EntryDataId,
                            TrackingState = TrackingState.Added,
                            TotalFreight = item.f.Sum(x => x.TotalFreight),
                            TotalInternalFreight = item.f.Sum(x => x.TotalInternalFreight),
                            TotalWeight = item.f.Sum(x => x.TotalWeight)
                        };
                        EDops.AsycudaDocumentSets.Add(new AsycudaDocumentSetEntryData(true)
                        {
                            AsycudaDocumentSetId = item.e.AsycudaDocumentSetId,
                            EntryDataId = item.e.EntryDataId,
                            TrackingState = TrackingState.Added
                        });
                        await CreateOpeningStock(EDops).ConfigureAwait(false);
                        break;
                    default:
                        throw new ApplicationException("Unknown FileType");
                        return true;
                }

                if (item.d.Count() == 0) throw new ApplicationException(item.e.EntryDataId + " has no details");

                var details = item.d;

                using (var ctx = new EntryDataDetailsService())
                {
                    foreach (var e in details)
                    {
                        await ctx.CreateEntryDataDetails(new EntryDataDetails(true)
                        {
                            EntryDataId = e.EntryDataId,
                            ItemNumber = e.ItemNumber,
                            ItemDescription = e.ItemDescription,
                            Quantity = e.Quantity,
                            Cost = e.Cost,
                            Units = e.Units,
                            TrackingState = TrackingState.Added,
                            Freight = e.Freight,
                            Weight = e.Weight,
                            InternalFreight = e.InternalFreight,
                        }).ConfigureAwait(false);
                    }
                }
            }

            
            return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<EntryData> GetEntryData(string entryDataId, DateTime entryDateTime)
        {
            using (var ctx = new EntryDataService())
            {
                return
                    (await ctx.GetEntryDataByExpressionLst(new List<string>()
                    {
                        string.Format("EntryDataId == \"{0}\"",entryDataId),
                        string.Format("EntryDataDate == \"{0}\"", entryDateTime.ToString("yyyy-MMM-dd"))
                    }).ConfigureAwait(false)).FirstOrDefault();
                //eLst.FirstOrDefault(x => x.EntryDataId == item.e.EntryDataId && x.EntryDataDate != item.e.EntryDataDate);
            }
        }


        private void GetMappings(Dictionary<string, int> mapping, string[] headings)
        {
            for (var i = 0; i < headings.Count(); i++)
            {
                var h = headings[i].Trim().ToUpper();

                if (h == "") continue;

                if ("INVNO|Reciept #|NUM|Invoice #".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("EntryDataId", i);
                    continue;
                }

                if ("DATE|Invoice Date".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("EntryDataDate", i);
                    continue;
                }

                if ("ItemNumber|ITEM-#|Item Code".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("ItemNumber", i);
                    continue;
                }

                if ("DESCRIPTION|MEMO|Item Description".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("ItemDescription", i);
                    continue;
                }

                if ("QUANTITY|QTY".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("Quantity", i);
                    continue;
                }


                if ("PRICE|COST|Sales Price".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("Cost", i);
                    continue;
                }

                if ("TotalCost".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("TotalCost", i);
                    continue;
                }

                if ("UNITS".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("Units", i);
                    continue;
                }

                if ("Customer".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("CustomerName", i);
                    continue;
                }
                if ("Tax".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("Tax", i);
                    continue;
                }

                if ("TariffCode".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("TariffCode", i);
                    continue;
                }

                if ("Freight".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("Freight", i);
                    continue;
                }
                if ("Weight".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("Weight", i);
                    continue;
                }
                if ("InternalFreight".ToUpper().Contains(h.ToUpper()))
                {
                    mapping.Add("InternalFreight", i);
                    continue;
                }

            }
        }


        private List<CSVDataSummary> GetCSVDataSummayList(string[] lines, Dictionary<string, int> mapping)
        {
            var eslst = new List<CSVDataSummary>();
            for (var i = 1; i < lines.Count(); i++)
            {
                var d = GetCSVDataFromLine(lines[i], mapping);
                if (d != null)
                {
                    eslst.Add(d);
                }
            }
            return eslst;
        }



        private CSVDataSummary GetCSVDataFromLine(string line, Dictionary<string, int> mapping)
        {
            try
            {
                var splits = line.CsvSplit();
                if (splits[mapping["EntryDataId"]] != "" && splits[mapping["ItemNumber"]] != "")
                {
                  var res = new CSVDataSummary()
                    {
                        EntryDataId = splits[mapping["EntryDataId"]],
                        EntryDataDate = DateTime.Parse(string.IsNullOrEmpty(splits[mapping["EntryDataDate"]]) ? DateTime.MinValue.ToShortDateString() : splits[mapping["EntryDataDate"]]),
                        ItemNumber = splits[mapping["ItemNumber"]],
                        ItemDescription = splits[mapping["ItemDescription"]],
                        Cost = !mapping.ContainsKey("Cost") ? 0 : Convert.ToSingle(string.IsNullOrEmpty(splits[mapping["Cost"]]) ? "0" : splits[mapping["Cost"]].Replace("$","")),
                        Quantity = Convert.ToSingle(splits[mapping["Quantity"]]),
                        Units = mapping.ContainsKey("Units") ? splits[mapping["Units"]] : "",
                        CustomerName = mapping.ContainsKey("CustomerName") ? splits[mapping["CustomerName"]] : "",
                        Tax = Convert.ToSingle(mapping.ContainsKey("Tax") ? splits[mapping["Tax"]] : "0"),
                        TariffCode = mapping.ContainsKey("TariffCode") ? splits[mapping["TariffCode"]] : "",
                        SupplierCode = mapping.ContainsKey("SupplierCode") ? splits[mapping["SupplierCode"]] : "",
                        Freight = Convert.ToSingle(mapping.ContainsKey("Freight") && !string.IsNullOrEmpty(splits[mapping["Freight"]]) ? splits[mapping["Freight"]] : "0"),
                        Weight = Convert.ToSingle(mapping.ContainsKey("Weight") && !string.IsNullOrEmpty(splits[mapping["Weight"]]) ? splits[mapping["Weight"]] : "0"),
                        InternalFreight = Convert.ToSingle(mapping.ContainsKey("InternalFreight") && !string.IsNullOrEmpty(splits[mapping["InternalFreight"]]) ? splits[mapping["InternalFreight"]] : "0"),
                        TotalFreight = Convert.ToSingle(mapping.ContainsKey("TotalFreight") && !string.IsNullOrEmpty(splits[mapping["TotalFreight"]]) ? splits[mapping["TotalFreight"]] : "0"),
                        TotalWeight = Convert.ToSingle(mapping.ContainsKey("TotalWeight") && !string.IsNullOrEmpty(splits[mapping["TotalWeight"]]) ? splits[mapping["TotalWeight"]] : "0"),
                        TotalInternalFreight = Convert.ToSingle(mapping.ContainsKey("TotalInternalFreight") && !string.IsNullOrEmpty(splits[mapping["TotalInternalFreight"]]) ? splits[mapping["TotalInternalFreight"]] : "0")
                    };

                    if(mapping.ContainsKey("TotalCost") && !string.IsNullOrEmpty(splits[mapping["TotalCost"]]))
                    {
                        res.Cost = Convert.ToSingle(splits[mapping["TotalCost"]].Replace("$", "")) /res.Quantity;
                    }
                    return res;
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
            return null;
        }

        class CSVDataSummary
        {
            public string EntryDataId { get; set; }
            public DateTime EntryDataDate { get; set; }
            public string ItemNumber { get; set; }
            public string ItemDescription { get; set; }
            public Single Quantity { get; set; }
            public Single Cost { get; set; }
            public string Units { get; set; }
            public string CustomerName { get; set; }
            public Single Tax { get; set; }
            public string TariffCode { get; set; }

            public string SupplierCode { get; set; }

            public double? Freight { get; set; }

            public double? Weight { get; set; }

            public double? InternalFreight { get; set; }
            public double? TotalFreight { get; set; }

            public double? TotalWeight { get; set; }

            public double? TotalInternalFreight { get; set; }
        }

        private async Task ImportInventory(List<CSVDataSummary> eslst)
        {
            var itmlst = from i in eslst
                         group i by i.ItemNumber.ToUpper()
                             into g
                             select new { ItemNumber = g.Key, g.FirstOrDefault().ItemDescription, g.FirstOrDefault().TariffCode };

            using (var ctx = new InventoryItemService(){StartTracking = true})
            {
                foreach (var item in itmlst)
                {
                    var i =
                       await ctx.GetInventoryItemByKey(item.ItemNumber, null, true).ConfigureAwait(false);
                            

                    if (i == null)
                    {
                        i = new InventoryItem(true)
                        {
                            Description = item.ItemDescription,
                            ItemNumber = item.ItemNumber,
                           TrackingState = TrackingState.Added
                        };
                        if (!string.IsNullOrEmpty(item.TariffCode)) i.TariffCode = item.TariffCode;
                        await ctx.CreateInventoryItem(i).ConfigureAwait(false);
                       
                    }
                    else
                    {
                        i.StartTracking();
                        i.Description = item.ItemDescription;
                        if (!string.IsNullOrEmpty(item.TariffCode)) i.TariffCode = item.TariffCode;
                        await ctx.UpdateInventoryItem(i).ConfigureAwait(false);
                       
                    }

                }
            }
        }

        private async Task<OpeningStock> CreateOpeningStock(OpeningStock EDops)
        {
            using (var ctx = new OpeningStockService())
            {
                return await ctx.CreateOpeningStock(EDops).ConfigureAwait(false);
            }
        }
        private async Task<PurchaseOrders> CreatePurchaseOrders(PurchaseOrders EDpo)
        {
            using (var ctx = new PurchaseOrdersService())
            {
                return await ctx.CreatePurchaseOrders(EDpo).ConfigureAwait(false);
            }
        }

        private async Task<Sales> CreateSales(Sales EDsale)
        {
            using (var ctx = new SalesService())
            {
                return await ctx.CreateSales(EDsale).ConfigureAwait(false);
            }
        }

        private async Task DeleteEntryData(EntryData olded)
        {
            using (var ctx = new EntryDataService())
            {
                await ctx.DeleteEntryData(olded.EntryDataId).ConfigureAwait(false);
            }
        }

        private async Task ClearEntryDataDetails(EntryData olded)
        {
            using (var ctx = new EntryDataDetailsService())
            {
                foreach (var itm in olded.EntryDataDetails.ToList())
                {
                    await ctx.DeleteEntryDataDetails(itm.EntryDataDetailsId.ToString()).ConfigureAwait(false);
                }
            }
        }
    }
}