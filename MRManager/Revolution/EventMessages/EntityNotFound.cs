using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityNotFound<T> : ProcessSystemMessage where T : IEntity
    {
        public EntityNotFound(int entityId, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            EntityId = entityId;
        }

        public int EntityId { get; }

    }
}
