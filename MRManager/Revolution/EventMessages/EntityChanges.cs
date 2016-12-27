﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityChanges<T> : ProcessSystemMessage where T : IEntity
    {
        public Dictionary<string, dynamic> Changes { get; }
        public int EntityId { get; }
        
        public EntityChanges(int entityId, Dictionary<string,dynamic> changes, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            Contract.Requires(changes.Count > 0);
            EntityId = entityId;
            Changes = changes;
           
        }
    }
}
