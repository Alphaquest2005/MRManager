﻿using System.Collections.Generic;
using SystemInterfaces;

namespace DomainMessages
{

    public interface IUpdatePatientEntityWithChanges<out TEntity>: IEntityRequest<TEntity> where TEntity:IEntity
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
        string EntityName { get; }
        string SyntomName { get; }
        string InterviewName { get; }
    }

    public interface IUpdatePatientEntityListWithChanges<out TEntity> : IEntityRequest<TEntity> where TEntity : IEntity
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
        int ListId { get; }
        string EntityName { get; }
        string Attribute { get; }
        string SyntomName { get; }
        string InterviewName { get; }
    }
}