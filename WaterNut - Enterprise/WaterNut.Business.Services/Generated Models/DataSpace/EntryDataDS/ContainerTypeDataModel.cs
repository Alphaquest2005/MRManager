﻿// <autogenerated>
//   This file was generated by T4 code generator AllDataSpaceViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.Generic;
using System.Threading.Tasks;
//using SimpleMvvmToolkit;
using EntryDataDS.Business.Entities;
using EntryDataDS.Business.Services;




namespace WaterNut.DataSpace.EntryDataDS.DataModels
{
	public partial class ContainerTypeDataModel_AutoGen 
	{
        private static readonly ContainerTypeDataModel_AutoGen instance;
        static ContainerTypeDataModel_AutoGen()
        {
            instance = new ContainerTypeDataModel_AutoGen();
        }

        public static  ContainerTypeDataModel_AutoGen Instance
        {
            get { return instance; }
        }

       //Search Entities 
        public async Task<IEnumerable<ContainerType>> SearchContainerType(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new ContainerTypeService())
            {
                return await ctx.GetContainerTypesByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

    }
}
		