using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{


    [Export(typeof(IEntityChanges))]
    public class EntityChanges<T> : ProcessSystemMessage, IEntityChanges where T : IEntityId
    {
        public EntityChanges() { } 
        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }
        
        public EntityChanges(int entityId, Dictionary<string,dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(changes.Count > 0);
            EntityId = entityId;
            Changes = changes;
           
        }
        public EntityChanges(int entityId, Dictionary<string, dynamic> changes, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Contract.Requires(changes.Count > 0);
            EntityId = entityId;
            Changes = changes;

        }
    }
}
