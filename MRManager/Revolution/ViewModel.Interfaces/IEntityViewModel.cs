using System.ComponentModel.Composition;
using SystemInterfaces;
using FluentValidation;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ViewModel.Interfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface IEntityViewModel<TEntity>: IViewModel where TEntity:IEntity
    {
        AbstractValidator<TEntity> Validator { get; }
        ReactiveProperty<IProcessState<TEntity>> State { get; }
        ObservableDictionary<string, dynamic> ChangeTracking { get; }


    }

    
}
