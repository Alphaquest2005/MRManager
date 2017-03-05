using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IUpdateEntityViewWithChanges<>))]
    
    public class UpdateEntityViewWithChanges<TView> : ProcessSystemMessage, IUpdateEntityViewWithChanges<TView> where TView : IEntityView
    {

        public UpdateEntityViewWithChanges() { }
        public UpdateEntityViewWithChanges(int entityId, Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Changes = changes;
            EntityId = entityId;
        }

        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }
        public Type ViewType => typeof (TView);
    }

    [Export(typeof(IAddEntityViewWithChanges<>))]

    public class AddEntityViewWithChanges<TView> : ProcessSystemMessage, IAddEntityViewWithChanges<TView> where TView : IEntityView
    {

        public AddEntityViewWithChanges() { }
        public AddEntityViewWithChanges(Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Changes = changes;
        }

        public Dictionary<string, dynamic> Changes { get; }
        public Type ViewType => typeof(TView);

    }
}