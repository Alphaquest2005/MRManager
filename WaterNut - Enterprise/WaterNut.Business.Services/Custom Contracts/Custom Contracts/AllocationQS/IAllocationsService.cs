using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Core.Common.Business.Services;
using Core.Common.Contracts;

namespace AllocationQS.Business.Services
{
     [ServiceContract(Namespace = "http://www.insight-software.com/WaterNut")]
    public partial interface IAllocationsService : IBusinessService
    {

        [OperationContract][FaultContract(typeof(ValidationFault))]
        Task CreateEx9(string filterExpression, bool perIM7, bool applyEx9Bucket, bool breakOnMonthYear, int AsycudaDocumentSetId);
        [OperationContract][FaultContract(typeof(ValidationFault))]
        Task CreateOPS(string filterExpression, int AsycudaDocumentSetId);
        [OperationContract][FaultContract(typeof(ValidationFault))]
        Task ManuallyAllocate(int AllocationId, int PreviousItem_Id);
        [OperationContract][FaultContract(typeof(ValidationFault))]
        Task ClearAllocations(IEnumerable<int> alst);
        [OperationContract][FaultContract(typeof(ValidationFault))]
        Task ClearAllocationsByFilter(string filterExpression);
        //[OperationContract][FaultContract(typeof(ValidationFault))]
        //Task CreateIncompOPS(string filterExpression, int AsycudaDocumentSetId);
        [OperationContract][FaultContract(typeof(ValidationFault))]
        Task AllocateSales(bool itemDescriptionContainsAsycudaAttribute);

        [OperationContract][FaultContract(typeof(ValidationFault))]
         Task ReBuildSalesReports();

        //[OperationContract][FaultContract(typeof(ValidationFault))]
        //Task ReBuildSalesReports(int asycuda_id);
    }
}

