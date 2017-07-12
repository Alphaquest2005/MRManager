﻿// <autogenerated>
//   This file was generated by T4 code generator AllDataSpaceViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using OversShortQS.Business.Entities;
using OversShortQS.Business.Services;

using System.Threading.Tasks;
using System.Collections.Generic;


//using WaterNut.Client.Repositories;



namespace WaterNut.DataSpace.OversShortQS.DataModels
{
	 public partial class BaseDataModel 
	{
        private static readonly BaseDataModel instance;
        static BaseDataModel()
        {
            instance = new BaseDataModel();
        }

        public static  BaseDataModel Instance
        {
            get { return instance; }
        }

       //Search Entities
   
        public async Task<IEnumerable<AsycudaDocument>> SearchAsycudaDocument(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new AsycudaDocumentService())
            {
                return await ctx.GetAsycudaDocumentsByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveAsycudaDocument(AsycudaDocument i)
        {
            if (i == null) return;
            using (var ctx = new AsycudaDocumentService())
            {
                await ctx.UpdateAsycudaDocument(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<AsycudaDocumentItem>> SearchAsycudaDocumentItem(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new AsycudaDocumentItemService())
            {
                return await ctx.GetAsycudaDocumentItemsByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveAsycudaDocumentItem(AsycudaDocumentItem i)
        {
            if (i == null) return;
            using (var ctx = new AsycudaDocumentItemService())
            {
                await ctx.UpdateAsycudaDocumentItem(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<InventoryItem>> SearchInventoryItem(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new InventoryItemService())
            {
                return await ctx.GetInventoryItemsByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveInventoryItem(InventoryItem i)
        {
            if (i == null) return;
            using (var ctx = new InventoryItemService())
            {
                await ctx.UpdateInventoryItem(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<OverShortAllocationsEX>> SearchOverShortAllocationsEX(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new OverShortAllocationsEXService())
            {
                return await ctx.GetOverShortAllocationsEXesByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveOverShortAllocationsEX(OverShortAllocationsEX i)
        {
            if (i == null) return;
            using (var ctx = new OverShortAllocationsEXService())
            {
                await ctx.UpdateOverShortAllocationsEX(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<OverShortDetail>> SearchOverShortDetail(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new OverShortDetailService())
            {
                return await ctx.GetOverShortDetailsByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveOverShortDetail(OverShortDetail i)
        {
            if (i == null) return;
            using (var ctx = new OverShortDetailService())
            {
                await ctx.UpdateOverShortDetail(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<OverShortDetailAllocation>> SearchOverShortDetailAllocation(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new OverShortDetailAllocationService())
            {
                return await ctx.GetOverShortDetailAllocationsByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveOverShortDetailAllocation(OverShortDetailAllocation i)
        {
            if (i == null) return;
            using (var ctx = new OverShortDetailAllocationService())
            {
                await ctx.UpdateOverShortDetailAllocation(i).ConfigureAwait(false);
            }
        }
   
        //public async Task<IEnumerable<OverShortDetailsEX>> SearchOverShortDetailsEX(List<string> lst, List<string> includeLst = null )
        //{
        //    using (var ctx = new OverShortDetailsEXService())
        //    {
        //        return await ctx.GetOverShortDetailsByExpressionLst(lst, includeLst).ConfigureAwait(false);
        //    }
        //}

        //public async Task SaveOverShortDetailsEX(OverShortDetailsEX i)
        //{
        //    if (i == null) return;
        //    using (var ctx = new OverShortDetailsEXService())
        //    {
        //        await ctx.UpdateOverShortDetailsEX(i).ConfigureAwait(false);
        //    }
        //}
   
        public async Task<IEnumerable<OverShortSuggestedDocument>> SearchOverShortSuggestedDocument(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new OverShortSuggestedDocumentService())
            {
                return await ctx.GetOverShortSuggestedDocumentsByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveOverShortSuggestedDocument(OverShortSuggestedDocument i)
        {
            if (i == null) return;
            using (var ctx = new OverShortSuggestedDocumentService())
            {
                await ctx.UpdateOverShortSuggestedDocument(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<OversShort>> SearchOversShort(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new OversShortService())
            {
                return await ctx.GetOversShortsByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        //public async Task SaveOversShort(OversShort i)
        //{
        //    if (i == null) return;
        //    using (var ctx = new OversShortService())
        //    {
        //        await ctx.UpdateOversShort(i).ConfigureAwait(false);
        //    }
        //}
   
        //public async Task<IEnumerable<OversShortEX>> SearchOversShortEX(List<string> lst, List<string> includeLst = null )
        //{
        //    using (var ctx = new OversShortEXService())
        //    {
        //        return await ctx.GetOversShortsByExpressionLst(lst, includeLst).ConfigureAwait(false);
        //    }
        //}

        //public async Task SaveOversShortEX(OversShortEX i)
        //{
        //    if (i == null) return;
        //    using (var ctx = new OversShortEXService())
        //    {
        //        await ctx.UpdateOversShortEX(i).ConfigureAwait(false);
        //    }
        //}
   
    

    }		
}
