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
	public partial class SalesDataModel_AutoGen 
	{
        private static readonly SalesDataModel_AutoGen instance;
        static SalesDataModel_AutoGen()
        {
            instance = new SalesDataModel_AutoGen();
        }

        public static  SalesDataModel_AutoGen Instance
        {
            get { return instance; }
        }

       //Search Entities 
        public async Task<IEnumerable<Sales>> SearchSales(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new SalesService())
            {
                return await ctx.GetEntryDataByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

    }
}
		
