using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IEntityRequest< out TEntity>:IEntityRequest where TEntity : IEntity
    {
    }

    public interface IEntityRequest:IProcessSystemMessage
    {
        Type ViewType { get; }
    }
}