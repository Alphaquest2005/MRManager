using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IGetEntityViewWithChanges<>))]

    public class GetEntityViewWithChanges<TView> : ProcessSystemMessage, IGetEntityViewWithChanges<TView> where TView : IEntityView
    {
        public GetEntityViewWithChanges() { }
        public GetEntityViewWithChanges( Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Changes = changes;
            
        }

        public Dictionary<string, dynamic> Changes { get; }
        public Type ViewType => typeof(TView);
    }
}