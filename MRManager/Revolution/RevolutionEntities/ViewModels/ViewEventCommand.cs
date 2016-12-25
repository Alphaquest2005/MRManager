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
        public ViewEventCommand(string commandName, Func<TViewModel, IObservable<bool>> commandPredicate, Func<TViewModel, IObservable<dynamic>> subject, IEnumerable<Func<TViewModel, bool>> subjectPredicate, IEnumerable<Func<TViewModel, dynamic>> messageData) : base(commandName, commandPredicate, subject, subjectPredicate, messageData)
        {
            CommandPredicate = (Func<IViewModel, IObservable<bool>>)base.CommandPredicate.Convert(typeof(IViewModel), typeof(IObservable<bool>));
            MessageData = base.MessageData.Select(x => (Func<IViewModel, object>)x.Convert(typeof(IViewModel), typeof(object))).ToList();
            SubjectPredicate = base.SubjectPredicate.Select(x => (Func<IViewModel, bool>)x.Convert(typeof(IViewModel), typeof(bool))).ToList();
            Subject = (Func<IViewModel, IObservable<dynamic>>)base.Subject.Convert(typeof(IViewModel), typeof(IObservable<dynamic>));
        }

        public new Func<IViewModel, IObservable<bool>> CommandPredicate { get; }
        public new Func<IViewModel, IObservable<dynamic>> Subject { get; }
        public new IEnumerable<Func<IViewModel, bool>> SubjectPredicate { get; }
        public new IEnumerable<Func<IViewModel, dynamic>> MessageData { get; }
    }
}