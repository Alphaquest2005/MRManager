using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using FluentValidation;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ViewModel.Interfaces;

namespace ViewModelInterfaces
{
    
    public interface IEntityViewModel<TEntity>: IViewModel where TEntity:IEntityId
    {
        ReactiveProperty<IProcessState<TEntity>> State { get; }
        ObservableDictionary<string, dynamic> ChangeTracking { get; }

        void NotifyPropertyChanged(string propertyName);

    }

    
    public interface IEntityListViewModel<TEntity> : IEntityViewModel<TEntity> where TEntity : IEntityId
    {
        IEntityListViewModel<TEntity> Instance { get; }
        new ReactiveProperty<IProcessStateList<TEntity>> State { get; }
        ReactiveProperty<TEntity> CurrentEntity { get; }
        ReactiveProperty<ObservableList<TEntity>> EntitySet { get; }
        ReactiveProperty<ObservableList<TEntity>> SelectedEntities { get; }
        
    }


}
