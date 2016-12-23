using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using SystemInterfaces;
using Common;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using FluentValidation;
using JB.Collections.Reactive;
using ReactiveUI;
using Utilities;
using ViewModel.Interfaces;
using ValidationResult = FluentValidation.Results.ValidationResult;


namespace Core.Common.UI
{
    public partial class BaseWriteEntityViewModel<TEntity,TViewModel> :BaseViewModel<TViewModel>, IWriteEntityViewModel<TEntity>
// ReactiveObject, IViewModel where TEntity:IEntity
    {

        protected AbstractValidator<TEntity> Validator { get; }
        protected ValidationResult ValidationResults = new ValidationResult();
       
        private static bool _intialize = false;

        
        protected BaseWriteEntityViewModel(AbstractValidator<TEntity> validator, ISystemProcess process, List<IEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IEventPublication<IViewModel, IEvent>> eventPublications, List<IViewCommand<IViewModel, IEvent>> commands) : base(process, eventSubscriptions, eventPublications, commands)
        {
            Validator = validator;
            Process = process;
            if (_intialize == false)
            {
                WireEvents();
                _intialize = true;
            }
            CrudeOpsContructor();
           
        }

        private void WireEvents()
        {
            this.ObservableForProperty(x => x.ValidationResults.Errors).Where(x => x.Value.Count > 0)
                .Subscribe(x => ErrorsChanged(this, new DataErrorsChangedEventArgs(x.Value.First().PropertyName)));

            //Save Changes on Expression Change
            ChangeTracking.DictionaryChanges.Subscribe(x => HandleSaveChanges());


           EventMessageBus.Current.GetEvent<EntityUpdated<TEntity>>(MsgSource)
                                .Subscribe(z => handleEntityUpdated(z.Entity));

             EventMessageBus.Current.GetEvent<EntityCreated<TEntity>>(MsgSource)
                .Subscribe(x => HandleEntityCreated(x.Entity));

            EventMessageBus.Current.GetEvent<EntityDeleted<TEntity>>(MsgSource)
                .Subscribe(x => HandleEntityDeleted(x.EntityId));

            EventMessageBus.Current.GetEvent<EntitySetWithFilterLoaded<TEntity>>(MsgSource)
                .Subscribe(x => handleEntitySetUpdated(x.Entities));

            EventMessageBus.Current.GetEvent<EntitySetLoaded<TEntity>>(MsgSource)
                .Subscribe(x => handleEntitySetUpdated(x.Entities));

            EventMessageBus.Current.GetEvent<EntitySetWithFilterWithIncludesLoaded<TEntity>>(MsgSource)
                .Subscribe(x => handleEntitySetUpdated(x.Entities));
        }

       

        partial void CrudeOpsContructor();
        private TEntity _currentEntity;

        public TEntity CurrentEntityWithChanges { get; set; }

        public TEntity CurrentEntity
        {
            get { return _currentEntity; }
            set
            {
                if (value == null) return;
                if (Equals(_currentEntity, value)) return;
                //if(value.Id == EntityStates.NullEntity) return;
                // Application.Current.Dispatcher.Invoke(() =>this.RaiseAndSetIfChanged(ref _currentEntity, value)); 
                this.RaiseAndSetIfChanged(ref _currentEntity, value);

                if (_currentEntity != null && _currentEntity.Id != EntityStates.NullEntity)
                {
                    ValidateChangeTracking(_currentEntity);
                   
                   EventMessageBus.Current.Publish(new CurrentEntityChanged<TEntity>(_currentEntity.Id,Process, MsgSource), MsgSource);
                }
            }
        }

        protected void ValidateChangeTracking(TEntity instance)
        {
            CurrentEntityWithChanges = instance.DeepClone();
            ChangeTracking.Clear();
            ChangeTracking.DictionaryChanges.Subscribe(x => ValidationResults = Validator.Validate(instance));
        }

        protected void handleEntityUpdated(TEntity newEntity)
        {
            try
            {

                if (EntitySet == null) return;
                var oldEntity = EntitySet.FirstOrDefault(x => x.Id == newEntity.Id);
                if (oldEntity == null) return;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ReplaceItemInEntitySet(newEntity, oldEntity);
                    //ce.ApplyChanges(p);
                    CurrentEntity = newEntity;
                });
                
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void ReplaceItemInEntitySet(TEntity newEntity, TEntity oldEntity)
        {
            oldEntity.ApplyChanges(newEntity);
            //var index = EntitySet.IndexOf(oldEntity);
            //EntitySet.RemoveAt(index);
            //EntitySet.Insert(index, newEntity);
        }

        private List<Expression<Func<TEntity, bool>>> _filterExpression;

        public List<Expression<Func<TEntity, bool>>> FilterExpression
        {
            get { return _filterExpression; }
            set
            {
                if (!Equals(_filterExpression, value))
                {
                    _filterExpression = value;
                    DataRequest = new LoadEntitySetWithFilter<TEntity>(_filterExpression,Process, MsgSource) ;
                    EventMessageBus.Current.Publish(DataRequest, new MessageSource(MsgSource.Source + " FilterExpression"));
                }
            }
        }
       
        private LoadEntitySetWithFilter<TEntity> dataRequest;

        protected LoadEntitySetWithFilter<TEntity> DataRequest
        {
            get { return dataRequest; }
            set
            {
                if(value != null)
                dataRequest = value;
            }

        }

        protected virtual void handleEntitySetUpdated(IList<TEntity> entities)
        {
           
                EntitySet = new ObservableList<TEntity>(entities.Select(x => (TEntity)x).ToList());
                RowState = RowState.Loaded;
          
        }

        private ObservableList<TEntity> _EntitySet = new ObservableList<TEntity>();
        public virtual ObservableList<TEntity> EntitySet
        {
            get
            {
                return _EntitySet;
            }
            set
            {
                
                    this.RaiseAndSetIfChanged(ref _EntitySet, value ?? new ObservableList<TEntity>());
                    if (_EntitySet.Count > 0) CurrentEntity = _EntitySet[0];
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
                this.RaiseAndSetIfChanged(ref _selectedEntities, value ?? new ObservableList<TEntity>());
            }
        }

        public void ViewAll()
        {
            FilterExpression = new List<Expression<Func<TEntity, bool>>>() { p => p != null };
        }

        public dynamic GetValue([CallerMemberName] string property = "UnspecifiedProperty")
        {
            
            return ChangeTracking.ContainsKey(property)
                ? ChangeTracking[property]
                :  CurrentEntityWithChanges.GetType().GetProperty(property).GetValue(CurrentEntityWithChanges);
        }
        protected dynamic GetOriginalValue([CallerMemberName] string property = "UnspecifiedProperty")
        {
            return CurrentEntity.GetType().GetProperty(property).GetValue(CurrentEntity);
        }
        protected bool GetPropertyIsChanged([CallerMemberName] string property = "UnspecifiedProperty")
        {
            return ChangeTracking.ContainsKey(property);
        }
        public void SetValue( dynamic value, [CallerMemberName] string property = "UnspecifiedProperty")
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

        protected void ClearData()
        {
            EntitySet = new ObservableBindingList<TEntity>();
            CurrentEntity = CreateNullEntity();
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