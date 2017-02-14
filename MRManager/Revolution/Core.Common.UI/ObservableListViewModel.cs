using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using SystemInterfaces;


using FluentValidation;
using FluentValidation.Results;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ReactiveUI;
using RevolutionEntities;
using ValidationSets;
using ViewModel.Interfaces;
using ViewModelInterfaces;


namespace Core.Common.UI
{



    
    public partial class ObservableListViewModel<TEntity> : BaseViewModel<ObservableListViewModel<TEntity>>, IEntityListViewModel<TEntity> where TEntity: IEntityId//
    {

        protected AbstractValidator<TEntity> Validator { get; }
        protected ValidationResult ValidationResults = new ValidationResult();
        protected static ObservableListViewModel<TEntity> _instance = null;
        public static ObservableListViewModel<TEntity> Instance => _instance;

        public ObservableListViewModel() { }

        public ObservableListViewModel(List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process, Type orientation) : base(process,eventSubscriptions,eventPublications,commandInfo, orientation)
        {
            Validator = new EntityValidator<TEntity>();
            State.WhenAnyValue(x => x.Value).Subscribe(x => UpdateLocalState(x));
            CurrentEntity.WhenAnyValue(x => x.Value).Subscribe(x => ChangeTracking.Clear());

            _instance = this;
        }

        private void UpdateLocalState(IProcessStateList<TEntity> state)
        {
            if (state == null) return;
            //CurrentEntity.Value = state.Entity;
            EntitySet = new ObservableList<TEntity>(state.EntitySet.ToList());
                
            
               if (!SelectedEntities.SequenceEqual(state.SelectedEntities.ToList())) SelectedEntities = new ObservableList<TEntity>(state.SelectedEntities.ToList());
        }

        
        
        ReactiveProperty<IProcessState<TEntity>> IEntityViewModel<TEntity>.State
        {
            get { return new ReactiveProperty<IProcessState<TEntity>>(_state.Value); }
        }

        //HACK:NEVER USER THIS IMPLEMENTATION - FOR SOME FUCKED UP REASON IT NOT RAISING CHANGE NOTIFICATIONS EVEN IF YOU NEVER CALL THE SETTER....
        // public ReactiveProperty<IProcessStateList<TEntity>> State => new ReactiveProperty<IProcessStateList<TEntity>>();

        private ReactiveProperty<IProcessStateList<TEntity>> _state = new ReactiveProperty<IProcessStateList<TEntity>>();
        public ReactiveProperty<IProcessStateList<TEntity>> State
        {
            get { return _state; }
            set { this.RaiseAndSetIfChanged(ref _state, value);}
        }

        private ReactiveProperty<TEntity> _currentEntity = new ReactiveProperty<TEntity>(NullEntity<TEntity>.Instance,ReactivePropertyMode.DistinctUntilChanged);
        public ReactiveProperty<TEntity> CurrentEntity
        {
            get { return _currentEntity; }
            set { this.RaiseAndSetIfChanged(ref _currentEntity, value); }
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
            if (CurrentEntity.Value == null) return null;
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
                //HACK: doing this shit cuz jbcollection generating some error long after add no control to prevent error
                try
                {
                    ChangeTracking.AddOrUpdate(property, value);
                }
                catch
                {
                }

            }
            else
            {
                ChangeTracking[property] = value;
            }
            this.RaisePropertyChanged(property);
        }

        
        public ObservableDictionary<string, dynamic> ChangeTracking { get; } = new ObservableDictionary<string, dynamic>();

        private ObservableBindingList<TEntity> _changeTrackingList = new ObservableBindingList<TEntity>();

        

        public ObservableBindingList<TEntity> ChangeTrackingList
        {
            get { return _changeTrackingList; }
            set { _changeTrackingList = value; }
        }


        public IEnumerable GetErrors(string propertyName)
        {
            return ValidationResults.Errors.Where(x => x.PropertyName == propertyName);
        }

        public bool HasErrors => !ValidationResults.IsValid;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;




    }

}