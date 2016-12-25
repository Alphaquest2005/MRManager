using System;
using System.Collections.Generic;
using System.Linq;
using SystemInterfaces;
using Utilities;

namespace RevolutionEntities.ViewModels
{
    public class ViewEventPublication<TViewModel, TEvent> :EventPublication<TViewModel, TEvent>, IEventPublication<IViewModel, IEvent> where TViewModel:IViewModel where TEvent:IEvent
    {
        
        public ViewEventPublication(Func<TViewModel, IObservable<dynamic>> subject, IEnumerable<Func<TViewModel, bool>> subjectPredicate, IEnumerable<Func<TViewModel, dynamic>> messageData)
                                    :base(subject, subjectPredicate, messageData)
        {
            MessageData = base.MessageData.Select(x => (Func<IViewModel,object>)x.Convert(typeof(IViewModel), typeof(object))).ToList();
            SubjectPredicate = base.SubjectPredicate.Select(x => (Func<IViewModel, bool>)x.Convert(typeof(IViewModel), typeof(bool))).ToList();
            Subject = (Func<IViewModel, IObservable<dynamic>>)base.Subject.Convert(typeof(IViewModel), typeof(IObservable<dynamic>)); 
        }

        public new Func<IViewModel, IObservable<dynamic>> Subject { get; }
        public new IEnumerable<Func<IViewModel, bool>> SubjectPredicate { get; }
        public new IEnumerable<Func<IViewModel, dynamic>> MessageData { get; }
    }
}