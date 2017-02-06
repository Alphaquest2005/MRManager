using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IGetEntityViewById<out TEntityView> : IProcessSystemMessage, IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        // void Create(int entityId);
        int EntityId { get; }
    }
}