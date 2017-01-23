using System;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    public class ProcessStateUpdated : ProcessSystemMessage, IProcessStateUpddated
    {
        public Type EntityType { get;}
        public IProcessStateMessage<IEntityId> StateMessage { get; }

        public ProcessStateUpdated(Type entityType, IProcessStateMessage<IEntityId> stateMessage, IStateEventInfo stateEventInfo, ISystemProcess process, ISystemSource source):base(stateEventInfo, process, source)
        {
            this.EntityType = entityType;
            StateMessage = stateMessage;
            
        }
    }
}
