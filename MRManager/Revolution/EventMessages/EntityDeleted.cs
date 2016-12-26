using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityDeleted<T> : ProcessSystemMessage where T : IEntity
    {
        public int EntityId { get; }
        
        public EntityDeleted(int entityId, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            Contract.Requires(entityId != 0);
            EntityId = entityId;
        }

    }
}
