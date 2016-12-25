using System.ComponentModel.Composition;
using DataInterfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ViewModel.Interfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface IEntityViewModel<TEntity>: IViewModel where TEntity:IEntity
    {
        ReactiveProperty<TEntity> CurrentEntity { get; }
        ObservableDictionary<string, dynamic> ChangeTracking { get; }


    }

    
}
