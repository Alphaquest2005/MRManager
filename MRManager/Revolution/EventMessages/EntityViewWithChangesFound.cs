using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    [Export]
    public class EntityViewWithChangesFound<TView> : ProcessSystemMessage, IEntityViewWithChangesFound<TView> where TView:IEntityView
    {
        public EntityViewWithChangesFound(TView entity, Dictionary<string, dynamic> changes, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Entity = entity;
            Changes = changes;
        }

        public TView Entity { get; set; }
        public Dictionary<string, dynamic> Changes { get; }

    }
}