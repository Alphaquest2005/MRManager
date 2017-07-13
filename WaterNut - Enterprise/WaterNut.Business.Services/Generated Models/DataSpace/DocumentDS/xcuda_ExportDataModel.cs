﻿// <autogenerated>
//   This file was generated by T4 code generator AllDataSpaceViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.Generic;
using System.Threading.Tasks;
//using SimpleMvvmToolkit;
using DocumentDS.Business.Entities;
using DocumentDS.Business.Services;




namespace WaterNut.DataSpace.DocumentDS.DataModels
{
	public partial class xcuda_ExportDataModel_AutoGen 
	{
        private static readonly xcuda_ExportDataModel_AutoGen instance;
        static xcuda_ExportDataModel_AutoGen()
        {
            instance = new xcuda_ExportDataModel_AutoGen();
        }

        public static  xcuda_ExportDataModel_AutoGen Instance
        {
            get { return instance; }
        }

       //Search Entities 
        public async Task<IEnumerable<xcuda_Export>> Searchxcuda_Export(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new xcuda_ExportService())
            {
                return await ctx.Getxcuda_ExportByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

    }
}
		