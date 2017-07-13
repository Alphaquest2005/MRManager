﻿// <autogenerated>
//   This file was generated by T4 code generator AllServices.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Core.Common.Contracts;
using CoreEntities.Client.DTO;

namespace CoreEntities.Client.Contracts
{
    [ServiceContract (Namespace="http://www.insight-software.com/WaterNut")]
    public partial interface IEntryPreviousItemsService : IClientService
    {
        [OperationContract]
        Task<IEnumerable<EntryPreviousItems>> GetEntryPreviousItems(List<string> includesLst = null);

        [OperationContract]
        Task<EntryPreviousItems> GetEntryPreviousItemsByKey(string id, List<string> includesLst = null);

		[OperationContract]
        Task<IEnumerable<EntryPreviousItems>> GetEntryPreviousItemsByExpression(string exp, List<string> includesLst = null);

		[OperationContract]
        Task<IEnumerable<EntryPreviousItems>> GetEntryPreviousItemsByExpressionLst(List<string> expLst, List<string> includesLst = null);

		[OperationContract]
		Task<IEnumerable<EntryPreviousItems>> GetEntryPreviousItemsByExpressionNav(string exp,
														 Dictionary<string, string> navExp, List<string> includesLst = null);        
        [OperationContract]
        Task<IEnumerable<EntryPreviousItems>> GetEntryPreviousItemsByBatch(string exp,
                                                                        int totalrow, List<string> includesLst = null);
        [OperationContract]
        Task<IEnumerable<EntryPreviousItems>> GetEntryPreviousItemsByBatchExpressionLst(List<string> expLst,
                                                                        int totalrow, List<string> includesLst = null);

		[OperationContract]
        Task<EntryPreviousItems> UpdateEntryPreviousItems(DTO.EntryPreviousItems entity);

        [OperationContract]
        Task<EntryPreviousItems> CreateEntryPreviousItems(DTO.EntryPreviousItems entity);

        [OperationContract]
        Task<bool> DeleteEntryPreviousItems(string id);

		// Virtural List Implementation

        [OperationContract]
        Task<int> CountByExpressionLst(List<string> expLst);
    
		[OperationContract]
        Task<int> Count(string exp);

		[OperationContract]
        Task<int> CountNav(string exp, Dictionary<string, string> navExp);

        [OperationContract]
        Task<IEnumerable<EntryPreviousItems>> LoadRange(int startIndex, int count, string exp);

		[OperationContract]
		Task<IEnumerable<EntryPreviousItems>> LoadRangeNav(int startIndex, int count, string exp,
                                                                                 Dictionary<string, string> navExp, IEnumerable<string> includeLst = null);

		[OperationContract]
		decimal SumField(string whereExp, string field);
        
        [OperationContract]
        Task<decimal> SumNav( string exp, Dictionary<string, string> navExp, string field);

		[OperationContract]
		string MinField(string whereExp, string field);

				[OperationContract]
		Task<IEnumerable<EntryPreviousItems>> GetEntryPreviousItemsByPreviousItem_Id(string PreviousItem_Id, List<string> includesLst = null);
        
  		[OperationContract]
		Task<IEnumerable<EntryPreviousItems>> GetEntryPreviousItemsByItem_Id(string Item_Id, List<string> includesLst = null);
        
  		
    }
}
