﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    [Export]
    public class EntityWithChangesFound<TEntity> : ProcessSystemMessage, IEntityWithChangesFound<TEntity> where TEntity : IEntity
    {
        public EntityWithChangesFound(TEntity entity, Dictionary<string, dynamic> changes, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Entity = entity;
            Changes = changes;
        }

        public TEntity Entity { get; set; }
        public Dictionary<string, dynamic> Changes { get; }

    }
}