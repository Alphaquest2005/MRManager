using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    [Export]
    public class DeleteEntity<T> : ProcessSystemMessage where T : IEntity
    {
       
        public DeleteEntity(int entityId, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            Contract.Requires(entityId > 0);
            EntityId = entityId;
        }
        public int EntityId { get; private set; }

    }
}
