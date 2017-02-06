using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IEntityDeleted<>))]
    public class EntityDeleted<TEntity> : ProcessSystemMessage, IEntityDeleted<TEntity> where TEntity : IEntity
    {
        public EntityDeleted() { } 
        public int EntityId { get; }
        
        public EntityDeleted(int entityId, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(entityId != 0);
            EntityId = entityId;
        }

    }
}
