using System;
using SystemInterfaces;

namespace DomainMessages
{
    public interface IGetEntityFromPatientResponse<out TEntityView> : IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        string EntityName { get; }
        int PatientId { get; }

        
    }
}