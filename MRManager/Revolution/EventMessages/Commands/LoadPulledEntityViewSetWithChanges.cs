using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(ILoadPulledEntityViewSetWithChanges<,>))]
    public class LoadPulledEntityViewSetWithChanges<TView, TMatchType> : ProcessSystemMessage, ILoadPulledEntityViewSetWithChanges<TView, TMatchType> where TView : IEntityView where TMatchType : IMatchType
    {
        public LoadPulledEntityViewSetWithChanges(){}

        public LoadPulledEntityViewSetWithChanges(string entityName, Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Changes = changes;
            EntityName = entityName;
        }

        public Dictionary<string, dynamic> Changes { get; }
        public string EntityName { get; }

        public Type ViewType => typeof(TView);
    }

    [Export(typeof(ILoadPulledEntityViewSetWithChanges<>))]
    public class LoadPulledEntityViewSetWithChanges<TMatchType> : ProcessSystemMessage, ILoadPulledEntityViewSetWithChanges<TMatchType> where TMatchType : IMatchType
    {
        public LoadPulledEntityViewSetWithChanges(){}

        public LoadPulledEntityViewSetWithChanges(Type viewType, string entityName, Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Changes = changes;
            ViewType = viewType;
            EntityName = entityName;
        }

        public Dictionary<string, dynamic> Changes { get; }
        public string EntityName { get; }
        public Type ViewType { get; }
    }
}