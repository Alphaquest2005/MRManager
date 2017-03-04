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
using RevolutionData.Context;
using ValidationSets;
using ViewModel.Interfaces;
using ViewModelInterfaces;


namespace Core.Common.UI
{

    public partial class ObservableViewModel<TEntity> : BaseViewModel<ObservableViewModel<TEntity>> , IEntityViewModel<TEntity> where TEntity:IEntityId
    {
        public AbstractValidator<TEntity> Validator { get; }
        protected ValidationResult ValidationResults = new ValidationResult();
        protected static ObservableViewModel<TEntity> _instance = null;
        public static ObservableViewModel<TEntity> Instance => _instance;

        public ObservableViewModel(IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process, Type orientation, int priority) : base(process, viewInfo,eventSubscriptions,eventPublications,commandInfo, orientation, priority)
        {
            //Leave the validation for client side input validation...
            Validator = new EntityValidator<TEntity>();
            
        }

        

        private ReactiveProperty<IProcessState<TEntity>> _state = new ReactiveProperty<IProcessState<TEntity>>(null, ReactivePropertyMode.DistinctUntilChanged) ;
        public ReactiveProperty<IProcessState<TEntity>> State
        {
            get { return _state; }
            set
            {
                this.RaiseAndSetIfChanged(ref _state, value);
                // must broad cast event to handle simultanious events per single event IE continue chaining events
                
            }
        }
       
       
        public dynamic GetValue([CallerMemberName] string property = "UnspecifiedProperty")
        {
            if (State == null || State.Value == null) return null;
            var prop =  State?.Value.Entity.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if(prop == null) return null;
            return ChangeTracking.ContainsKey(property)
                ? ChangeTracking[property]
                : prop.GetValue(State.Value.Entity);
        }
        protected dynamic GetOriginalValue([CallerMemberName] string property = "UnspecifiedProperty")
        {
            return State.Value.Entity.GetType().GetProperty(property).GetValue(State);
        }
        protected bool GetPropertyIsChanged([CallerMemberName] string property = "UnspecifiedProperty")
        {
            return ChangeTracking.ContainsKey(property);
        }
        public void SetValue(dynamic value, [CallerMemberName] string property = "UnspecifiedProperty")
        {
            if (State.Value.Entity.GetType().GetProperty(property, BindingFlags.Public |BindingFlags.SetProperty| BindingFlags.Instance) == null) return;
            if (!ChangeTracking.ContainsKey(property))
            {
                //Hack: fix this
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
        public void NotifyPropertyChanged(string propertyName)
        {
            this.RaisePropertyChanged(propertyName);
        }


        public IEnumerable GetErrors(string propertyName)
        {
            return ValidationResults.Errors.Where(x => x.PropertyName == propertyName);
        }

        public bool HasErrors => !ValidationResults.IsValid;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


    }

    public class ObservableViewModel : BaseViewModel<ObservableViewModel>
    {
        public ObservableViewModel(IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process, Type orientation, int priority) : base(process, viewInfo, eventSubscriptions, eventPublications, commandInfo, orientation, priority)
        {
        }
    }

    }