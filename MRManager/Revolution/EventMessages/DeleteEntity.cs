﻿using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{


    [Export]
    public class DeleteEntity<TEntity> : ProcessSystemMessage, IDeleteEntity<TEntity> where TEntity : IEntity
    {
       
        public DeleteEntity(int entityId, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Contract.Requires(entityId > 0);
            EntityId = entityId;
        }
        public int EntityId { get; }

    }
}
