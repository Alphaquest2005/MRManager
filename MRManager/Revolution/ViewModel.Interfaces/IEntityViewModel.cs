using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using FluentValidation;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ViewModel.Interfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface IEntityViewModel<TEntity>: IViewModel where TEntity:IEntityId
    {
        ReactiveProperty<IProcessState<TEntity>> State { get; }
        ObservableDictionary<string, dynamic> ChangeTracking { get; }

    }

    [InheritedExport]
    public interface IEntityListViewModel<TEntity> : IEntityViewModel<TEntity> where TEntity : IEntityId
    {
        new ReactiveProperty<IProcessStateList<TEntity>> State { get; }
        ReactiveProperty<TEntity> CurrentEntity { get; }
        ObservableList<TEntity> EntitySet { get; }
        ObservableList<TEntity> SelectedEntities { get; }
    }


}
