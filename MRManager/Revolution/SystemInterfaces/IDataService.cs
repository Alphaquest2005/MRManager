using System.ComponentModel.Composition;
using SystemInterfaces;

namespace StartUp.Messages
{
    
    public interface IDataService<TEntity>:IService<IDataService<TEntity>> where TEntity:IEntity
    {
    }
}