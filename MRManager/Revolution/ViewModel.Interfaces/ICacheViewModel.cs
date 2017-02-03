using System.ComponentModel.Composition;
using SystemInterfaces;
using ViewModelInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface ICacheViewModel : IViewModel
    {

    }
    [InheritedExport]
    public interface IEntityCacheViewModel<TEntity> :ICacheViewModel, IEntityListViewModel<TEntity> where TEntity : IEntity
    {

    }

    [InheritedExport]
    public interface IEntityViewCacheViewModel<TView> : ICacheViewModel, IEntityListViewModel<TView> where TView : IEntityView
    {

    }
}