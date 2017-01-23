using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    
    public class EntityNotFound<T> : ProcessSystemMessage where T : IEntity
    {
        public EntityNotFound(int entityId, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            EntityId = entityId;
        }

        public int EntityId { get; }

    }
}
