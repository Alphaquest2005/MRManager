﻿using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{


    public class EntityCreated<T> : ProcessSystemMessage, IEntityCreated<T> where T : IEntity
    {
        public T Entity { get; }
        
        public EntityCreated(T entity, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
