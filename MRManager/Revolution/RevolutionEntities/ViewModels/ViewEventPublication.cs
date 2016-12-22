using System;
using System.Collections.Generic;
using SystemInterfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewEventPublication<TViewModel, TEvent> : IEventPublication<TViewModel, TEvent> where TViewModel:IViewModel where TEvent:IEvent
    {
        public Func<TEvent, bool> EventPredicate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Type EventType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int ProcessId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Func<TViewModel, IObservable<IObservable<dynamic>>> Subject
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Func<TViewModel, IObservable<IObservable<dynamic>>, TEvent, bool>> SubjectPredicate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Type ViewModelType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}