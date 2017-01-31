using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IUpdateEntityWithChanges<out TEntity> : IProcessSystemMessage, IEntityRequest<TEntity> where TEntity : IEntity
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
    }
}