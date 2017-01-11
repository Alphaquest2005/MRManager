﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using SystemInterfaces;
using Actor.Interfaces;
using EventAggregator;
using EventMessages;
using ReactiveUI;
using RevolutionData;
using RevolutionEntities.Process;
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

            var msg = new ComplexEventLogCreated(res,new StateEventInfo(Process.Id, RevolutionData.Context.Process.Events.LogCreated), Process, Source);
            Publish(msg);

        }

        private void OnTimeOut()
        {
            if (ComplexEventAction.Events.All(z => z.Raised())) return;
                //Create Timeout Message
                var timeoutMsg = new ComplexEventActionTimedOut(ComplexEventAction,new StateEventInfo(Process.Id, RevolutionData.Context.Process.Events.ProcessTimeOut), Process, Source);
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
            var inMsg = new ExecuteComplexEventAction(ComplexEventAction.Action, new ComplexEventParameters(this, InMessages.ToDictionary(x => x.Key, x => x.Value as object)),new StateCommandInfo(Process.Id, RevolutionData.Context.Actor.Commands.CreateAction), Process, Source);
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