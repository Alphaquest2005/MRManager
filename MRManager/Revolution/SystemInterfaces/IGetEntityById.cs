using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    [InheritedExport]
    public interface IGetEntityById<T>:IProcessSystemMessage
    {
       // void Create(int entityId);
        int EntityId { get; }
    }
}