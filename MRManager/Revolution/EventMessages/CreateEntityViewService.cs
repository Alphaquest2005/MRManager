using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages
{
    public class CreateEntityViewService : ProcessSystemMessage, ICreateEntityViewService
    {
        public Type ActorType { get; }
        public object Action { get; }

        public CreateEntityViewService(Type actorType, object action, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            ActorType = actorType;
            Action = action;
        }
    }
}
