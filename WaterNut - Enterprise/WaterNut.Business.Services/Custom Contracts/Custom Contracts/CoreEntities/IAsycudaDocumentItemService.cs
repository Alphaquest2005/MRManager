﻿// <autogenerated>
//   This file was generated by T4 code generator AllServices.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Core.Common.Business.Services;
using CoreEntities.Business.Entities;

namespace CoreEntities.Business.Services
{
    
    public partial interface IAsycudaDocumentItemService 
    {
        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        
        Task RemoveSelectedItems(IEnumerable<int> lst);

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]

        Task SaveAsycudaDocumentItem(AsycudaDocumentItem asycudaDocumentItem);
    }
}

