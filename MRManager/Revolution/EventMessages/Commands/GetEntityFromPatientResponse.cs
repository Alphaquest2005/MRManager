using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IGetEntityFromPatientResponse<>))]

    public class GetEntityFromPatientResponse<TView> : ProcessSystemMessage, IGetEntityFromPatientResponse<TView> where TView : IEntityView
    {
        public GetEntityFromPatientResponse() {}

        public int PatientId { get; }
        public string EntityName { get; }

        public GetEntityFromPatientResponse(int patientId,string entityName, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            PatientId = patientId;
            EntityName = entityName;
        }

        
        
    }
}