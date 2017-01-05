using System.ComponentModel.Composition;
using System.Runtime.InteropServices.ComTypes;

namespace SystemInterfaces
{
    
    [InheritedExport]
    public interface IGetEntityById<TEntity>:IProcessSystemMessage where TEntity:IEntity
    {
       // void Create(int entityId);
        int EntityId { get; }
    }
}