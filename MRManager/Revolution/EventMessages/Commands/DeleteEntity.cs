using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{


    [Export(typeof(IDeleteEntity<>))]

    public class DeleteEntity<TEntity> : ProcessSystemMessage, IDeleteEntity<TEntity> where TEntity : IEntity
    {
       public DeleteEntity() { }
        public DeleteEntity(int entityId, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(entityId > 0);
            EntityId = entityId;
        }
        public int EntityId { get; }
        public Type ViewType => typeof(TEntity);

    }
}
