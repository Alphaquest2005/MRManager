using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using AllocationDS.Business.Entities;
using AllocationDS.Business.Services;
using Core.Common.UI;
using DocumentDS.Business.Entities;
using DocumentDS.Business.Services;
using TrackableEntities;
using WaterNut.Business.Entities;
using WaterNut.Interfaces;
using EntryDataDetails = AllocationDS.Business.Entities.EntryDataDetails;
using xBondAllocations = DocumentItemDS.Business.Entities.xBondAllocations;
using xcuda_Weight = DocumentDS.Business.Entities.xcuda_Weight;
using xcuda_Weight_itm = DocumentItemDS.Business.Entities.xcuda_Weight_itm;
using System.Data.Entity;

//using xcuda_Item = AllocationDS.Business.Entities.xcuda_Item;
//using xcuda_PreviousItem = AllocationDS.Business.Entities.xcuda_PreviousItem;

namespace WaterNut.DataSpace
{
    public class CreateEx9Class
    {
        private static readonly CreateEx9Class _instance;

        static CreateEx9Class()
        {
            _instance = new CreateEx9Class();
        }

        public static CreateEx9Class Instance
        {
            get { return _instance; }
        }

        public bool BreakOnMonthYear { get; set; }


        public bool PerIM7 { get; set; }


        public bool ApplyEx9Bucket { get; set; }


