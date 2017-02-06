using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(ICreateEntityService))]
    public class CreateEntityService : ProcessSystemMessage, ICreateEntityService
    {
        public CreateEntityService() { }
        public Type ActorType { get; }
        public object Action { get; }

        public CreateEntityService(Type actorType, object action, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            ActorType = actorType;
            Action = action;
            ProcessInfo = processInfo;
        }

        public new IStateCommandInfo ProcessInfo { get; }
    }
}