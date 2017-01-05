using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{

    public class EntityDeleted<T> : ProcessSystemMessage, IEntityDeleted where T : IEntity
    {
        public int EntityId { get; }
        
        public EntityDeleted(int entityId, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Contract.Requires(entityId != 0);
            EntityId = entityId;
        }

    }
}
