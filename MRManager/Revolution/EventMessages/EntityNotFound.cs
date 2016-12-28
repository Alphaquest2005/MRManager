using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityNotFound<T> : ProcessSystemMessage where T : IEntity
    {
        public EntityNotFound(int entityId, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            EntityId = entityId;
        }

        public int EntityId { get; }

    }
}
