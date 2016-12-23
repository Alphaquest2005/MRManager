using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    .Invoke(viewModel, new object[] { viewModel, itm.EventPredicate, itm.ActionPredicate, itm.Action });


                // Wire(itm.EventType,itm.EventPredicate, itm.ActionPredicate,itm.Action);
            }

            foreach (var itm in viewModel.EventPublications)
            {
                var subject = itm.Subject.Invoke(viewModel);
                Action<dynamic> publishMessage = x => {
                    var paramArray = itm.MessageData.Select(p => p.Invoke(viewModel)).Cast<object>().ToList();
                    paramArray.Add(viewModel.Process);
                    paramArray.Add(new MessageSource(viewModel.GetType().ToString()));
                    var msg = (SystemProcessMessage)Activator.CreateInstance(itm.EventType, paramArray.ToArray());
                    EventMessageBus.Current.Publish(msg, msg.Source);
                };
                subject.Where(x => itm.SubjectPredicate.All(z => z.Invoke(viewModel, x)))
                    .Subscribe(publishMessage);
            }

        }

        public static void Subscribe<TEvent, TViewModel>(TViewModel viewModel, Func<TEvent, bool> eventPredicate,
            IEnumerable<Func<TViewModel, TEvent, bool>> predicate, Action<TViewModel, TEvent> action)
        {
            EventMessageBus.Current.GetEvent<TEvent>(new MessageSource(viewModel.GetType().ToString())).Where(eventPredicate).Where(x => predicate.All(z => z.Invoke(viewModel, x))).Subscribe(x => action.Invoke(viewModel, x));
        }

        public static void WireCommands(this DynamicViewModel<IViewModel> viewModel)
        {
            
        }

        public static void VerifyConstuctorVsParameterArray(Type t, params object[] p)
        {
            System.Diagnostics.Debug.WriteLine("<---- foo");
            foreach (System.Reflection.ConstructorInfo ci in t.GetConstructors())
            {
                System.Diagnostics.Debug.WriteLine(t.FullName + ci.ToString());
            }
            foreach (object o in p)
            {
                System.Diagnostics.Debug.WriteLine("param:" + o.GetType().FullName);
            }
            System.Diagnostics.Debug.WriteLine("foo ---->");
        }
    }
}
