using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IUpdateEntityWithChanges<>))]

    public class UpdateEntityWithChanges<TEntity> : ProcessSystemMessage, IUpdateEntityWithChanges<TEntity> where TEntity : IEntity
    {
        public UpdateEntityWithChanges() { }
        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }

        public UpdateEntityWithChanges(int entityId, Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(changes.Count > 0);
            EntityId = entityId;
            Changes = changes;

        }

        public Type ViewType => typeof(TEntity);
    }
}