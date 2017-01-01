using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using SystemMessages;
using BootStrapper;
using CommonMessages;
using EventAggregator;
using EventMessages;
using JB.Collections.Reactive;
using JB.Reactive.ExtensionMethods;
using ReactiveUI;
using Utilities;
using ViewModel.Interfaces;


namespace Core.Common.UI
{
    public static class ViewModelExtensions
    {
        public static void WireEvents(this IViewModel viewModel)
        {

            foreach (var itm in viewModel.CommandInfo)
            {
                var publishMessage = CreatePublishMessageAction(viewModel, itm);

                var cmd = ReactiveCommand.Create(publishMessage,itm.CommandPredicate.Invoke(viewModel));//new Action<IViewModel>(x => { })

                viewModel.Commands.Add(itm.CommandName, cmd);

                //var subject = itm.Subject.Invoke(viewModel);

                

                //subject//.Where(x => itm.SubjectPredicate.All(z => z.Invoke(viewModel, x)))
                //   .Subscribe(publishMessage);

            }
            //EventMessageBus.Current.GetEvent<CurrentEntityChanged<IAddresses>>(SourceMessage).Subscribe(x => handleIdChanged(x.EntityId));  
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
                var subject  = itm.Subject.Invoke(viewModel);

                var publishMessage = CreatePublishMessageAction(viewModel, itm);
                subject.Where(x => itm.SubjectPredicate.All(z => z.Invoke(viewModel)))
                    .Subscribe(publishMessage);
            }

           EventMessageBus.Current.Publish(new RequestProcessState(viewModel.Process, viewModel.SourceMessage), viewModel.SourceMessage);

        }

        private static Action<dynamic> CreatePublishMessageAction(IViewModel viewModel, IViewModelEventPublication<IViewModel, IEvent> itm)
        {
            Action<dynamic> publishMessage = x =>
            {
                var paramArray = itm.MessageData.Select(p => p.Invoke(viewModel)).Cast<object>().ToList();
                paramArray.Add(viewModel.Process);
                paramArray.Add(viewModel.SourceMessage);
                var concreteEvent = BootStrapper.BootStrapper.Container.GetExportedTypes(itm.EventType).FirstOrDefault();
                var msg = (ProcessSystemMessage) Activator.CreateInstance(concreteEvent??itm.EventType, paramArray.ToArray());
                EventMessageBus.Current.Publish(msg, viewModel.SourceMessage);
            };
            return publishMessage;
        }

        public static void Subscribe<TEvent, TViewModel>(TViewModel viewModel, Func<TEvent, bool> eventPredicate,
            IEnumerable<Func<TViewModel, TEvent, bool>> predicate, Action<TViewModel, TEvent> action)
        {
            EventMessageBus.Current.GetEvent<TEvent>(((IViewModel)viewModel).SourceMessage).Where(eventPredicate).Where(x => predicate.All(z => z.Invoke(viewModel, x))).Subscribe(x => action.Invoke(viewModel, x));
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
