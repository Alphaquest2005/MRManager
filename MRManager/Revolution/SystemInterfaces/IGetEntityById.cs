using System.ComponentModel.Composition;
using System.Runtime.InteropServices.ComTypes;

namespace SystemInterfaces
{
    
    
    public interface IGetEntityById<out TEntity>:IProcessSystemMessage, IEntityRequest<TEntity> where TEntity:IEntity
    {
       // void Create(int entityId);
        int EntityId { get; }
    }
}