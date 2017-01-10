using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{

    public class CurrentEntityChanged<T> : ProcessSystemMessage, IEvent where T:IEntity
    {
        public int EntityId { get; }
        
        public CurrentEntityChanged(int entityId, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            EntityId = entityId;
        }
    }

 
}
