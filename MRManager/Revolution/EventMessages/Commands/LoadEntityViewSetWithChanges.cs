using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export]
    public class LoadEntityViewSetWithChanges<TView, TMatchType> : ProcessSystemMessage, ILoadEntityViewSetWithChanges<TView, TMatchType> where TView : IEntityView where TMatchType:IMatchType
    {
        public LoadEntityViewSetWithChanges( Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Changes = changes;
        }

        public Dictionary<string, dynamic> Changes { get; }
    }
}