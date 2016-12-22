using System;
using System.Collections.Generic;
using SystemInterfaces;
using DataInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class WriteEntityViewModelInfo<T> : ViewModelInfo, IWriteEntityViewModelInfo<T> where T : IEntity
    {
        public WriteEntityViewModelInfo(int processId, Type viewModelType, Func<T> createEntityAction,
            Func<T> createNullEntityAction, List<IEventSubscription<IViewModel, IEvent>> viewModelEventSubscriptions, List<IEventPublication<IViewModel, IEvent>> viewModelEventPublications)
            : base(processId, viewModelEventSubscriptions,viewModelEventPublications,typeof(WriteEntityViewModelInfo<T>))
        {
            CreateEntityAction = createEntityAction;
            CreateNullEntityAction = createNullEntityAction;
            EntityType = default(T);
        }


        public Func<T> CreateEntityAction { get; }
        public Func<T> CreateNullEntityAction { get; }
        public T EntityType { get; }
        
    }

    public class ReadEntityViewModelInfo<T> : ViewModelInfo, IReadEntityViewModelInfo<T> where T : IEntity
    {
        public ReadEntityViewModelInfo(int processId, Type viewModelType, List<IEventSubscription<IViewModel, IEvent>> viewModelEventSubscriptions, List<IEventPublication<IViewModel, IEvent>> viewModelEventPublications)
            : base(processId, viewModelEventSubscriptions,viewModelEventPublications, typeof(ReadEntityViewModelInfo<T>))
        {
           EntityType = default(T);
        }
        public T EntityType { get; }

    }
}