using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityNotFound<T> : BaseMessage where T : IEntity
    {
        public EntityNotFound(int entityId,MessageSource source) :base(source)
        {
            EntityId = entityId;
        }

        public int EntityId { get; }

    }
}
