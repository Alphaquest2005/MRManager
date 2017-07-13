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
    public partial interface IAsycudaDocumentItemService : IClientService
    {
        [OperationContract]
        Task<IEnumerable<AsycudaDocumentItem>> GetAsycudaDocumentItems(List<string> includesLst = null);

        [OperationContract]
        Task<AsycudaDocumentItem> GetAsycudaDocumentItemByKey(string id, List<string> includesLst = null);

		[OperationContract]
        Task<IEnumerable<AsycudaDocumentItem>> GetAsycudaDocumentItemsByExpression(string exp, List<string> includesLst = null);

		[OperationContract]
        Task<IEnumerable<AsycudaDocumentItem>> GetAsycudaDocumentItemsByExpressionLst(List<string> expLst, List<string> includesLst = null);

		[OperationContract]
		Task<IEnumerable<AsycudaDocumentItem>> GetAsycudaDocumentItemsByExpressionNav(string exp,
														 Dictionary<string, string> navExp, List<string> includesLst = null);        
        [OperationContract]
        Task<IEnumerable<AsycudaDocumentItem>> GetAsycudaDocumentItemsByBatch(string exp,
                                                                        int totalrow, List<string> includesLst = null);
        [OperationContract]
        Task<IEnumerable<AsycudaDocumentItem>> GetAsycudaDocumentItemsByBatchExpressionLst(List<string> expLst,
                                                                        int totalrow, List<string> includesLst = null);

		[OperationContract]
        Task<AsycudaDocumentItem> UpdateAsycudaDocumentItem(DTO.AsycudaDocumentItem entity);

        [OperationContract]
        Task<AsycudaDocumentItem> CreateAsycudaDocumentItem(DTO.AsycudaDocumentItem entity);

        [OperationContract]
        Task<bool> DeleteAsycudaDocumentItem(string id);

		// Virtural List Implementation

        [OperationContract]
        Task<int> CountByExpressionLst(List<string> expLst);
    
		[OperationContract]
        Task<int> Count(string exp);

		[OperationContract]
        Task<int> CountNav(string exp, Dictionary<string, string> navExp);

        [OperationContract]
        Task<IEnumerable<AsycudaDocumentItem>> LoadRange(int startIndex, int count, string exp);

		[OperationContract]
		Task<IEnumerable<AsycudaDocumentItem>> LoadRangeNav(int startIndex, int count, string exp,
                                                                                 Dictionary<string, string> navExp, IEnumerable<string> includeLst = null);

		[OperationContract]
		decimal SumField(string whereExp, string field);
        
        [OperationContract]
        Task<decimal> SumNav( string exp, Dictionary<string, string> navExp, string field);

		[OperationContract]
		string MinField(string whereExp, string field);

				[OperationContract]
		Task<IEnumerable<AsycudaDocumentItem>> GetAsycudaDocumentItemByAsycudaDocumentId(string AsycudaDocumentId, List<string> includesLst = null);
        
  		[OperationContract]
		Task<IEnumerable<AsycudaDocumentItem>> GetAsycudaDocumentItemByEntryDataDetailsId(string EntryDataDetailsId, List<string> includesLst = null);
        
  		
    }
}
