using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    [Export]
    public class DeleteEntity<T> : BaseMessage where T : IEntity
    {
       
        public DeleteEntity(int entityId, MessageSource source) : base(source)
        {
            Contract.Requires(entityId > 0);
            EntityId = entityId;
        }
        public int EntityId { get; private set; }

    }
}
