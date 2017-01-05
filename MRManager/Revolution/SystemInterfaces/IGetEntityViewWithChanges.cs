using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IGetEntityViewWithChanges<TEntityView> : IProcessSystemMessage
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
    }
}