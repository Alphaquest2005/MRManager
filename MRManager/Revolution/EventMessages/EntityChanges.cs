using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class EntityChanges<T> : ProcessSystemMessage where T : IEntityId
    {
        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }
        
        public EntityChanges(int entityId, Dictionary<string,dynamic> changes, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Contract.Requires(changes.Count > 0);
            EntityId = entityId;
            Changes = changes;
           
        }
    }
}
