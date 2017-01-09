using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class EntityNotFound<T> : ProcessSystemMessage where T : IEntity
    {
        public EntityNotFound(int entityId, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            EntityId = entityId;
        }

        public int EntityId { get; }

    }
}
