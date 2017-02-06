using System.Collections.Generic;

namespace SystemInterfaces
{
    public interface IEntityChanges
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
    }
}