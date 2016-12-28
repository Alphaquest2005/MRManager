using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{

    public class CurrentEntityChanged<T> : ProcessSystemMessage, IEvent where T:IEntity
    {
        public int EntityId { get; }
        
        public CurrentEntityChanged(int entityId, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            EntityId = entityId;
        }
    }

 
}
