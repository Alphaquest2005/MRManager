using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web.Script.Serialization;
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
using Utilities;
using IProcessService = Actor.Interfaces.IProcessService;
using DataServices.Utils;

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
            //Todo: make time out configurable
            this.WhenAny(x => x.InMessages, x => x.Value.Count).Timeout(TimeSpan.FromSeconds((double) EventTimeOut.LongWait)).Subscribe(x => ExecuteAction(),z => OnTimeOut());
            EventMessageBus.Current.GetEvent<IRequestComplexEventLog>(Source).Subscribe(x => handleComplexEventLogRequest());

        }

        private void handleComplexEventLogRequest()
        {
            var xlogs = ComplexEventAction.Events.CreatEventLogs(InMessages.ToDictionary(x => x.Key, x => x.Value), Source);
            var ologs = OutMessages.CreatEventLogs(Source);
            var res = new List<IComplexEventLog>();
            res.AddRange(xlogs);
            res.AddRange(ologs);

            var msg = new ComplexEventLogCreated(res,new StateEventInfo(Process.Id, StateEvents.LogCreated), Process, Source);
            Publish(msg);

        }

        private void OnTimeOut()
        {
            if (ComplexEventAction.Events.All(z => z.Raised())) return;
                //Create Timeout Message
                var timeoutMsg = new ComplexEventActionTimedOut(ComplexEventAction,new StateEventInfo(Process.Id, StateEvents.TimeOut), Process, Source);
                PublishProcesError(timeoutMsg, new ApplicationException($"ComplexEventActionTimedOut:<{ComplexEventAction.ProcessInfo.State.Name}>"), ComplexEventAction.ExpectedMessageType);
            // publish message
        }



        public void WireEvents<TEvent>(TEvent eventType, IProcessExpectedEvent expectedEvent) where TEvent : IProcessSystemMessage
        {
            EventMessageBus.Current.GetEvent<TEvent>(Source).Subscribe(x => CheckEvent(expectedEvent,x));
        }

        private void CheckEvent(IProcessExpectedEvent expectedEvent, IProcessSystemMessage message)
        {
           if(!expectedEvent.EventPredicate.Invoke(message)) return;
            expectedEvent.Validate(message);
            InMessages.AddOrUpdate(expectedEvent.Key, message, (k,v) => message);
            
        }

        private void ExecuteAction()
        {
            if (!ComplexEventAction.Events.All(z => z.Raised())) return;
            var inMsg = new ExecuteComplexEventAction(ComplexEventAction.Action, new ComplexEventParameters(this, InMessages.ToDictionary(x => x.Key, x => x.Value)),new StateCommandInfo(Process.Id, StateCommands.CreateAction), Process, Source);
            Publish(inMsg);
            try
            {
                var outMsg = ComplexEventAction.Action.Action.Invoke(inMsg.ComplexEventParameters);
                Publish(outMsg);
            }
            catch (Exception ex)
            {
                PublishProcesError(inMsg, ex, ComplexEventAction.ExpectedMessageType);
            }
           
        }


        public IComplexEventAction ComplexEventAction { get; }
        public ISystemProcess Process { get;  }
        private readonly ConcurrentDictionary<string, IProcessSystemMessage> InMessages = new ConcurrentDictionary<string, IProcessSystemMessage>(); 

    }


}