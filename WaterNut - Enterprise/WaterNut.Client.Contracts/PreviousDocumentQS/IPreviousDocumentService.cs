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
using PreviousDocumentQS.Client.DTO;

namespace PreviousDocumentQS.Client.Contracts
{
    [ServiceContract (Namespace="http://www.insight-software.com/WaterNut")]
    public partial interface IPreviousDocumentService : IClientService
    {
        [OperationContract]
        Task<IEnumerable<PreviousDocument>> GetPreviousDocuments(List<string> includesLst = null);

        [OperationContract]
        Task<PreviousDocument> GetPreviousDocumentByKey(string id, List<string> includesLst = null);

		[OperationContract]
        Task<IEnumerable<PreviousDocument>> GetPreviousDocumentsByExpression(string exp, List<string> includesLst = null);

		[OperationContract]
        Task<IEnumerable<PreviousDocument>> GetPreviousDocumentsByExpressionLst(List<string> expLst, List<string> includesLst = null);

		[OperationContract]
		Task<IEnumerable<PreviousDocument>> GetPreviousDocumentsByExpressionNav(string exp,
														 Dictionary<string, string> navExp, List<string> includesLst = null);        
        [OperationContract]
        Task<IEnumerable<PreviousDocument>> GetPreviousDocumentsByBatch(string exp,
                                                                        int totalrow, List<string> includesLst = null);
        [OperationContract]
        Task<IEnumerable<PreviousDocument>> GetPreviousDocumentsByBatchExpressionLst(List<string> expLst,
                                                                        int totalrow, List<string> includesLst = null);

		[OperationContract]
        Task<PreviousDocument> UpdatePreviousDocument(DTO.PreviousDocument entity);

        [OperationContract]
        Task<PreviousDocument> CreatePreviousDocument(DTO.PreviousDocument entity);

        [OperationContract]
        Task<bool> DeletePreviousDocument(string id);

		// Virtural List Implementation

        [OperationContract]
        Task<int> CountByExpressionLst(List<string> expLst);
    
		[OperationContract]
        Task<int> Count(string exp);

		[OperationContract]
        Task<int> CountNav(string exp, Dictionary<string, string> navExp);

        [OperationContract]
        Task<IEnumerable<PreviousDocument>> LoadRange(int startIndex, int count, string exp);

		[OperationContract]
		Task<IEnumerable<PreviousDocument>> LoadRangeNav(int startIndex, int count, string exp,
                                                                                 Dictionary<string, string> navExp, IEnumerable<string> includeLst = null);

		[OperationContract]
		decimal SumField(string whereExp, string field);
        
        [OperationContract]
        Task<decimal> SumNav( string exp, Dictionary<string, string> navExp, string field);

		[OperationContract]
		string MinField(string whereExp, string field);

				[OperationContract]
		Task<IEnumerable<PreviousDocument>> GetPreviousDocumentByAsycudaDocumentSetId(string AsycudaDocumentSetId, List<string> includesLst = null);
        
  		
    }
}

