using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IEntityView<TEntity>: IEntityView, IEquatable<TEntity> where TEntity: IEntity
    {
      
    }

    
    public interface IEntityView : IEntityId 
    {
       
    }


}