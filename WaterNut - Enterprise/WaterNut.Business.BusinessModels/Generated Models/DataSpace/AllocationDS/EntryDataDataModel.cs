﻿// <autogenerated>
//   This file was generated by T4 code generator AllDataSpaceViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using SimpleMvvmToolkit;
using TrackableEntities;
using System;
using AllocationDS.Business.Entities;
using AllocationDS.Business.Services;




namespace WaterNut.DataSpace.AllocationDS.DataModels
{
	public partial class EntryDataDataModel_AutoGen 
	{
        private static readonly EntryDataDataModel_AutoGen instance;
        static EntryDataDataModel_AutoGen()
        {
            instance = new EntryDataDataModel_AutoGen();
        }

        public static  EntryDataDataModel_AutoGen Instance
        {
            get { return instance; }
        }

       //Search Entities 
        public async Task<IEnumerable<EntryData>> SearchEntryData(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new EntryDataService())
            {
                return await ctx.GetEntryDataByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

    }
}
		
