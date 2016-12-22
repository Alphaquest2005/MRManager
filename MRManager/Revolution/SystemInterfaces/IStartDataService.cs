using System.ComponentModel.Composition;
using DataInterfaces;

namespace StartUp.Messages
{
    [InheritedExport]
    public interface IStartService<TService> where TService:IService<TService>
    {
    }
    public interface IViewModelService : IService<IViewModelService>
    {

    }

    public interface IProcessService : IService<IProcessService>
    {

    }

    public interface IDataService<TEntity>:IService<IDataService<TEntity>> where TEntity:IEntity
    {
    }

    public interface IService<TService>
    {
    }
}