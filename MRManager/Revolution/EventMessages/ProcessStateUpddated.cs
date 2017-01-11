using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
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
