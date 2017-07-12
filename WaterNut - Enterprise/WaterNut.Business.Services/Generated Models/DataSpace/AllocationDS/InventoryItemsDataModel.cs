﻿// <autogenerated>
//   This file was generated by T4 code generator ViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.Generic;
using System.Threading.Tasks;
//using SimpleMvvmToolkit;
using AllocationDS.Business.Entities;
using AllocationDS.Business.Services;




namespace WaterNut.DataSpace.AllocationDS.DataModels
{
	public partial class InventoryItemsDataModel_AutoGen 
	{
        private static readonly InventoryItemsDataModel_AutoGen instance;
        static InventoryItemsDataModel_AutoGen()
        {
            instance = new InventoryItemsDataModel_AutoGen();
        }

        public static  InventoryItemsDataModel_AutoGen Instance
        {
            get { return instance; }
        }

       //Search Entities 
        public async Task<IEnumerable<InventoryItems>> SearchInventoryItems(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new InventoryItemsService())
            {
                return await ctx.GetInventoryItemsByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

    }
}
		
