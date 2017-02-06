using System.ComponentModel.Composition;
using SystemInterfaces;
using ViewModelInterfaces;

namespace ViewModel.Interfaces
{
    
    public interface ICacheViewModel : IViewModel
    {

    }
    
    public interface IEntityCacheViewModel<TEntity> :ICacheViewModel, IEntityListViewModel<TEntity> where TEntity : IEntity//
    {

    }

    public interface IEntityCacheViewModeltest<TEntity> : ICacheViewModel
    {

    }
    public interface IEntityViewCacheViewModel<TView> : ICacheViewModel, IEntityListViewModel<TView> where TView : IEntityView
    {

    }
}

