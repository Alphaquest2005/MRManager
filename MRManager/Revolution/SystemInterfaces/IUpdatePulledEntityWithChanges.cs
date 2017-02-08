﻿using System.Collections.Generic;

namespace SystemInterfaces
{
    public interface IUpdatePulledEntityWithChanges<out TEntity> : IProcessSystemMessage, IEntityRequest<TEntity> where TEntity:IEntity
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
        string EntityName { get; }
    }
}