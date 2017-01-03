using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IGetEntityWithChanges<TEntity> : IProcessSystemMessage where TEntity:IEntity
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
    }
}
