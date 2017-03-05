using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IGetEntityViewById<out TEntityView> : IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        // void Create(int entityId);
        int EntityId { get; }
    }
}