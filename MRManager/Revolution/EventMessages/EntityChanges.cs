using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityChanges<T> : SystemProcessMessage where T : IEntity
    {
        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }
        
        public EntityChanges(int entityId, Dictionary<string,dynamic> changes, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Contract.Requires(changes.Count > 0);
            EntityId = entityId;
            Changes = changes;
           
        }
    }
}
