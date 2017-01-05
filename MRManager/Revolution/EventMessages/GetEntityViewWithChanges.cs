using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    [Export]
    public class GetEntityViewWithChanges<TView> : ProcessSystemMessage, IGetEntityViewWithChanges<TView>
    {
        public GetEntityViewWithChanges(int entityId, Dictionary<string, dynamic> changes, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Changes = changes;
            EntityId = entityId;
        }

        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }
    }
}