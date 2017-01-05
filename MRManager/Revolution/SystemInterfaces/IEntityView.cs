using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEntityView<TEntity>:IEntityId where TEntity: IEntity
    {
        Type EntityType { get; }
    }

    
}