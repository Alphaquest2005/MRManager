using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using System.Diagnostics.Contracts;

namespace EventMessages.Events
{
    [Export(typeof(IEntityViewWithChangesUpdated<>))]
    public class EntityViewWithChangesUpdated<TView> : ProcessSystemMessage, IEntityViewWithChangesUpdated<TView> where TView : IEntityView
    {
        public EntityViewWithChangesUpdated() { }
        public EntityViewWithChangesUpdated(TView entity, Dictionary<string, dynamic> changes, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
            Changes = changes;
        }

        public TView Entity { get; set; }
        public Dictionary<string, dynamic> Changes { get; }

    }
}