using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{

    public class CurrentEntityChanged<T> : ProcessSystemMessage, IEvent where T:IEntity
    {
        public int EntityId { get; }
        
        public CurrentEntityChanged(int entityId, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            EntityId = entityId;
        }
    }

 
}
