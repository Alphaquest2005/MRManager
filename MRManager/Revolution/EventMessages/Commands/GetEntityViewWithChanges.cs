using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    [Export]
    public class GetEntityViewWithChanges<TView> : ProcessSystemMessage, IGetEntityViewWithChanges<TView> where TView : IEntityView
    {
        public GetEntityViewWithChanges(int entityId, Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Changes = changes;
            EntityId = entityId;
        }

        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }
    }
}