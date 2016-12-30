using System.ComponentModel.Composition;
using SystemInterfaces;

namespace StartUp.Messages
{
    [InheritedExport]
    public interface IDataService<TEntity>:IService<IDataService<TEntity>> where TEntity:IEntity
    {
    }
}