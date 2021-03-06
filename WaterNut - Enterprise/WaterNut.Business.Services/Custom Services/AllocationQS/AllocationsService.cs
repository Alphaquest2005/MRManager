﻿
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.ServiceModel;
using System.Threading.Tasks;
using AllocationDS.Business.Entities;
using Core.Common.Contracts;
using ConcurrencyMode = System.ServiceModel.ConcurrencyMode;


namespace AllocationQS.Business.Services
{
    [Export(typeof(IAsycudaSalesAllocationsExService))]
    [Export(typeof(IBusinessService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class AllocationsService : IAllocationsService, IDisposable
   {
       
       

        public async Task CreateEx9(string filterExpression, bool perIM7, bool applyEx9Bucket, bool breakOnMonthYear,
            int AsycudaDocumentSetId)
        {
            var docset =
                await WaterNut.DataSpace.BaseDataModel.Instance.GetAsycudaDocumentSet(AsycudaDocumentSetId, null)
                    .ConfigureAwait(false);
           await WaterNut.DataSpace.CreateEx9Class.Instance.CreateEx9(filterExpression, perIM7, applyEx9Bucket,
                breakOnMonthYear, docset).ConfigureAwait(false);
        }

        public async Task CreateOPS(string filterExpression, int AsycudaDocumentSetId)
        {
            var docset =
               await WaterNut.DataSpace.BaseDataModel.Instance.GetAsycudaDocumentSet(AsycudaDocumentSetId, null)
                   .ConfigureAwait(false);

            //await WaterNut.DataSpace.CreateOPSClass.Instance.CreateOPS(filterExpression, docset).ConfigureAwait(false);
            await WaterNut.DataSpace.CreateErrOPS.Instance.CreateErrorOPS(filterExpression, docset).ConfigureAwait(false);

        }

        public async Task ManuallyAllocate(int AllocationId, int PreviousItem_Id)
        {
            xcuda_Item pitm;
            AsycudaSalesAllocations allo;
            using (var ctx = new AllocationDSContext())
            {
                pitm = ctx.xcuda_Item.FirstOrDefault(x => x.Item_Id == PreviousItem_Id);
                allo = ctx.AsycudaSalesAllocations.FirstOrDefault(x => x.AllocationId == AllocationId);
            }
            
            await WaterNut.DataSpace.AllocationsModel.Instance.ManuallyAllocate(allo, pitm).ConfigureAwait(false);
        }

        public async Task ClearAllocations(IEnumerable<int> alst)
        {
            var allst = new List<AsycudaSalesAllocations>();
            using (var ctx = new AllocationDSContext())
            {
                allst.AddRange(alst.Select(aid => ctx.AsycudaSalesAllocations
                                .Include(x => x.EntryDataDetails)
                                .Include(x => x.PreviousDocumentItem)
                                .FirstOrDefault(x => x.AllocationId == aid)));
            }
            await WaterNut.DataSpace.AllocationsModel.Instance.ClearAllocations(allst).ConfigureAwait(false);
        }

        public async Task ClearAllocationsByFilter(string filterExpression)
        {
            await WaterNut.DataSpace.AllocationsModel.Instance.ClearAllocations(filterExpression).ConfigureAwait(false);
        }

        //public async Task CreateIncompOPS(string filterExpression, int AsycudaDocumentSetId)
        //{
        //    var docset =
        //       await WaterNut.DataSpace.BaseDataModel.Instance.GetAsycudaDocumentSet(AsycudaDocumentSetId, null)
        //           .ConfigureAwait(false);
        //    await
        //        WaterNut.DataSpace.CreateIncompOPSClass.Instance.CreateIncompOPS(filterExpression, docset)
        //            .ConfigureAwait(false);
        //}

        public async Task AllocateSales(bool itemDescriptionContainsAsycudaAttribute)
        {
            await
                WaterNut.DataSpace.AllocationsBaseModel.Instance.AllocateSales(itemDescriptionContainsAsycudaAttribute)
                    .ConfigureAwait(false);
        }

        public async Task ReBuildSalesReports()
        {
            await WaterNut.DataSpace.BuildSalesReportClass.Instance.ReBuildSalesReports().ConfigureAwait(false);
        }

        public async Task ReBuildSalesReports(int asycuda_id)
        {
            AsycudaDocument doc = null;
            using (var ctx = new AllocationDSContext())
            {
                doc = ctx.AsycudaDocument.FirstOrDefault(x => x.ASYCUDA_Id == asycuda_id);
            }
            await WaterNut.DataSpace.BuildSalesReportClass.Instance.ReBuildSalesReports(doc).ConfigureAwait(false);
        }

        #region IDisposable Members

        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        #endregion
   }
}

