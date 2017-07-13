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
using DocumentItemDS.Business.Entities;
using DocumentItemDS.Business.Services;




namespace WaterNut.DataSpace.DocumentItemDS.DataModels
{
	public partial class PreviousItemsExDataModel_AutoGen 
	{
        private static readonly PreviousItemsExDataModel_AutoGen instance;
        static PreviousItemsExDataModel_AutoGen()
        {
            instance = new PreviousItemsExDataModel_AutoGen();
        }

        public static  PreviousItemsExDataModel_AutoGen Instance
        {
            get { return instance; }
        }

       //Search Entities 
        public async Task<IEnumerable<PreviousItemsEx>> SearchPreviousItemsEx(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new PreviousItemsExService())
            {
                return await ctx.GetPreviousItemsExByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

    }
}
		