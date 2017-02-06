using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(ICreateEntityViewService))]
    public class CreateEntityViewService : ProcessSystemMessage, ICreateEntityViewService
    {
        public CreateEntityViewService() { }
        public Type ActorType { get; }
        public object Action { get; }

        public CreateEntityViewService(Type actorType, object action, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            ActorType = actorType;
            Action = action;
        }
    }
}
