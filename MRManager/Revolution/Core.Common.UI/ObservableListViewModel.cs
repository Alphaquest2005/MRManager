using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using SystemInterfaces;


using FluentValidation;
using FluentValidation.Results;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ReactiveUI;
using RevolutionEntities;
using ValidationSets;
using ViewModel.Interfaces;


namespace Core.Common.UI
{
    public partial class ObservableListViewModel<TEntity> : BaseViewModel<ObservableListViewModel<TEntity>> where TEntity: IEntityId
    {
        protected AbstractValidator<TEntity> Validator { get; }
        protected ValidationResult ValidationResults = new ValidationResult();
        protected static ObservableViewModel<TEntity> _instance = null;
        public static ObservableViewModel<TEntity> Instance => _instance;

        public ObservableListViewModel(List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process, Type orientation) : base(process,eventSubscriptions,eventPublications,commandInfo, orientation)
        {
            Validator = new EntityValidator<TEntity>();
        }

        private ReactiveProperty<IProcessStateList<TEntity>> _state = new ReactiveProperty<IProcessStateList<TEntity>>();
        public ReactiveProperty<IProcessStateList<TEntity>> State
        {
            get { return _state; }
            set
            {
                this.RaiseAndSetIfChanged(ref _state, value);
                CurrentEntity.Value = _state.Value.Entity;
                EntitySet = new ObservableList<TEntity>(_state.Value.EntitySet.ToList());
                SelectedEntities = new ObservableList<TEntity>(_state.Value.SelectedEntities.ToList());

                // must broad cast event to handle simultanious events per single event IE continue chaining events

            }
        }

       private ReactiveProperty<TEntity> _currentEntity = new ReactiveProperty<TEntity>(NullEntity<TEntity>.Instance);
        public ReactiveProperty<TEntity> CurrentEntity
        {
            get { return _currentEntity; }
            set
            {
                this.RaiseAndSetIfChanged(ref _currentEntity, value);
            }
        }
       
        private ObservableList<TEntity> _entitySet = new ObservableList<TEntity>();
        public virtual ObservableList<TEntity> EntitySet
        {
            get
            {
                return _entitySet;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _entitySet, value);
                
            }
        }

        private ObservableList<TEntity> _selectedEntities = new ObservableList<TEntity>();
        public ObservableList<TEntity> SelectedEntities
        {
            get
            {
                return _selectedEntities;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedEntities, value);
               
            }
        }


        public dynamic GetValue([CallerMemberName] string property = "UnspecifiedProperty")
        {
            var prop =  CurrentEntity.Value.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if(prop == null) return null;
            return ChangeTracking.ContainsKey(property)
                ? ChangeTracking[property]
                : prop.GetValue(CurrentEntity.Value);
        }
        protected dynamic GetOriginalValue([CallerMemberName] string property = "UnspecifiedProperty")
        {
            return CurrentEntity.Value.GetType().GetProperty(property).GetValue(CurrentEntity);
        }
        protected bool GetPropertyIsChanged([CallerMemberName] string property = "UnspecifiedProperty")
        {
            return ChangeTracking.ContainsKey(property);
        }
        public void SetValue(dynamic value, [CallerMemberName] string property = "UnspecifiedProperty")
        {
            if (CurrentEntity.Value.GetType().GetProperty(property, BindingFlags.Public |BindingFlags.SetProperty| BindingFlags.Instance) == null) return;
            if (!ChangeTracking.ContainsKey(property))
            {
                ChangeTracking.AddOrUpdate(property, value);
            }
            else
            {
                ChangeTracking[property] = value;
            }
            this.RaisePropertyChanged(property);
        }
        public ObservableDictionary<string, dynamic> ChangeTracking { get; } = new ObservableDictionary<string, dynamic>();



        public IEnumerable GetErrors(string propertyName)
        {
            return ValidationResults.Errors.Where(x => x.PropertyName == propertyName);
        }

        public bool HasErrors => !ValidationResults.IsValid;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


    }

}