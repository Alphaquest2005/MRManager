using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityNotFound<T> : SystemProcessMessage where T : IEntity
    {
        public EntityNotFound(int entityId, ISystemProcess process, MessageSource source) : base(process, source)
        {
            EntityId = entityId;
        }

        public int EntityId { get; }

    }
}
