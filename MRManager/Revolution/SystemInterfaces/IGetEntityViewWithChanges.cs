using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IGetEntityViewWithChanges<out TEntityView> : IProcessSystemMessage, IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
    }
}