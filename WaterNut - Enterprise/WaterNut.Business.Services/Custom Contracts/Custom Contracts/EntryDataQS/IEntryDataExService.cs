﻿using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Core.Common.Business.Services;

namespace EntryDataQS.Business.Services
{
   
    public partial interface IEntryDataExService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        Task AddDocToEntry(IEnumerable<string> lst, int docSetId, bool perInvoice);

        [OperationContract]
        [FaultContract(typeof (ValidationFault))]
        Task RemoveEntryData(string po);

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        Task RemoveSelectedEntryData(IEnumerable<string> selectedEntryDataEx);
       
        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        Task SaveCSV(string droppedFilePath, string fileType, int docSetId,
            bool overWriteExisting);

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        Task SavePDF(string droppedFilePath, string fileType, int docSetId, bool overwrite);

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        Task SaveTXT(string droppedFilePath, string fileType, int docSetId, bool overwrite);
    }
}

