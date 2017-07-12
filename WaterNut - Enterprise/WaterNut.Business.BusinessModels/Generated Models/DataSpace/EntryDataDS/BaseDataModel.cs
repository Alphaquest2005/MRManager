﻿// <autogenerated>
//   This file was generated by T4 code generator AllDataSpaceViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.ObjectModel;
using System.Linq;
using SimpleMvvmToolkit;
using System;
using System.ComponentModel;

using EntryDataDS.Business.Entities;
using EntryDataDS.Business.Services;

using System.Threading.Tasks;
using System.Collections.Generic;


//using WaterNut.Business.Repositories;



namespace WaterNut.DataSpace.EntryDataDS.DataModels
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
   
        public async Task<IEnumerable<AsycudaDocumentEntryData>> SearchAsycudaDocumentEntryData(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new AsycudaDocumentEntryDataService())
            {
                return await ctx.GetAsycudaDocumentEntryDataByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveAsycudaDocumentEntryData(AsycudaDocumentEntryData i)
        {
            if (i == null) return;
            using (var ctx = new AsycudaDocumentEntryDataService())
            {
                await ctx.UpdateAsycudaDocumentEntryData(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<AsycudaDocumentSetEntryData>> SearchAsycudaDocumentSetEntryData(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new AsycudaDocumentSetEntryDataService())
            {
                return await ctx.GetAsycudaDocumentSetEntryDataByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveAsycudaDocumentSetEntryData(AsycudaDocumentSetEntryData i)
        {
            if (i == null) return;
            using (var ctx = new AsycudaDocumentSetEntryDataService())
            {
                await ctx.UpdateAsycudaDocumentSetEntryData(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<EntryData>> SearchEntryData(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new EntryDataService())
            {
                return await ctx.GetEntryDataByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveEntryData(EntryData i)
        {
            if (i == null) return;
            using (var ctx = new EntryDataService())
            {
                await ctx.UpdateEntryData(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<EntryDataDetails>> SearchEntryDataDetails(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new EntryDataDetailsService())
            {
                return await ctx.GetEntryDataDetailsByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveEntryDataDetails(EntryDataDetails i)
        {
            if (i == null) return;
            using (var ctx = new EntryDataDetailsService())
            {
                await ctx.UpdateEntryDataDetails(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<EntryDataDetailsEx>> SearchEntryDataDetailsEx(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new EntryDataDetailsExService())
            {
                return await ctx.GetEntryDataDetailsExByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveEntryDataDetailsEx(EntryDataDetailsEx i)
        {
            if (i == null) return;
            using (var ctx = new EntryDataDetailsExService())
            {
                await ctx.UpdateEntryDataDetailsEx(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<InventoryItemsEx>> SearchInventoryItemsEx(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new InventoryItemsExService())
            {
                return await ctx.GetInventoryItemsExByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveInventoryItemsEx(InventoryItemsEx i)
        {
            if (i == null) return;
            using (var ctx = new InventoryItemsExService())
            {
                await ctx.UpdateInventoryItemsEx(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<OpeningStock>> SearchOpeningStock(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new OpeningStockService())
            {
                return await ctx.GetEntryDataByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveOpeningStock(OpeningStock i)
        {
            if (i == null) return;
            using (var ctx = new OpeningStockService())
            {
                await ctx.UpdateOpeningStock(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<PurchaseOrders>> SearchPurchaseOrders(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new PurchaseOrdersService())
            {
                return await ctx.GetEntryDataByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SavePurchaseOrders(PurchaseOrders i)
        {
            if (i == null) return;
            using (var ctx = new PurchaseOrdersService())
            {
                await ctx.UpdatePurchaseOrders(i).ConfigureAwait(false);
            }
        }
   
        public async Task<IEnumerable<Sales>> SearchSales(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new SalesService())
            {
                return await ctx.GetEntryDataByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

        public async Task SaveSales(Sales i)
        {
            if (i == null) return;
            using (var ctx = new SalesService())
            {
                await ctx.UpdateSales(i).ConfigureAwait(false);
            }
        }
   
    

    }		
}
