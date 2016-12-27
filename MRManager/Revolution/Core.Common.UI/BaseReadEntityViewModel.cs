//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using SystemInterfaces;
//using SystemMessages;
//using DataInterfaces;
//using EventAggregator;
//using EventMessages;
//using JB.Collections.Reactive;
//using ReactiveUI;
//using Utilities;
//using ViewModel.Interfaces;


//namespace Core.Common.UI
//{
//    public abstract partial class BaseReadEntityViewModel<TView> : BaseViewModel<BaseReadEntityViewModel<TView>>, IReadEntityViewModel<TView> where TView:IEntity

//    {
//        private static bool _intialize = false;
//        protected static BaseReadEntityViewModel<TView> _instance = null;
//        public static BaseReadEntityViewModel<TView> Instance => _instance;

//        protected BaseReadEntityViewModel(List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process) : base(process,eventSubscriptions,eventPublications,commandInfo)
//        {
//            _instance = this;
//            Process = process;
//            //Validator = validator;
//            if (_intialize == false)
//            {
//                WireEvents();
//                _intialize = true;
//            }
            
           
//        }

//        private void WireEvents()
//        {
//            EventMessageBus.Current.GetEvent<ServiceStarted<EntitySetWithFilterLoaded<TView>>>(SourceMessage)
//                .Subscribe(z => EventMessageBus.Current.GetEvent<EntitySetWithFilterLoaded<TView>>(SourceMessage).Subscribe(x => HandleEntitySetUpdated(x.Entities)));

//        }

        
//        private TView _currentEntity;

        
//        public TView CurrentEntity
//        {
//            get { return _currentEntity; }
//            set
//            {
//                if (value == null) return;
//                if (Equals(_currentEntity, value)) return;
               
//                this.RaiseAndSetIfChanged(ref _currentEntity, value); 
                
//                if (_currentEntity != null && _currentEntity.Id != EntityStates.NullEntity)
//                {
//                  EventMessageBus.Current.Publish(new CurrentEntityChanged<TView>(_currentEntity.Id,Process, MsgSource), MsgSource);
//                }
//            }
//        }


//        protected virtual void HandleEntitySetUpdated(IList<TView> entities)
//        {
//           EntitySet = new ObservableList<TView>(entities.Select(x => (TView)x).ToList());
//        }

//        private ObservableList<TView> _entitySet = new ObservableList<TView>();
//        public virtual ObservableList<TView> EntitySet
//        {
//            get
//            {
//                return _entitySet;
//            }
//            set
//            {
//                this.RaiseAndSetIfChanged(ref _entitySet, value ?? new ObservableList<TView>());
//                if (_entitySet.Count > 0) CurrentEntity = _entitySet[0];
//            }
//        }

//        private ObservableList<TView> _selectedEntities = new ObservableList<TView>();
//        public ObservableList<TView> SelectedEntities
//        {
//            get
//            {
//                return _selectedEntities;
//            }
//            set
//            {
//                this.RaiseAndSetIfChanged(ref _selectedEntities, value ?? new ObservableList<TView>());
//            }
//        }

       
//        protected dynamic GetValue([CallerMemberName] string property = "UnspecifiedProperty")
//        {

//            return CurrentEntity.GetType().GetProperty(property).GetValue(CurrentEntity);
//        }
        
      
//    }
//}