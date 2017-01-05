using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IGetEntityWithChanges<TEntity> : IProcessSystemMessage where TEntity:IEntityId
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
    }
}
