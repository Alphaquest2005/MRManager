using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEntityView<out TEntity>:IEntity where TEntity:IEntity
    {
        Type EntityType { get; }
        
    }
}