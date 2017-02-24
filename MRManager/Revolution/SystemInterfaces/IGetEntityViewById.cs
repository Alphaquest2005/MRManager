using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IGetEntityViewById<out TEntityView> : IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        // void Create(int entityId);
        int EntityId { get; }
    }

    public interface IGetEntityFromPatientResponse<out TEntityView> : IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        string EntityName { get; }
        int PatientId { get; }
    }
}