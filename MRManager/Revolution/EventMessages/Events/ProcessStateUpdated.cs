using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IProcessStateUpddated))]
    public class ProcessStateUpdated : ProcessSystemMessage, IProcessStateUpddated
    {
        public ProcessStateUpdated() { }
        public Type EntityType { get;}
        public IProcessStateMessage<IEntityId> StateMessage { get; }

        public ProcessStateUpdated(Type entityType, IProcessStateMessage<IEntityId> stateMessage, IStateEventInfo stateEventInfo, ISystemProcess process, ISystemSource source):base(stateEventInfo, process, source)
        {
            this.EntityType = entityType;
            StateMessage = stateMessage;
            
        }
    }
}
