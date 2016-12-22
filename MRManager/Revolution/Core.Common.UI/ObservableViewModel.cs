using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using SystemInterfaces;
using DataInterfaces;
using FluentValidation;
using FluentValidation.Results;
using JB.Collections.Reactive;

using ReactiveUI;


namespace Core.Common.UI
{
    public abstract partial class ObservableViewModel<TEntity> : BaseViewModel<ObservableViewModel<TEntity>> where TEntity:IEntity
    {
        protected AbstractValidator<TEntity> Validator { get; }
        protected ValidationResult ValidationResults = new ValidationResult();
        protected static ObservableViewModel<TEntity> _instance = null;
        public static ObservableViewModel<TEntity> Instance => _instance;

        protected ObservableViewModel(AbstractValidator<TEntity> validator, List<IEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IEventPublication<IViewModel, IEvent>> eventPublications, ISystemProcess process) : base(process,eventSubscriptions,eventPublications)
        {
            
            Validator = validator;
        }

        public TEntity CurrentEntityWithChanges { get; set; }

        private TEntity _currentEntity;
        public TEntity CurrentEntity
        {
            get { return _currentEntity; }
            set
            {
                this.RaiseAndSetIfChanged(ref _currentEntity, value);
            }
        }

        private ObservableList<Expression<Func<TEntity, bool>>> _filterExpression;

        public ObservableList<Expression<Func<TEntity, bool>>> FilterExpression
        {
            get { return _filterExpression; }
            set
            {
                this.RaiseAndSetIfChanged(ref _filterExpression, value);
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

            return ChangeTracking.ContainsKey(property)
                ? ChangeTracking[property]
                : CurrentEntityWithChanges.GetType().GetProperty(property).GetValue(CurrentEntityWithChanges);
        }
        protected dynamic GetOriginalValue([CallerMemberName] string property = "UnspecifiedProperty")
        {
            return CurrentEntity.GetType().GetProperty(property).GetValue(CurrentEntity);
        }
        protected bool GetPropertyIsChanged([CallerMemberName] string property = "UnspecifiedProperty")
        {
            return ChangeTracking.ContainsKey(property);
        }
        public void SetValue(dynamic value, [CallerMemberName] string property = "UnspecifiedProperty")
        {

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