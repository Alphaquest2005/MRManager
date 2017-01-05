using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IGetEntityViewById<TEntityView> : IProcessSystemMessage 
    {
        // void Create(int entityId);
        int EntityId { get; }
    }
}