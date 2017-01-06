using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEntityView<TEntity>: IEntityView where TEntity: IEntity
    {
      
    }

    [InheritedExport]
    public interface IEntityView : IEntityId 
    {
       
    }


}