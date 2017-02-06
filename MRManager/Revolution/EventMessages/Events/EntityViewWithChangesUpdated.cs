using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IEntityViewWithChangesUpdated<>))]
    public class EntityViewWithChangesUpdated<TView> : ProcessSystemMessage, IEntityViewWithChangesUpdated<TView> where TView : IEntityView
    {
        public EntityViewWithChangesUpdated() { }
        public EntityViewWithChangesUpdated(TView entity, Dictionary<string, dynamic> changes, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Entity = entity;
            Changes = changes;
        }

        public TView Entity { get; set; }
        public Dictionary<string, dynamic> Changes { get; }

    }
}