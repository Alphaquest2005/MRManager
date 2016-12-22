using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    [Export]
    public class DeleteEntity<T> : SystemProcessMessage where T : IEntity
    {
       
        public DeleteEntity(int entityId, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Contract.Requires(entityId > 0);
            EntityId = entityId;
        }
        public int EntityId { get; private set; }

    }
}
