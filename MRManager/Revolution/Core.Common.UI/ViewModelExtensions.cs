using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using SystemInterfaces;
using CommonMessages;
using EventAggregator;

namespace Core.Common.UI
{
    public static class ViewModelExtensions
    {
        public static void WireEvents(this IViewModel viewModel)
        {


            //EventMessageBus.Current.GetEvent<CurrentEntityChanged<IAddresses>>(MsgSource).Subscribe(x => handleIdChanged(x.EntityId));  
            foreach (var itm in viewModel.EventSubscriptions)
            {
                typeof(ViewModelExtensions)
                    .GetMethod("Subscribe")
                    .MakeGenericMethod(itm.EventType, viewModel.GetType())
                    .Invoke(viewModel, new object[] {viewModel, itm.EventPredicate, itm.ActionPredicate, itm.Action});


                // Wire(itm.EventType,itm.EventPredicate, itm.ActionPredicate,itm.Action);
            }

            foreach (var itm in viewModel.EventPublications)
            {
                

              


                // Wire(itm.EventType,itm.EventPredicate, itm.ActionPredicate,itm.Action);
            }

        }

        public static void Subscribe<TEvent, TViewModel>(TViewModel viewModel, Func<TEvent, bool> eventPredicate,
            IEnumerable<Func<TViewModel, TEvent, bool>> predicate, Action<TViewModel, TEvent> action)
        {
            EventMessageBus.Current.GetEvent<TEvent>(new MessageSource(viewModel.GetType().ToString())).Where(eventPredicate).Where(x => predicate.All(z => z.Invoke(viewModel, x))).Subscribe(x => action.Invoke(viewModel, x));
        }

        public static void Publish<TViewModel>(TViewModel viewModel, SystemMessage msg)
        {
            EventMessageBus.Current.Publish(msg, new MessageSource(viewModel.GetType().ToString()));
        }
    }
}