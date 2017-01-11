using System;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages
{
    public class CreateEntityService : ProcessSystemMessage, ICreateEntityService
    {
        public Type ActorType { get; }
        public object Action { get; }

        public CreateEntityService(Type actorType, object action, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            ActorType = actorType;
            Action = action;
        }
    }
}