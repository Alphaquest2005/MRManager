using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using SystemInterfaces;
using BootStrapper;
using CommonMessages;
using EventAggregator;
using EventMessages;
using EventMessages.Commands;
using EventMessages.Events;
using ReactiveUI;
using RevolutionEntities.Process;
using ViewModel.Interfaces;


namespace Core.Common.UI
{
    public static class ViewModelExtensions
    {
        public static void WireEvents(this IViewModel viewModel)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Parallel.ForEach(viewModel.CommandInfo, new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount}, (itm) =>
            {
                var subject = itm.Subject.Invoke(viewModel);


                if (subject.GetType() == Observable.Empty<ReactiveCommand<IViewModel, Unit>>().GetType())
                {
                    var publishMessage = CreateCommandMessageAction<IViewModel>(viewModel, itm);
                    var cmd = ReactiveCommand.Create(publishMessage);

                    viewModel.Commands.Add(itm.Key, cmd);
                }
                else
                {
                    var publishMessage = CreateCommandMessageAction<dynamic>(viewModel, itm);
                    subject.Where(x => itm.CommandPredicate.All(z => z.Invoke(viewModel)))
                        .Subscribe(publishMessage);
                }
                
            });
            
            Parallel.ForEach(viewModel.EventSubscriptions,
                new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount}, (itm) =>
                {
                    typeof (ViewModelExtensions)
                        .GetMethod("Subscribe")
                        .MakeGenericMethod(itm.EventType, viewModel.GetType())
                        .Invoke(viewModel, new object[] {viewModel, itm.EventPredicate, itm.ActionPredicate, itm.Action});
                });



            Parallel.ForEach(viewModel.EventPublications,
                new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount}, (itm) =>
                {

                    var subject = itm.Subject.Invoke(viewModel);

                    var publishMessage = CreatePublishMessageAction(viewModel, itm);
                    subject.Where(x => itm.SubjectPredicate.All(z => z.Invoke(viewModel)))
                        .Subscribe(publishMessage);
                });

            

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
                var concreteEvent = BootStrapper.BootStrapper.Container.GetExportedTypes(itm.EventType).FirstOrDefault() ?? BootStrapper.BootStrapper.Container.GetExportedType(itm.EventType) ;
                //TODO: Replace MEF with Good IOC container - can't do <,>
                ProcessSystemMessage msg;
                if (concreteEvent == null)
                {
                    msg = new ProcessEventFailure(itm.EventType, new FailedMessageData(itm, param.ProcessInfo, param.Process, param.Source), itm.EventType, new InvalidOperationException($"Type:{itm.EventType.Name} not found in MEF - consider adding export to type."), param.ProcessInfo, viewModel.Source);
                }
                else
                {
                    msg = (ProcessSystemMessage) Activator.CreateInstance(concreteEvent, paramArray.ToArray());
                }
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
                var concreteEvent = BootStrapper.BootStrapper.Container.GetExportedTypes(itm.EventType).FirstOrDefault() ?? BootStrapper.BootStrapper.Container.GetExportedType(itm.EventType);
                //TODO: Replace MEF with Good IOC container - can't do <,>
                ProcessSystemMessage msg;
                if (concreteEvent == null)
                {
                    msg = new ProcessEventFailure(itm.EventType,
                            new FailedCommandData(itm, param.ProcessInfo, param.Process, param.Source), itm.EventType,
                            new InvalidOperationException("Type not found in MEF - consider adding export to type."), new StateEventInfo(param.ProcessInfo.ProcessId,"Error","Error occured getting type","",param.ProcessInfo.State)
                            , viewModel.Source);
                }
                else
                {
                    msg = (ProcessSystemMessage)Activator.CreateInstance(concreteEvent, paramArray.ToArray());
                }
                
                EventMessageBus.Current.Publish(msg, viewModel.Source);
            };
            return publishMessage;
        }

        public static void Subscribe<TEvent, TViewModel>(TViewModel viewModel, Func<TEvent, bool> eventPredicate,
            IEnumerable<Func<TViewModel, TEvent, bool>> predicate, Action<TViewModel, TEvent> action) where TEvent : IProcessSystemMessage
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
