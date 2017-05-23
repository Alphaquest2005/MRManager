using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace DomainMessages
{
    [Export(typeof(IGetEntityFromPatientResponse<>))]

    public class GetEntityFromPatientResponse<TView> : ProcessSystemMessage, IGetEntityFromPatientResponse<TView> where TView : IEntityView
    {
        public GetEntityFromPatientResponse(){}

        public int PatientId { get; }
        public Type ViewType  => typeof(TView);
        public string EntityName { get; }

        public GetEntityFromPatientResponse(int patientId,string entityName, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            PatientId = patientId;
            EntityName = entityName;
            
        }

        
        
    }
}