using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityDeleted<T> : SystemProcessMessage where T : IEntity
    {
        public int EntityId { get; }
        
        public EntityDeleted(int entityId, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Contract.Requires(entityId != 0);
            EntityId = entityId;
        }

    }
}
