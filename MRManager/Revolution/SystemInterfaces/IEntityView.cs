using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IEntityView<TEntity>: IEntityView where TEntity: IEntity
    {
      
    }

    
    public interface IEntityView : IEntityId 
    {
       
    }


}