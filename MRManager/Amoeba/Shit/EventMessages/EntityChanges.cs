using System.Collections.Generic;
using System.Diagnostics.Contracts;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityChanges<T> : BaseMessage where T : IEntity
    {
        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }
        
        public EntityChanges(int entityId, Dictionary<string,dynamic> changes, MessageSource source) : base(source)
        {
            Contract.Requires(changes.Count > 0);
            EntityId = entityId;
            Changes = changes;
           
        }
    }
}
