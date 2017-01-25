using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
using BootStrapper;
using CommonMessages;
using EventAggregator;
using EventMessages;
using EventMessages.Commands;
using ReactiveUI;
using RevolutionEntities.Process;
using ViewModel.Interfaces;


namespace Core.Common.UI
{
    public static class ViewModelExtensions
    {
        public static void WireEvents(this IViewModel viewModel)
        {

            foreach (var itm in viewModel.CommandInfo)
            {
                var subject = itm.Subject.Invoke(viewModel);
                

                if(subject.GetType() == Observable.Empty<ReactiveCommand<IViewModel, Unit>>().GetType())
                {
                    var publishMessage = CreateCommandMessageAction<IViewModel>(viewModel, itm);
                    var cmd = ReactiveCommand.Create(publishMessage);//, viewModel.WhenAny(x => x, x => itm.CommandPredicate.All(z => z.Invoke(x.Value))) );

                    viewModel.Commands.Add(itm.Key, cmd);
                }
                else
                {
                    var publishMessage = CreateCommandMessageAction<dynamic>(viewModel, itm);
                    subject.Where(x => itm.CommandPredicate.All(z => z.Invoke(viewModel)))
                     .Subscribe(publishMessage);
                }

                

                //var subject = itm.Subject.Invoke(viewModel);

                

                //subject//.Where(x => itm.SubjectPredicate.All(z => z.Invoke(viewModel, x)))
                //   .Subscribe(publishMessage);

            }
            //EventMessageBus.Current.GetEvent<CurrentEntityChanged<IAddresses>>(Source).Subscribe(x => handleIdChanged(x.EntityId));  
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

           EventMessageBus.Current.Publish(new RequestProcessState(new StateCommandInfo(viewModel.Process.Id, RevolutionData.Context.Process.Commands.PublishState), viewModel.Process, viewModel.Source), viewModel.Source);

        }

        private static Action<dynamic> CreatePublishMessageAction(IViewModel viewModel, IViewModelEventPublication<IViewModel, IEvent> itm)
        {
            Action<dynamic> publishMessage = x =>
            {
                var param = itm.MessageData.Invoke(viewModel);
                var paramArray = param.Params.ToList();
                paramArray.Add(param.ProcessInfo);
                paramArray.Add(param.Process);
                paramArray.Add(param.Source);
                var concreteEvent = BootStrapper.BootStrapper.Container.GetExportedTypes(itm.EventType).FirstOrDefault();
                //TODO: Replace MEF with Good IOC container - can't do <,>
                var msg = (ProcessSystemMessage) Activator.CreateInstance(concreteEvent??itm.EventType, paramArray.ToArray());
                EventMessageBus.Current.Publish(msg, viewModel.Source);
            };
            return publishMessage;
        }

        private static Action<TResult> CreateCommandMessageAction<TResult>(IViewModel viewModel, IViewModelEventCommand<IViewModel, IEvent> itm)
        {
            Action<TResult> publishMessage = x =>
            {
                var param = itm.MessageData.Invoke(viewModel);
                var paramArray = param.Params.ToList();
                paramArray.Add(param.ProcessInfo);
                paramArray.Add(param.Process);
                paramArray.Add(param.Source);
                var concreteEvent = BootStrapper.BootStrapper.Container.GetExportedTypes(itm.EventType).FirstOrDefault();
                //TODO: Replace MEF with Good IOC container - can't do <,>
                var msg = (ProcessSystemMessage)Activator.CreateInstance(concreteEvent ?? itm.EventType, paramArray.ToArray());
                EventMessageBus.Current.Publish(msg, viewModel.Source);
            };
            return publishMessage;
        }

        public static void Subscribe<TEvent, TViewModel>(TViewModel viewModel, Func<TEvent, bool> eventPredicate,
            IEnumerable<Func<TViewModel, TEvent, bool>> predicate, Action<TViewModel, TEvent> action) where TEvent : IMessage
        {
            EventMessageBus.Current.GetEvent<TEvent>(((IViewModel)viewModel).Source).Where(eventPredicate).Where(x => predicate.All(z => z.Invoke(viewModel, x))).Subscribe(x => action.Invoke(viewModel, x));
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
