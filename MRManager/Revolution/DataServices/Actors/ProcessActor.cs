using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using CommonMessages;
using EventAggregator;
using RevolutionEntities.Process;
using StartUp.Messages;

namespace DataServices.Actors
{
    public class ProcessActor : BaseActor<ProcessActor> 
    {
        public ISystemProcess Process { get; }
        public ISystemMessage Msg { get; set; }
        private readonly List<ProcessSystemMessage> msgQue = new List<ProcessSystemMessage>(); 
        private readonly ImmutableList<ProcessExpectedEvent> _expectedEvents; 

        public ProcessActor(ISystemProcess process, ISystemMessage msg)
        {
            Process = process;
            Msg = msg;
            Command<ProcessSystemMessage>(z => HandleProcessEvents(z));
            _expectedEvents = Processes.ExpectedEvents.Where(x => x.ProcessId == process.Id).ToImmutableList();
            EventMessageBus.Current.Publish(new ServiceStarted<IProcessService>(process, msg), MsgSource);
        }

        private void HandleProcessEvents(ProcessSystemMessage pe)
        {
            // Log the message 
            Persist(pe, (x) => msgQue.Add(x));
           //msgQue.Add(pe);
            if (pe.GetType() == typeof (SystemProcessCompleted)) Self.GracefulStop(TimeSpan.FromSeconds(10));
            ProcessEvents();
        }

        private void ProcessEvents()
        {
            // raise events like Process Complete -> go to Next Step
            
            var success = true;
            foreach (var expectedEvent in _expectedEvents)
            {
                //get from 
                var events = msgQue.Where(x => x.GetType() == expectedEvent.EventType).ToList();
                if (!events.Any())
                {
                    success = false;
                    break;
                }
                else
                {
                    if (events.Any(x => expectedEvent.EventPredicate.Invoke(x) != true))
                    {
                        success = false;
                        break;
                    }
                    else
                    {
                        // raise Complex Event
                    }
                }
            }

            if(success) EventMessageBus.Current.Publish(new SystemProcessCompleted(Process, Msg), MsgSource);
        }
        protected override void OnPersistRejected(Exception cause, object @event, long sequenceNr)
        {
            base.OnPersistRejected(cause, @event, sequenceNr);
            Debugger.Break();
        }

        protected override void OnPersistFailure(Exception cause, object @event, long sequenceNr)
        {
            base.OnPersistFailure(cause, @event, sequenceNr);
            Debugger.Break();
        }

    }




}