        public async Task CreateEx9(string filterExpression, bool perIM7, bool applyEx9Bucket, bool breakOnMonthYear,
            AsycudaDocumentSet docSet)
        {

            try
            {
                PerIM7 = perIM7;
                ApplyEx9Bucket = applyEx9Bucket;
                BreakOnMonthYear = breakOnMonthYear;

                await ProcessEx9(docSet, filterExpression).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task ProcessEx9(AsycudaDocumentSet docSet, string filterExp)
        {
           

            //var dutylst = CreateDutyList(slst);
            var dutylst = new List<string>(){"Duty Paid", "Duty Free"};
            foreach (var dfp in dutylst)
            {
                var slst =
                (await CreateAllocationDataBlocks(filterExp).ConfigureAwait(false)).Where(
                    x => x.Allocations.Count > 0);

                await CreateDutyFreePaidDocument(dfp, slst.Where(x => x.DutyFreePaid == dfp), docSet)
                    .ConfigureAwait(false);
            }

            StatusModel.StopStatusUpdate();
        }

        private async Task CreateDutyFreePaidDocument(string dfp, IEnumerable<AllocationDataBlock> slst,
            AsycudaDocumentSet docSet)
        {
            try
            {


                var itmcount = 0;


                Document_Type dt;
                dt = await GetDocumentType(dfp).ConfigureAwait(false);
                if (dt == null)
                {
                    throw new ApplicationException(
                        string.Format("Null Document Type for '{0}' Contact your Network Administrator", dfp));
                }
                //TODO: Redo this check
                if (slst.ToList().SelectMany(x => x.Allocations).Select(x => x.InvoiceDate.Month).Distinct().Count() > 1)
                {
                    throw new ApplicationException(
                        string.Format("Data Contains Multiple Months", dfp));
                }

                

                StatusModel.StatusUpdate(string.Format("Creating xBond Entries - {0}", dfp));

                var cdoc = await BaseDataModel.Instance.CreateDocumentCt(docSet).ConfigureAwait(false);

                foreach (var monthyear in slst) //.Where(x => x.DutyFreePaid == dfp)
                {

                    var prevEntryId = "";
                    var prevIM7 = "";
                    var elst = PrepareAllocationsData(monthyear);

                    var effectiveAssessmentDate =
                    monthyear.Allocations.Select(x => x.InvoiceDate).Min();

                    foreach (var mypod in elst)
                    {
                        //itmcount = await InitializeDocumentCT(itmcount, prevEntryId, mypod, cdoc, prevIM7, monthyear, dt, dfp).ConfigureAwait(true);

                        if (MaxLineCount(itmcount)
                            || InvoicePerEntry(prevEntryId, mypod)
                            || (cdoc.Document == null || itmcount == 0)
                            || IsPerIM7(prevIM7, monthyear))
                        {
                            if (itmcount != 0)
                            {
                                if (cdoc.Document != null)
                                {
                                    await SaveDocumentCT(cdoc).ConfigureAwait(false);
                                    //}
                                    //else
                                    //{
                                    cdoc = await BaseDataModel.Instance.CreateDocumentCt(docSet).ConfigureAwait(false);
                                }
                            }
                            Ex9InitializeCdoc(dt, dfp, cdoc, docSet);
                            if (PerIM7)
                                cdoc.Document.xcuda_Declarant.Number =
                                    cdoc.Document.xcuda_Declarant.Number.Replace(
                                        docSet.Declarant_Reference_Number,
                                        docSet.Declarant_Reference_Number +
                                        "-" +
                                        monthyear.CNumber);
                            InsertEntryIdintoRefNum(cdoc, mypod.EntlnData.EntryDataDetails.First().EntryDataId);

                          //  if (cdoc.Document.xcuda_General_information == null) cdoc.Document.xcuda_General_information = new xcuda_General_information(true) {TrackingState = TrackingState.Added};
                            cdoc.Document.xcuda_General_information.Comments_free_text =
                                    $"EffectiveAssessmentDate:{effectiveAssessmentDate.ToString("MMM-dd-yyyy")}";

                            itmcount = 0;
                        }

                        if (
                            await
                                CreateEx9EntryAsync(mypod, cdoc, itmcount, dfp, this.ApplyEx9Bucket)
                                    .ConfigureAwait(false))
                        {
                            itmcount += 1;
                        }

                        prevEntryId = mypod.EntlnData.EntryDataDetails.Count() > 0
                            ? mypod.EntlnData.EntryDataDetails[0].EntryDataId
                            : "";
                        prevIM7 = PerIM7 == true ? monthyear.CNumber : "";
                        StatusModel.StatusUpdate();
                    }

                }
                await SaveDocumentCT(cdoc).ConfigureAwait(false);
                if (cdoc.Document.ASYCUDA_Id == 0)
                {
                    //clean up
                    docSet.xcuda_ASYCUDA_ExtendedProperties.Remove(cdoc.Document.xcuda_ASYCUDA_ExtendedProperties);
                    cdoc.Document = null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        private bool IsPerIM7(string prevIM7, AllocationDataBlock monthyear)
        {
            return (PerIM7 == true &&
                    (string.IsNullOrEmpty(prevIM7) || (!string.IsNullOrEmpty(prevIM7) && prevIM7 != monthyear.CNumber)));
        }

        private bool InvoicePerEntry(string prevEntryId, MyPodData mypod)
        {
            return (BaseDataModel.Instance.CurrentApplicationSettings
                .InvoicePerEntry == true &&
                    //prevEntryId != "" &&
                    prevEntryId != mypod.EntlnData.EntryDataDetails[0].EntryDataId);
        }

        public bool MaxLineCount(int itmcount)
        {
            return (itmcount != 0 &&
                    itmcount%
                    BaseDataModel.Instance.CurrentApplicationSettings.MaxEntryLines ==
                    0);
        }

        private async Task SaveDocumentCT(DocumentCT cdoc)
        {
            try
            {

                if (cdoc != null && cdoc.DocumentItems.Any() == true)
                {
                    if (cdoc.Document.xcuda_Valuation == null)
                        cdoc.Document.xcuda_Valuation = new xcuda_Valuation(true)
                        {
                            ASYCUDA_Id = cdoc.Document.ASYCUDA_Id,
                            TrackingState = TrackingState.Added
                        };
                    if (cdoc.Document.xcuda_Valuation.xcuda_Weight == null)
                        cdoc.Document.xcuda_Valuation.xcuda_Weight = new xcuda_Weight(true)
                        {
                            Valuation_Id = cdoc.Document.xcuda_Valuation.ASYCUDA_Id,
                            TrackingState = TrackingState.Added
                        };
                    cdoc.Document.xcuda_Valuation.xcuda_Weight.Gross_weight =
                        cdoc.DocumentItems.Select(x => x.xcuda_PreviousItem).Sum(x => x.Net_weight);



                    await BaseDataModel.Instance.SaveDocumentCT(cdoc).ConfigureAwait(false);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<Document_Type> GetDocumentType(string dfp)
        {
            try
            {

                Document_Type dt;
                using (var ctx = new Document_TypeService())
                {
                    if (dfp == "Duty Free")
                    {
                        dt =
                            (await
                                ctx.GetDocument_TypeByExpression(
                                    "Type_of_declaration + Declaration_gen_procedure_code == \"EX9\"")
                                    .ConfigureAwait(false)).FirstOrDefault();
                    }
                    else
                    {
                        dt =
                            (await
                                ctx.GetDocument_TypeByExpression(
                                    "Type_of_declaration + Declaration_gen_procedure_code == \"IM4\"")
                                    .ConfigureAwait(false)).FirstOrDefault();
                    }
                }
                return dt;

            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        }

        private IEnumerable<string> CreateDutyList(IEnumerable<AllocationDataBlock> slst)
        {
            try
            {
                var dutylst = slst.Where(x => x.Allocations.Count > 0).Select(x => x.DutyFreePaid).Distinct();
                return dutylst;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private async Task<IEnumerable<AllocationDataBlock>> CreateAllocationDataBlocks(string filterExpression)
        {
            try
            {
                StatusModel.Timer("Getting ExBond Data");
                var slstSource = GetEX9Data(filterExpression);
                StatusModel.StartStatusUpdate("Creating xBond Entries", slstSource.Count());
                IEnumerable<AllocationDataBlock> slst;
                slst = BreakOnMonthYear
                    ? CreateBreakOnMonthYearAllocationDataBlocks(slstSource)
                    : CreateWholeAllocationDataBlocks(slstSource);
                return slst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        private IEnumerable<EX9SalesAllocations> GetEX9Data(string FilterExpression)
        {
            FilterExpression =
                FilterExpression.Replace("&& (pExpiryDate >= \"" + DateTime.Now.Date.ToShortDateString() + "\")", "");

            FilterExpression += "&& PreviousDocumentItem.DoNotAllocate != true && PreviousDocumentItem.DoNotEX != true && PreviousDocumentItem.AsycudaDocument.DocumentType == \"IM7\"";

            var exp1 = AllocationsModel.Instance.TranslateAllocationWhereExpression(FilterExpression);
            var map = new Dictionary<string, string>()
            {
                {"pIsAssessed", "PreviousDocumentItem.IsAssessed"},
                {"pRegistrationDate", "PreviousDocumentItem.AsycudaDocument.RegistrationDate"},

                // {"pExpiryDate", "(DbFunctions.AddDays(PreviousDocumentItem.AsycudaDocument.RegistrationDate.GetValueOrDefault(),730))"},
                {"Invalid", "EntryDataDetails.InventoryItem.TariffCodes.Invalid"},
                {"xBond_Item_Id == 0", "PreviousDocumentItem != null"}//xBondAllocations != null  && xBondAllocations.Any() == false

            };
            var exp = map.Aggregate(exp1, (current, m) => current.Replace(m.Key, m.Value));
            var res = new List<EX9SalesAllocations>();
            using (var ctx = new AllocationDSContext())
            {
                try
                {
                   IQueryable<AsycudaSalesAllocations> pres;
                    if (FilterExpression.Contains("xBond_Item_Id == 0"))
                    {
                        
                        pres = ctx.AsycudaSalesAllocations.OrderBy(x => x.AllocationId)
                            .Where(
                                x =>
                                    (DbFunctions.AddDays(
                                        ((DateTime) x.PreviousDocumentItem.AsycudaDocument.RegistrationDate), 730)) >
                                    DateTime.Now).Where(x => !x.xBondAllocations.Any());
                    }
                    else
                    {
                        pres = ctx.AsycudaSalesAllocations.OrderBy(x => x.AllocationId)
                            .Where(
                                x =>
                                    (DbFunctions.AddDays(
                                        ((DateTime) x.PreviousDocumentItem.AsycudaDocument.RegistrationDate), 730)) >
                                    DateTime.Now);
                            
                    }
                    res = pres.Where(exp)
                        .Where(
                            x =>
                                x.PreviousDocumentItem.xcuda_Tarification.xcuda_Supplementary_unit.Any() &&
                                x.PreviousDocumentItem.xcuda_Tarification.xcuda_Supplementary_unit.FirstOrDefault()
                                    .Suppplementary_unit_quantity != 0)
                        .GroupJoin(ctx.xcuda_Weight_itm, x => x.PreviousItem_Id, q => q.Valuation_item_Id,
                            (x, w) => new {x, w})
                        .Select(c => new EX9SalesAllocations
                        {
                            AllocationId = c.x.AllocationId,
                            Commercial_Description =
                                c.x.PreviousDocumentItem == null
                                    ? null
                                    : c.x.PreviousDocumentItem.xcuda_Goods_description.Commercial_Description,
                            DutyFreePaid = c.x.EntryDataDetails.Sales.TaxAmount != 0 ? "Duty Paid" : "Duty Free",
                            EntryDataDetailsId = (int) c.x.EntryDataDetailsId,
                            InvoiceDate = c.x.EntryDataDetails.Sales.EntryDataDate,
                            InvoiceNo = c.x.EntryDataDetails.EntryDataId,
                            ItemDescription = c.x.EntryDataDetails.ItemDescription,
                            ItemNumber = c.x.EntryDataDetails.ItemNumber,
                            pCNumber =
                                c.x.PreviousDocumentItem != null ? c.x.PreviousDocumentItem.AsycudaDocument.CNumber : "",
                            pItemCost =
                                c.x.PreviousDocumentItem != null
                                    ? (double)
                                        (c.x.PreviousDocumentItem.xcuda_Tarification.Item_price/
                                         c.x.PreviousDocumentItem.xcuda_Tarification.xcuda_Supplementary_unit
                                             .FirstOrDefault().Suppplementary_unit_quantity)
                                    : 0,
                            Status = c.x.Status,
                            PreviousItem_Id = c.x.PreviousItem_Id,
                            QtyAllocated = (int) c.x.QtyAllocated,
                            SalesFactor = c.x.PreviousDocumentItem.SalesFactor,
                            SalesQtyAllocated = c.x.EntryDataDetails.QtyAllocated,
                            SalesQuantity = (int) c.x.EntryDataDetails.Quantity,
                            pItemNumber =
                                c.x.PreviousDocumentItem != null
                                    ? c.x.PreviousDocumentItem.xcuda_Tarification.xcuda_HScode.Precision_4
                                    : "",
                            pTariffCode =
                                c.x.PreviousDocumentItem != null
                                    ? c.x.PreviousDocumentItem.xcuda_Tarification.xcuda_HScode.Commodity_code
                                    : "",
                            DFQtyAllocated =
                                (int) (c.x.PreviousDocumentItem != null ? c.x.PreviousDocumentItem.DFQtyAllocated : 0),
                            DPQtyAllocated =
                                (int) (c.x.PreviousDocumentItem != null ? c.x.PreviousDocumentItem.DPQtyAllocated : 0),
                            LineNumber = c.x.PreviousDocumentItem != null ? c.x.PreviousDocumentItem.LineNumber : 0,
                            Customs_clearance_office_code =
                                c.x.PreviousDocumentItem != null
                                    ? c.x.PreviousDocumentItem.AsycudaDocument.Customs_clearance_office_code
                                    : "",
                            pQuantity =
                                (double)
                                    (c.x.PreviousDocumentItem != null
                                        ? c.x.PreviousDocumentItem.xcuda_Tarification.xcuda_Supplementary_unit
                                            .FirstOrDefault().Suppplementary_unit_quantity
                                        : 0),
                            pRegistrationDate = (DateTime) c.x.PreviousDocumentItem.AsycudaDocument.RegistrationDate,
                            Country_of_origin_code =
                                c.x.PreviousDocumentItem.xcuda_Goods_description.Country_of_origin_code,
                            Total_CIF_itm =
                                c.x.PreviousDocumentItem != null
                                    ? c.x.PreviousDocumentItem.xcuda_Valuation_item.Total_CIF_itm
                                    : 0,
                            Net_weight_itm = c.w.FirstOrDefault() != null ? c.w.FirstOrDefault().Net_weight_itm : 0,
                            // Net_weight_itm = c.x.PreviousDocumentItem != null ? ctx.xcuda_Weight_itm.FirstOrDefault(q => q.Valuation_item_Id == x.PreviousItem_Id).Net_weight_itm: 0,
                            previousItems =
                                c.x.PreviousDocumentItem.EntryPreviousItems.Select(y => y.xcuda_PreviousItem)
                                    .Where(y => y.xcuda_Item.AsycudaDocument.CNumber != null && y.xcuda_Item.AsycudaDocument.Cancelled != true)
                                    .Select(z => new previousItems()
                                    {
                                        DutyFreePaid =
                                            z.xcuda_Item.AsycudaDocument.Type_of_declaration == "EX"
                                                ? "Duty Free"
                                                : "Duty Paid",
                                        Net_weight = z.Net_weight,
                                        Suplementary_Quantity = z.Suplementary_Quantity
                                    }).ToList(),
                            TariffSupUnitLkps =
                                c.x.EntryDataDetails.InventoryItem.TariffCodes.TariffCategory.TariffSupUnitLkps.ToList()
                            //.Select(x => (ITariffSupUnitLkp)x)

                        }
                        ).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return res;
        }


        public async Task<IEnumerable<EX9AsycudaSalesAllocations>> GetEX9AsycudaSalesAllocations(string FilterExpression)
        {
            try
            {

                //get total count

                using (var ctx = new EX9AsycudaSalesAllocationsService())
                {
                    var tot = await ctx.Count(FilterExpression).ConfigureAwait(false);

                    var res = await ctx.GetEX9AsycudaSalesAllocationsByBatch(FilterExpression, tot, new List<string>()
                    {
                        "PreviousDocumentItem.EntryPreviousItems.xcuda_PreviousItem.xcuda_Item.AsycudaDocument"
                        ,
                        "PreviousDocumentItem.xcuda_Tarification.xcuda_Supplementary_unit"

                    }).ConfigureAwait(false); //, plst

                    return res;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private IEnumerable<AllocationDataBlock> CreateWholeAllocationDataBlocks(
            IEnumerable<EX9SalesAllocations> slstSource)
        {
            IEnumerable<AllocationDataBlock> slst;
            if (PerIM7 == true)
            {
                slst = CreateWholeIM7AllocationDataBlocks(slstSource);
            }
            else
            {
                slst = CreateWholeNonIM7AllocationDataBlocks(slstSource);
            }
            return slst;
        }

        private IEnumerable<AllocationDataBlock> CreateWholeNonIM7AllocationDataBlocks(
            IEnumerable<EX9SalesAllocations> slstSource)
        {
            try
            {

                IEnumerable<AllocationDataBlock> slst;
                var source = slstSource.OrderBy(x => x.pTariffCode).ToList();

                slst = from s in source
                    group s by new {s.DutyFreePaid, MonthYear = "NoMTY"}
                    into g
                    select new AllocationDataBlock
                    {
                        MonthYear = g.Key.MonthYear,
                        DutyFreePaid = g.Key.DutyFreePaid,
                        Allocations = g.ToList(),
                    };
                return slst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<AllocationDataBlock> CreateWholeIM7AllocationDataBlocks(
            IEnumerable<EX9SalesAllocations> slstSource)
        {
            try
            {
                IEnumerable<AllocationDataBlock> slst;
                slst = from s in slstSource.OrderBy(x => x.pTariffCode)
                    group s by
                        new
                        {
                            s.DutyFreePaid,
                            MonthYear = "NoMTY",
                            CNumber = s.pCNumber
                        }
                    into g
                    select new AllocationDataBlock
                    {
                        MonthYear = g.Key.MonthYear,
                        DutyFreePaid = g.Key.DutyFreePaid,
                        Allocations = g.ToList(),
                        CNumber = g.Key.CNumber
                    };
                return slst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<AllocationDataBlock> CreateBreakOnMonthYearAllocationDataBlocks(
            IEnumerable<EX9SalesAllocations> slstSource)
        {
            try
            {
                IEnumerable<AllocationDataBlock> slst;
                if (PerIM7 == true)
                {
                    slst = CreatePerIM7AllocationDataBlocks(slstSource);
                }
                else
                {
                    slst = CreateAllocationDataBlocks(slstSource);
                }
                return slst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<AllocationDataBlock> CreateAllocationDataBlocks(
            IEnumerable<EX9SalesAllocations> slstSource)
        {
            try
            {
                IEnumerable<AllocationDataBlock> slst;
                slst = from s in slstSource
                    group s by
                        new
                        {
                            s.DutyFreePaid,
                            MonthYear =
                                Convert.ToDateTime(
                                    s.InvoiceDate)
                                    .ToString("MMM-yy")
                        }
                    into g
                    select new AllocationDataBlock
                    {
                        MonthYear = g.Key.MonthYear,
                        DutyFreePaid = g.Key.DutyFreePaid,
                        Allocations = g.ToList(),
                    };
                return slst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IEnumerable<AllocationDataBlock> CreatePerIM7AllocationDataBlocks(
            IEnumerable<EX9SalesAllocations> slstSource)
        {
            try
            {

                IEnumerable<AllocationDataBlock> slst;
                slst = from s in slstSource
                    group s by
                        new
                        {
                            s.DutyFreePaid,
                            MonthYear =
                                Convert.ToDateTime(
                                    s.InvoiceDate)
                                    .ToString("MMM-yy"),
                            CNumber = s.pCNumber
                        }
                    into g
                    select new AllocationDataBlock
                    {
                        MonthYear = g.Key.MonthYear,
                        DutyFreePaid = g.Key.DutyFreePaid,
                        Allocations = g.ToList(),
                        CNumber = g.Key.CNumber
                    };
                return slst;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<MyPodData> PrepareAllocationsData(AllocationDataBlock monthyear)
        {
            try
            {
                List<MyPodData> elst;
                if (BaseDataModel.Instance.CurrentApplicationSettings.GroupEX9 == true)
                {
                    elst = GroupAllocationsByEx9(monthyear);
                }
                else
                {
                    elst = GroupAllocations(monthyear);
                }

                return elst.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<MyPodData> GroupAllocations(AllocationDataBlock monthyear)
        {
            try
            {


                List<MyPodData> elst;
                elst =
                    (from s in
                        Enumerable.OrderBy<EX9SalesAllocations, string>(monthyear.Allocations, p => p.pTariffCode)
                            .GroupBy(x => x.PreviousItem_Id)
                            .Where(z => z.ToList().Any(q => q.SalesQuantity < 0) == false)
                            .SelectMany(x => x.ToList())
                        select new MyPodData
                        {
                            Allocations = new List<AsycudaSalesAllocations>()
                            {
                                new AsycudaSalesAllocations()
                                {
                                    AllocationId = s.AllocationId,
                                    EntryDataDetailsId = s.EntryDataDetailsId,
                                    PreviousItem_Id = s.PreviousItem_Id,
                                    Status = s.Status,
                                    SANumber = 0,
                                    EntryDataDetails = new EntryDataDetails()
                                    {
                                        EntryDataDetailsId = s.EntryDataDetailsId,
                                        EntryDataId = s.InvoiceNo,
                                    }

                                }
                            },
                            EntlnData = new AlloEntryLineData
                            {
                                ItemNumber = s.ItemNumber,
                                ItemDescription = s.ItemDescription,
                                TariffCode = s.pTariffCode,
                                Cost = Convert.ToDouble(s.pItemCost),
                                Quantity = s.QtyAllocated/s.SalesFactor,
                                EntryDataDetails = new List<EntryDataDetailSummary>()
                                {
                                    new EntryDataDetailSummary()
                                    {
                                        EntryDataDetailsId = s.EntryDataDetailsId,
                                        EntryDataId = s.InvoiceNo
                                    }
                                },
                                PreviousDocumentItemId = s.PreviousItem_Id.ToString(),
                                pDocumentItem = new pDocumentItem()
                                {
                                    DFQtyAllocated = s.DFQtyAllocated,
                                    DPQtyAllocated = s.DPQtyAllocated,
                                    LineNumber = s.LineNumber,
                                    previousItems = s.previousItems,
                                    ItemQuantity = s.pQuantity
                                },
                                EX9Allocation = new EX9Allocation()
                                {
                                    Country_of_origin_code = s.Country_of_origin_code,
                                    Customs_clearance_office_code = s.Customs_clearance_office_code,
                                    pCNumber = s.pCNumber,
                                    pQtyAllocated = s.DFQtyAllocated + s.DPQtyAllocated,
                                    pQuantity = s.pQuantity,
                                    pTariffCode = s.pTariffCode,
                                    pRegistrationDate = s.pRegistrationDate,
                                    SalesFactor = s.SalesFactor,
                                    Net_weight_itm = s.Net_weight_itm,
                                    Total_CIF_itm = s.Total_CIF_itm
                                }
                            }
                        }).ToList();
                // group the returns
                var returnlst =
                    (from s in
                        Enumerable.OrderBy<EX9SalesAllocations, string>(monthyear.Allocations, p => p.pTariffCode)
                            .GroupBy(x => x.PreviousItem_Id)
                            .Where(z => z.ToList().Any(q => q.SalesQuantity < 0) == true)
                        select new MyPodData
                        {
                            Allocations =
                                s.Select(
                                    x =>
                                        new AsycudaSalesAllocations()
                                        {
                                            AllocationId = x.AllocationId,
                                            PreviousItem_Id = x.PreviousItem_Id,
                                            EntryDataDetailsId = x.EntryDataDetailsId,
                                            Status = x.Status,
                                            EntryDataDetails = new EntryDataDetails()
                                            {
                                                EntryDataDetailsId = x.EntryDataDetailsId,
                                                EntryDataId = x.InvoiceNo,
                                            }
                                        }).ToList(),
                            EntlnData = new AlloEntryLineData
                            {
                                ItemNumber = s.LastOrDefault().ItemNumber,
                                ItemDescription = s.LastOrDefault().ItemDescription,
                                TariffCode = s.LastOrDefault().pTariffCode,
                                Cost = s.LastOrDefault().pItemCost,
                                Quantity = s.Sum(x => x.QtyAllocated/x.SalesFactor),
                                EntryDataDetails = new List<EntryDataDetailSummary>()
                                {
                                    new EntryDataDetailSummary()
                                    {
                                        EntryDataDetailsId = s.LastOrDefault().EntryDataDetailsId,
                                        EntryDataId = s.LastOrDefault().InvoiceNo
                                    }
                                },
                                PreviousDocumentItemId = s.Key.ToString(),
                                InternalFreight = s.LastOrDefault().InternalFreight,
                                Freight = s.LastOrDefault().Freight,
                                Weight = s.LastOrDefault().Weight,
                                pDocumentItem = new pDocumentItem()
                                {
                                    DFQtyAllocated = s.LastOrDefault().DFQtyAllocated,
                                    DPQtyAllocated = s.LastOrDefault().DPQtyAllocated,
                                    ItemQuantity = s.LastOrDefault().pQuantity,
                                    LineNumber = s.LastOrDefault().LineNumber,
                                    previousItems = s.LastOrDefault().previousItems
                                },
                                EX9Allocation = new EX9Allocation()
                                {
                                    SalesFactor = s.LastOrDefault().SalesFactor,
                                    Net_weight_itm = s.LastOrDefault().Net_weight_itm,
                                    pQuantity = s.LastOrDefault().pQuantity,
                                    pCNumber = s.LastOrDefault().pCNumber,
                                    Customs_clearance_office_code = s.LastOrDefault().Customs_clearance_office_code,
                                    Country_of_origin_code = s.LastOrDefault().Country_of_origin_code,
                                    pRegistrationDate = s.LastOrDefault().pRegistrationDate,
                                    pQtyAllocated = s.LastOrDefault().QtyAllocated,
                                    Total_CIF_itm = s.LastOrDefault().Total_CIF_itm,
                                    pTariffCode = s.LastOrDefault().pTariffCode
                                },
                                TariffSupUnitLkps =
                                    s.LastOrDefault().TariffSupUnitLkps.Select(x => (ITariffSupUnitLkp) x).ToList()
                            }
                        }).ToList();
                elst.AddRange(returnlst);
                
                return elst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<MyPodData> GroupAllocationsByEx9(
            AllocationDataBlock monthyear)
        {
            try
            {

                var elst = monthyear.Allocations.GroupBy(x => x.PreviousItem_Id);
                //var elst = from s in monthyear.Allocations

                //    //  where s.EntryDataDetails.ItemNumber == "SPG20331"
                //    group s by s.PreviousDocumentItem.Item_Id
                //    into g ;
                var res = new ConcurrentQueue<MyPodData>();
                Parallel.ForEach(elst, new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount*1},
                    g =>
                    {
                        var itm =
                            new MyPodData
                            {
                                Allocations = g.Select(x => new AsycudaSalesAllocations()
                                {
                                    AllocationId = x.AllocationId,
                                    PreviousItem_Id = x.PreviousItem_Id,
                                    EntryDataDetailsId = x.EntryDataDetailsId,
                                    Status = x.Status,
                                    QtyAllocated = x.QtyAllocated,
                                    EntryDataDetails = new EntryDataDetails()
                                    {
                                        EntryDataDetailsId = x.EntryDataDetailsId,
                                        EntryDataId = x.InvoiceNo,
                                    }
                                }).ToList(),
                                EntlnData = new AlloEntryLineData
                                {
                                    //ItemNumber = g.Key.ItemNumber,
                                    ItemNumber = g.LastOrDefault().pItemNumber,
                                    // InventoryItem = BaseDataModel.Instance.InventoryCache.GetSingle(x => x.ItemNumber == g.LastOrDefault().pItemNumber),
                                    //ItemDescription = g.Key.ItemDescription,
                                    ItemDescription = g.LastOrDefault().Commercial_Description,
                                    //TariffCode = g.Key.TariffCode,
                                    TariffCode = g.LastOrDefault().pTariffCode,
                                    Cost = Convert.ToDouble(g.LastOrDefault().pItemCost),
                                    // InventoryItem = g.Key.InventoryItems,
                                    Quantity = g.Sum(x => x.QtyAllocated/x.SalesFactor),
                                    EntryDataDetails =
                                        g.Select(x => new EntryDataDetailSummary()
                                        {
                                            EntryDataDetailsId = x.EntryDataDetailsId,
                                            EntryDataId = x.InvoiceNo
                                        }).ToList(),
                                    //g.Select(x => new EntryDataDetails()
                                    //{
                                    //    EntryDataDetailsId = x.EntryDataDetailsId,
                                    //    EntryDataId = x.InvoiceNo,
                                    //    QtyAllocated = x.SalesQtyAllocated,
                                    //    Quantity = x.SalesQuantity,
                                    //    Sales = new Sales() { EntryDataId = x.InvoiceNo, EntryDataDate = x.InvoiceDate }
                                    //} as IEntryDataDetail).ToList(),
                                    PreviousDocumentItemId = g.LastOrDefault().PreviousItem_Id.ToString(),
                                    pDocumentItem = new pDocumentItem()
                                    {
                                        DFQtyAllocated = g.LastOrDefault().DFQtyAllocated,
                                        DPQtyAllocated = g.LastOrDefault().DPQtyAllocated,
                                        LineNumber = g.LastOrDefault().LineNumber,
                                        previousItems = g.LastOrDefault().previousItems,
                                        ItemQuantity = g.LastOrDefault().pQuantity,

                                    },
                                    EX9Allocation = new EX9Allocation()
                                    {
                                        Country_of_origin_code = g.LastOrDefault().Country_of_origin_code,
                                        Customs_clearance_office_code = g.LastOrDefault().Customs_clearance_office_code,
                                        pCNumber = g.LastOrDefault().pCNumber,
                                        pQtyAllocated =
                                            g.LastOrDefault().DFQtyAllocated + g.LastOrDefault().DPQtyAllocated,
                                        pQuantity = g.LastOrDefault().pQuantity,
                                        pTariffCode = g.LastOrDefault().pTariffCode,
                                        pRegistrationDate = g.LastOrDefault().pRegistrationDate,
                                        Net_weight_itm = g.LastOrDefault().Net_weight_itm,
                                        SalesFactor = g.LastOrDefault().SalesFactor,
                                        Total_CIF_itm = g.LastOrDefault().Total_CIF_itm


                                    },
                                    Freight = g.LastOrDefault().Freight,
                                    InternalFreight = g.LastOrDefault().InternalFreight,
                                    Weight = g.LastOrDefault().Weight,
                                    TariffSupUnitLkps =
                                        g.LastOrDefault().TariffSupUnitLkps.Select(x => (ITariffSupUnitLkp) x).ToList()
                                }
                            };
                        res.Enqueue(itm);
                    });
                return res.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertEntryIdintoRefNum(DocumentCT cdoc, string entryDataId)
        {
            try
            {
                if (BaseDataModel.Instance.CurrentApplicationSettings.InvoicePerEntry == true)
                {
                    cdoc.Document.xcuda_Declarant.Number = entryDataId;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> CreateEx9EntryAsync(dynamic mypod, DocumentCT cdoc, int itmcount, string dfp,
            bool applyEX9Bucket)
        {
            try
            {

                if (applyEX9Bucket == true)
                {
                    await Ex9Bucket(mypod, dfp).ConfigureAwait(false);
                }



                mypod.EntlnData.Quantity = Math.Round(mypod.EntlnData.Quantity, 2);
                if (mypod.EntlnData.Quantity <= 0) return false;

                global::DocumentItemDS.Business.Entities.xcuda_PreviousItem pitm = CreatePreviousItem(mypod.EntlnData,
                    itmcount, dfp);
                if (pitm.Net_weight < 0.01)
                {
                   return false;
                }


                
                pitm.ASYCUDA_Id = cdoc.Document.ASYCUDA_Id;
                global::DocumentItemDS.Business.Entities.xcuda_Item itm =
                    BaseDataModel.Instance.CreateItemFromEntryDataDetail(mypod.EntlnData, cdoc);

                

                //TODO:Refactor this dup code
                if (mypod.Allocations != null)
                {
                    var itmcnt = 1;
                    foreach (
                        var allo in (mypod.Allocations as List<AsycudaSalesAllocations>)) //.Distinct()
                    {
                        itm.xBondAllocations.Add(new xBondAllocations(true)
                        {
                            AllocationId = allo.AllocationId,
                            xcuda_Item = itm,
                            TrackingState = TrackingState.Added
                        });

                        itmcnt = AddFreeText(itmcnt, itm, allo.EntryDataDetails.EntryDataId);
                    }
                }
                


                itm.xcuda_PreviousItem = pitm;
                pitm.xcuda_Item = itm;

                if (pitm.Previous_Packages_number != null && pitm.Previous_Packages_number != "0")
                    itm.xcuda_Packages.FirstOrDefault().Number_of_packages =
                        Convert.ToDouble(pitm.Previous_Packages_number);

                

                itm.xcuda_Tarification.xcuda_HScode.Commodity_code = pitm.Hs_code;
                itm.xcuda_Goods_description.Country_of_origin_code = pitm.Goods_origin;


                itm.xcuda_Previous_doc.Summary_declaration = String.Format("{0} {1} C {2} art. {3}", pitm.Prev_reg_cuo,
                    pitm.Prev_reg_dat,
                    pitm.Prev_reg_nbr, pitm.Previous_item_number);
                itm.xcuda_Valuation_item.xcuda_Weight_itm = new xcuda_Weight_itm(true)
                {
                    TrackingState = TrackingState.Added
                };
                itm.xcuda_Valuation_item.xcuda_Weight_itm.Gross_weight_itm = pitm.Net_weight;
                itm.xcuda_Valuation_item.xcuda_Weight_itm.Net_weight_itm = pitm.Net_weight;
                // adjusting because not using real statistical value when calculating
                itm.xcuda_Valuation_item.xcuda_Item_Invoice.Amount_foreign_currency =
                    Convert.ToDouble(Math.Round((pitm.Current_value*pitm.Suplementary_Quantity), 2));
                itm.xcuda_Valuation_item.xcuda_Item_Invoice.Amount_national_currency =
                    Convert.ToDouble(Math.Round(pitm.Current_value*pitm.Suplementary_Quantity, 2));
                itm.xcuda_Valuation_item.xcuda_Item_Invoice.Currency_code = "XCD";
                itm.xcuda_Valuation_item.xcuda_Item_Invoice.Currency_rate = 1;


                if (cdoc.DocumentItems.Select(x => x.xcuda_PreviousItem).Count() == 1 || itmcount == 0)
                {
                    pitm.Packages_number = "1"; //(i.Packages.Number_of_packages).ToString();
                    pitm.Previous_Packages_number = pitm.Previous_item_number == "1" ? "1" : "0";
                }
                else
                {
                    if (pitm.Packages_number == null)
                    {
                        pitm.Packages_number = (0).ToString(CultureInfo.InvariantCulture);
                        pitm.Previous_Packages_number = (0).ToString(CultureInfo.InvariantCulture);
                    }
                }



                return true;
            }
            catch (Exception Ex)
            {
                throw;
            }


        }

        private int AddFreeText(int itmcnt, global::DocumentItemDS.Business.Entities.xcuda_Item itm, string entryDataId)
        {
            if (BaseDataModel.Instance.CurrentApplicationSettings.GroupEX9 == true) return itmcnt;
            if (itm.Free_text_1 == null) itm.Free_text_1 = "";
            itm.Free_text_1 = $"{entryDataId}|{itmcnt}";
            itmcnt += 1;

            if (itm.Free_text_1 != null && itm.Free_text_1.Length > 1)
            {
                itm.Free_text_1 = itm.Free_text_1.Length < 31 ? itm.Free_text_1.Substring(0) 
                                                              : itm.Free_text_1.Substring(0, 30);
            }


            if (itm.Free_text_2 != null && itm.Free_text_2.Length > 1)
            {
                itm.Free_text_2 = itm.Free_text_2.Length < 21 ? itm.Free_text_2.Substring(0) 
                                                              : itm.Free_text_2.Substring(0, 20);
            }
            return itmcnt;
        }
    

    private  async Task Ex9Bucket(MyPodData mypod, string dfp)
        {
            // prevent over draw down of pqty == quantity allocated
            try
            {
                
                var eld = mypod.EntlnData;
                var previousItem = mypod.EntlnData.pDocumentItem;
                
                if (previousItem == null) return;
                var PdfpAllocated = (dfp == "Duty Free" ? previousItem.DFQtyAllocated : previousItem.DPQtyAllocated);
                if (PdfpAllocated > previousItem.ItemQuantity) PdfpAllocated = (int) previousItem.ItemQuantity;
                //if (previousItem.QtyAllocated == 0) return;

                var maxQuantityToEx9 = previousItem.ItemQuantity;//PdfpAllocated;//


                if (previousItem.previousItems.Any() == false && eld.Quantity > maxQuantityToEx9)
                {
                    eld.Quantity = maxQuantityToEx9;
                    return;
                }

                var plst = previousItem.previousItems;
                var pqty = plst.Where(x => x.DutyFreePaid == dfp).Sum(xx => xx.Suplementary_Quantity);
                var apqty = plst.Sum(xx => xx.Suplementary_Quantity);
                
                if (maxQuantityToEx9 == apqty)
                {
                    mypod.Allocations.Clear();
                    eld.EntryDataDetails.Clear();
                    eld.Quantity = 0;
                    return;
                }
                if (PdfpAllocated - (pqty + eld.Quantity) < 0)
                {

                    var qty = PdfpAllocated - (pqty);
                    if (qty + apqty > maxQuantityToEx9) qty = maxQuantityToEx9 - apqty;
                    var lst = eld.EntryDataDetails.OrderBy(x => x.EntryDataDate).ToList();

                    var lqty = mypod.Allocations.Sum(x => x.QtyAllocated);
                    while (qty < lqty)
                    {
                        if (lst.Any() == false) break;

                        var entlst = mypod.Allocations.Where(x => x.EntryDataDetailsId == lst.ElementAt(0).EntryDataDetailsId).ToList();
                        foreach (var item in entlst)
                        {
                            lqty -= item.QtyAllocated;
                            mypod.Allocations.Remove(item);
                        }


                        eld.EntryDataDetails.Remove(lst.ElementAt(0));
                        lst.RemoveAt(0);
                    }


                    eld.Quantity = qty;
                    return;
                }
                if (eld.Quantity + apqty > maxQuantityToEx9) eld.Quantity = maxQuantityToEx9 - apqty;


            }
            catch (Exception Ex)
            {
                throw;
            }

        }

        private global::DocumentItemDS.Business.Entities.xcuda_PreviousItem CreatePreviousItem(AlloEntryLineData pod, int itmcount, string dfp)
        {

            try
            {
                var previousItem = pod.pDocumentItem;

                var pitm = new global::DocumentItemDS.Business.Entities.xcuda_PreviousItem(true) { TrackingState = TrackingState.Added };
                if (previousItem == null) return pitm;

                pitm.Hs_code = pod.EX9Allocation.pTariffCode;
                pitm.Commodity_code = "00";
                pitm.Current_item_number = (itmcount + 1).ToString(); // piggy back the previous item count
                pitm.Previous_item_number = previousItem.LineNumber.ToString();


                SetWeights(pod, pitm, dfp);


                pitm.Previous_Packages_number = "0";


                pitm.Suplementary_Quantity = Convert.ToDouble(pod.Quantity);
                pitm.Preveious_suplementary_quantity = Convert.ToDouble(pod.EX9Allocation.pQuantity);


                pitm.Goods_origin = pod.EX9Allocation.Country_of_origin_code;
                double pval = pod.EX9Allocation.Total_CIF_itm;//previousItem.xcuda_Valuation_item.xcuda_Item_Invoice.Amount_national_currency;
                pitm.Previous_value = Convert.ToDouble((pval / pod.EX9Allocation.pQuantity));
                pitm.Current_value = Convert.ToDouble((pval) / Convert.ToDouble(pod.EX9Allocation.pQuantity));
                pitm.Prev_reg_ser = "C";
                pitm.Prev_reg_nbr = pod.EX9Allocation.pCNumber;
                pitm.Prev_reg_dat = pod.EX9Allocation.pRegistrationDate.Year.ToString();
                pitm.Prev_reg_cuo = pod.EX9Allocation.Customs_clearance_office_code;

                return pitm;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SetWeights(AlloEntryLineData pod, global::DocumentItemDS.Business.Entities.xcuda_PreviousItem pitm, string dfp)
        {
            try
            {
                var previousItem = pod.pDocumentItem;
                if (previousItem == null) return;
                var plst = previousItem.previousItems;
                var pw = Convert.ToDouble(pod.EX9Allocation.Net_weight_itm);
                
                var iw = Convert.ToDouble((pod.EX9Allocation.Net_weight_itm
                                           / pod.EX9Allocation.pQuantity) * Convert.ToDouble(pod.Quantity));

                //var ppdfpqty =
                //        plst.Where(x => x.DutyFreePaid != dfp)
                //        .Sum(x => x.Suplementary_Quantity);
                //var pi = pod.EX9Allocation.pQuantity -
                //         plst.Where(x => x.DutyFreePaid == dfp)
                //             .Sum(x => x.Suplementary_Quantity);
                //var pdfpqty = (dfp == "Duty Free"
                //    ? previousItem.DPQtyAllocated
                //    : previousItem.DFQtyAllocated);
                var rw = plst.ToList().Sum(x => x.Net_weight);

                if ((pod.EX9Allocation.pQuantity - (plst.Sum(x => x.Suplementary_Quantity) + pod.Quantity))  <= 0)
                {

                    pitm.Net_weight = Math.Round(Convert.ToDouble(pw - rw), 2, MidpointRounding.ToEven);
                }
                else
                {
                    pitm.Net_weight = Convert.ToDouble(Math.Round(iw, 2));
                }

                pitm.Prev_net_weight = pw; //Convert.ToDouble((pw/pod.EX9Allocation.SalesFactor) - rw);
                if (pitm.Net_weight > pitm.Prev_net_weight) pitm.Net_weight = pitm.Prev_net_weight;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Ex9InitializeCdoc(Document_Type dt, string dfp, DocumentCT cdoc, AsycudaDocumentSet ads)
        {
            try
            {

                cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.AutoUpdate = false;
                BaseDataModel.Instance.IntCdoc(cdoc, dt, ads);

                switch (dfp)
                {
                    case "Duty Free":
                        var Exp = BaseDataModel.Instance.ExportTemplates.FirstOrDefault(y => y.Description.ToUpper() == "EX9".ToUpper());
                        if (Exp.Customs_Procedure == null || string.IsNullOrEmpty(Exp.Customs_Procedure))
                        {
                            throw new ApplicationException("Export Template default Customs Procedures not Configured!");
                        }
                        cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Description = "Duty Free Entries";
                        var df  =
                            BaseDataModel.Instance.Customs_Procedures.AsEnumerable()
                                .FirstOrDefault(
                                    x =>
                                        x.DisplayName ==
                                        ((Exp == null || string.IsNullOrEmpty(Exp.Customs_Procedure))
                                            ? "9070-000"
                                            : Exp.Customs_Procedure));
                       
                            BaseDataModel.Instance.AttachCustomProcedure(cdoc, df);
                        
                        break;
                    case "Duty Paid":
                        var Exp1 = BaseDataModel.Instance.ExportTemplates.FirstOrDefault(y => y.Description == "IM4");
                        if (Exp1.Customs_Procedure == null || string.IsNullOrEmpty(Exp1.Customs_Procedure))
                        {
                            throw new ApplicationException("Export Template default Customs Procedures not Configured!");
                        }
                        cdoc.Document.xcuda_ASYCUDA_ExtendedProperties.Description = "Duty Paid Entries";
                        var dp =
                            BaseDataModel.Instance.Customs_Procedures.AsEnumerable()
                                .FirstOrDefault(
                                    x =>
                                        x.DisplayName ==
                                        ((Exp1 == null || string.IsNullOrEmpty(Exp1.Customs_Procedure))
                                            ? "4070-000"
                                            : Exp1.Customs_Procedure));
                        BaseDataModel.Instance.AttachCustomProcedure(cdoc, dp);
                        break;
                    default:
                        break;
                }

                AllocationsModel.Instance.AddDutyFreePaidtoRef(cdoc, dfp, ads);

            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public class AlloEntryLineData: BaseDataModel.IEntryLineData //: AllocationsModel.AlloEntryLineData
        {
            public double Cost { get; set; }
            public List<EntryDataDetailSummary> EntryDataDetails { get; set; }
            public double Weight { get; set; }
            public double InternalFreight { get; set; }
            public double Freight { get; set; }
            public List<ITariffSupUnitLkp> TariffSupUnitLkps { get; set; }
            public string ItemDescription { get; set; }
            public string ItemNumber { get; set; }
            public string PreviousDocumentItemId { get; set; }
            public double Quantity { get; set; }
            public string TariffCode { get; set; }
            public pDocumentItem pDocumentItem { get; set; }
            public EX9Allocation EX9Allocation { get; set; }
            
        }

        public class pDocumentItem
        {
            public int LineNumber { get; set; }
            public int DPQtyAllocated { get; set; }
            public List<previousItems> previousItems { get; set; }
            public int DFQtyAllocated { get; set; }

            public int QtyAllocated => DFQtyAllocated + DPQtyAllocated;

            public double ItemQuantity { get; set; }
        }

        public class EX9Allocation
        {
            public string pTariffCode { get; set; }
            public double pQuantity { get; set; }
            public string Country_of_origin_code { get; set; }
            public double Total_CIF_itm { get; set; }
            public string pCNumber { get; set; }
            public DateTime pRegistrationDate { get; set; }
            public string Customs_clearance_office_code { get; set; }
            public double Net_weight_itm { get; set; }
            public double pQtyAllocated { get; set; }
            public double SalesFactor { get; set; }
        }
    }

    public class previousItems
    {
        public string DutyFreePaid { get; set; }
        public double Suplementary_Quantity { get; set; }
        public double Net_weight { get; set; }
    }


    public class EX9SalesAllocations
    {
        public string pTariffCode { get; set; }
        public string DutyFreePaid { get; set; }
        public string pCNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int? PreviousItem_Id { get; set; }
        public int SalesQuantity { get; set; }
        public int AllocationId { get; set; }
        public int EntryDataDetailsId { get; set; }
        public string Status { get; set; }
        public string InvoiceNo { get; set; }
        public string ItemNumber { get; set; }
        public string pItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public double pItemCost { get; set; }
        public int QtyAllocated { get; set; }
        public string Commercial_Description { get; set; }
        public double SalesQtyAllocated { get; set; }
        public int DFQtyAllocated { get; set; }
        public int DPQtyAllocated { get; set; }
        public int LineNumber { get; set; }
        public List<previousItems> previousItems { get; set; }
        public string Country_of_origin_code { get; set; }
        public string Customs_clearance_office_code { get; set; }
        public double pQuantity { get; set; }
        public DateTime pRegistrationDate { get; set; }
        public double Net_weight_itm { get; set; }
        public double Total_CIF_itm { get; set; }
        public double Freight { get; set; }
        public double InternalFreight { get; set; }
        public double Weight { get; set; }
        public List<TariffSupUnitLkps> TariffSupUnitLkps { get; set; }
        public double SalesFactor { get; set; }
    }

    public class AllocationDataBlock
    {
        public string MonthYear { get; set; }
        public string DutyFreePaid { get; set; }
        public List<EX9SalesAllocations> Allocations { get; set; }
        public string CNumber { get; set; }
    }

    public class MyPodData
    {
        public List<AsycudaSalesAllocations> Allocations { get; set; }
        public CreateEx9Class.AlloEntryLineData EntlnData { get; set; }
    }

    public class AlloEntryLineData
    {
    }
}