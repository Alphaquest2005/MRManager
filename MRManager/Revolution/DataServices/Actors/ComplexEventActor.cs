using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using Akka.Actor;
using CommonMessages;
using EventAggregator;
using EventMessages;
using MoreLinq;
using ReactiveUI;
using RevolutionData;
using RevolutionEntities.Process;
using StartUp.Messages;
using IProcessService = Actor.Interfaces.IProcessService;

namespace DataServices.Actors
{


    public class ComplexEventActor : BaseActor<ComplexEventActor>, IComplexEventService
    {
        public ComplexEventActor(IComplexEventAction complexEventAction, ISystemProcess process)
        {
            ComplexEventAction = complexEventAction;
            Process = process;
            foreach (var e in complexEventAction.Events)
            {
                this.GetType().GetMethod("WireEvents").MakeGenericMethod(e.EventType).Invoke(this, new object[] {e});
            }
            this.WhenAny(x => x.Messages, x => x.Value.Count).Timeout(TimeSpan.FromSeconds(10)).Subscribe(x => 
                                                                { if (ComplexEventAction.Events.All(z => z.Raised()))
                                                                     ExecuteAction();
                                                                },
                                                                z => OnTimeOut());

        }

        private void OnTimeOut()
        {
            //Create Timeout Message
            var timeoutMsg = new ComplexEventActionTimedOut(ComplexEventAction, Process, Source);
            PublishProcesError(timeoutMsg, new ApplicationException($"ComplexEventActionTimedOut:<{ComplexEventAction.ProcessInfo.Status}>"));
            // publish message
        }

        private void PublishProcesError(IProcessSystemMessage msg, Exception ex )
        {
            EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                        failedEventMessage: msg,
                        expectedEventType: ComplexEventAction.ExpectedMessageType,
                        exception: ex,
                        source: Source), Source);
        }

        public void WireEvents<TEvent>(TEvent eventType, IProcessExpectedEvent expectedEvent) where TEvent : IProcessSystemMessage
        {
            EventMessageBus.Current.GetEvent<TEvent>(Source).Subscribe(x => CheckEvent(expectedEvent,x));
        }

        private void CheckEvent(IProcessExpectedEvent expectedEvent, IProcessSystemMessage message)
        {
           if(!expectedEvent.EventPredicate.Invoke(message)) return;
            expectedEvent.Validate(message);
            Messages.AddOrUpdate(expectedEvent.Key, message, (k,v) => message);
            
        }

        private void ExecuteAction()
        {
            ComplexEventAction.Action.Action.Invoke(new ComplexEventParameters(this, Messages.ToDictionary(x => x.Key, x => x.Value )));
        }


        public IComplexEventAction ComplexEventAction { get; }
        public ISystemProcess Process { get;  }
        private readonly ConcurrentDictionary<string, IProcessSystemMessage> Messages = new ConcurrentDictionary<string, IProcessSystemMessage>(); 

    }


}