﻿// <autogenerated>
//   This file was generated by T4 code generator AllDataSpaceViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.Generic;
using System.Threading.Tasks;
//using SimpleMvvmToolkit;
using DocumentItemDS.Business.Entities;
using DocumentItemDS.Business.Services;




namespace WaterNut.DataSpace.DocumentItemDS.DataModels
{
	public partial class xcuda_Previous_docDataModel_AutoGen 
	{
        private static readonly xcuda_Previous_docDataModel_AutoGen instance;
        static xcuda_Previous_docDataModel_AutoGen()
        {
            instance = new xcuda_Previous_docDataModel_AutoGen();
        }

        public static  xcuda_Previous_docDataModel_AutoGen Instance
        {
            get { return instance; }
        }

       //Search Entities 
        public async Task<IEnumerable<xcuda_Previous_doc>> Searchxcuda_Previous_doc(List<string> lst, List<string> includeLst = null )
        {
            using (var ctx = new xcuda_Previous_docService())
            {
                return await ctx.Getxcuda_Previous_docByExpressionLst(lst, includeLst).ConfigureAwait(false);
            }
        }

    }
}
		