using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using Interfaces;

namespace EventMessages.Commands
{
    [Export(typeof(IUpdatePulledEntityWithChanges<IPatients>))]
    public class UpdatePulledEntityWithChanges : ProcessSystemMessage, IUpdatePulledEntityWithChanges<IPatients>
    {
        public UpdatePulledEntityWithChanges(){}
       public UpdatePulledEntityWithChanges(int entityId, string entityName, Dictionary<string, object> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source): base(processInfo, process, source)
        {
            Changes = changes;
            EntityId = entityId;
            EntityName = entityName;
        }

        public Dictionary<string, object> Changes { get; }
        public int EntityId { get; }
        public string EntityName { get; }
    }
}