using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    public class UpdateEntity<TEntity> : ProcessSystemMessage, IUpdateEntity<TEntity> where TEntity : IEntity
    {
        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }

        public UpdateEntity(int entityId, Dictionary<string, dynamic> changes, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Contract.Requires(changes.Count > 0);
            EntityId = entityId;
            Changes = changes;

        }
    }
}