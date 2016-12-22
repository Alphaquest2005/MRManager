using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{

    public class CurrentEntityChanged<T> : SystemProcessMessage, IEvent where T:IEntity
    {
        public int EntityId { get; }
        
        public CurrentEntityChanged(int entityId, ISystemProcess process, MessageSource source) : base(process,source)
        {
            EntityId = entityId;
        }
    }

 
}
