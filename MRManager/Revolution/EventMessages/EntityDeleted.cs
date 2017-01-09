using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{

    public class EntityDeleted<TEntity> : ProcessSystemMessage, IEntityDeleted<TEntity> where TEntity : IEntity
    {
        public int EntityId { get; }
        
        public EntityDeleted(int entityId, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Contract.Requires(entityId != 0);
            EntityId = entityId;
        }

    }
}
