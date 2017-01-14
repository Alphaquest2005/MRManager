using System;
using System.Collections.Generic;
using System.Linq;
using SystemInterfaces;
using Utilities;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewEventCommand<TViewModel, TEvent> : ViewModelEventCommand<TViewModel,TEvent>, IViewModelEventCommand<IViewModel,IEvent> where TViewModel : IViewModel where TEvent : IEvent
    {
        public ViewEventCommand(string key, IEnumerable<Func<TViewModel, bool>> commandPredicate, Func<TViewModel, IObservable<dynamic>> subject, Func<TViewModel, IViewEventCommandParameter> messageData) : base(key, subject, commandPredicate, messageData)
        {
           
            MessageData = (Func<IViewModel, IViewEventCommandParameter>)base.MessageData.Convert(typeof(IViewModel), typeof(IViewEventCommandParameter));
            CommandPredicate = base.CommandPredicate.Select(x => (Func<IViewModel, bool>)x.Convert(typeof(IViewModel), typeof(bool))).ToList();
            Subject = (Func<IViewModel, IObservable<dynamic>>)base.Subject.Convert(typeof(IViewModel), typeof(IObservable<dynamic>));
        }

       
        public new Func<IViewModel, IObservable<dynamic>> Subject { get; }
        public new IEnumerable<Func<IViewModel, bool>> CommandPredicate { get; }
       public new Func<IViewModel, IViewEventCommandParameter> MessageData { get; }
        //public IEnumerable<Func<IViewModel, bool>> SubjectPredicate { get; }
        
    }
}