﻿// <autogenerated>
//   This file was generated by T4 code generator AllServices.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using EntryDataQS.Client.DTO;
using EntryDataQS.Client.Contracts;
using Core.Common.Client.Services;

using Core.Common.Contracts;
using System.ComponentModel.Composition;


namespace EntryDataQS.Client.Services
{
    [Export (typeof(InventoryItemsExClient))]
    [Export (typeof(IInventoryItemsExService))]
    [Export(typeof(IClientService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class InventoryItemsExClient :  ClientService<IInventoryItemsExService>, IInventoryItemsExService, IDisposable
    {
        
        public async Task<IEnumerable<DTO.InventoryItemsEx>> GetInventoryItemsEx(List<string> includesLst = null)
        {
            return await Channel.GetInventoryItemsEx(includesLst).ConfigureAwait(false);
        }

        public async Task<DTO.InventoryItemsEx> GetInventoryItemsExByKey(string id, List<string> includesLst = null)
        {
            return await Channel.GetInventoryItemsExByKey(id, includesLst).ConfigureAwait(false);
        }

		public async Task<IEnumerable<DTO.InventoryItemsEx>> GetInventoryItemsExByExpression(string exp, List<string> includesLst = null)
        {
            return await Channel.GetInventoryItemsExByExpression(exp, includesLst).ConfigureAwait(false);
        }

		public async Task<IEnumerable<DTO.InventoryItemsEx>> GetInventoryItemsExByExpressionLst(List<string> expLst, List<string> includesLst = null)
        {
            return await Channel.GetInventoryItemsExByExpressionLst(expLst, includesLst).ConfigureAwait(false);
        }

		public async Task<IEnumerable<DTO.InventoryItemsEx>> GetInventoryItemsExByExpressionNav(string exp,
															 Dictionary<string, string> navExp, List<string> includesLst = null)
		{
			return await Channel.GetInventoryItemsExByExpressionNav(exp, navExp, includesLst).ConfigureAwait(false);
		}

        public async Task<IEnumerable<InventoryItemsEx>> GetInventoryItemsExByBatch(string exp,
                                                                        int totalrow, List<string> includesLst = null)
        {
            return await Channel.GetInventoryItemsExByBatch(exp, totalrow, includesLst).ConfigureAwait(false);
        }

        public async Task<IEnumerable<InventoryItemsEx>> GetInventoryItemsExByBatchExpressionLst(List<string> expLst,
                                                                        int totalrow, List<string> includesLst = null)
        {
            return await Channel.GetInventoryItemsExByBatchExpressionLst(expLst, totalrow, includesLst).ConfigureAwait(false);
        }

        public async Task<DTO.InventoryItemsEx> UpdateInventoryItemsEx(DTO.InventoryItemsEx entity)
        {
           return await Channel.UpdateInventoryItemsEx(entity).ConfigureAwait(false);
        }

        public async Task<DTO.InventoryItemsEx> CreateInventoryItemsEx(DTO.InventoryItemsEx entity)
        {
           return await Channel.CreateInventoryItemsEx(entity).ConfigureAwait(false);
        }

        public async Task<bool> DeleteInventoryItemsEx(string id)
        {
            return await Channel.DeleteInventoryItemsEx(id).ConfigureAwait(false);
        }

       // Virtural List implementation

        public async Task<int> CountByExpressionLst(List<string> expLst)
        {
            return await Channel.CountByExpressionLst(expLst).ConfigureAwait(continueOnCapturedContext: false);
        }
        
	    public async Task<int> Count(string exp)
        {
            return await Channel.Count(exp).ConfigureAwait(continueOnCapturedContext: false);
        }

		public async Task<int> CountNav(string exp, Dictionary<string, string> navExp)
        {
           return await Channel.CountNav(exp, navExp).ConfigureAwait(false);
        }

        public async Task<IEnumerable<DTO.InventoryItemsEx>> LoadRange(int startIndex, int count, string exp)
        {
            return await Channel.LoadRange(startIndex,count,exp).ConfigureAwait(false);
        }

		public async Task<IEnumerable<DTO.InventoryItemsEx>>  LoadRangeNav(int startIndex, int count, string exp,
                                                                                 Dictionary<string, string> navExp, IEnumerable<string> includeLst = null)
        {
            return await Channel.LoadRangeNav(startIndex,count,exp, navExp, includeLst).ConfigureAwait(false);
        }
        public decimal SumField(string whereExp, string sumExp)
		{
			return Channel.SumField(whereExp,sumExp);
		}

        public async Task<decimal> SumNav( string exp, Dictionary<string, string> navExp, string field)
        {
            return await Channel.SumNav(exp,navExp,field).ConfigureAwait(false);
        }

		public string MinField(string whereExp, string sumExp)
		{
			return Channel.MinField(whereExp,sumExp);
		}

		#region IDisposable implementation

            /// <summary>
            /// IDisposable.Dispose implementation, calls Dispose(true).
            /// </summary>
            void IDisposable.Dispose()
            {
                Dispose(true);
            }

            /// <summary>
            /// Dispose worker method. Handles graceful shutdown of the
            /// client even if it is an faulted state.
            /// </summary>
            /// <param name="disposing">Are we disposing (alternative
            /// is to be finalizing)</param>
            protected new void Dispose(bool disposing)
            {
                if (disposing)
                {
                    try
                    {
                        if (State != CommunicationState.Faulted)
                        {
                            Close();
                        }
                    }
                    finally
                    {
                        if (State != CommunicationState.Closed)
                        {
                            Abort();
                        }
                        GC.SuppressFinalize(this);
                    }
                }
            }



            #endregion
    }
}

