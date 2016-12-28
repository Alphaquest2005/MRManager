using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using CommonMessages;
using EventAggregator;
using FluentValidation.Resources;
using NHibernate.Util;
using RevolutionEntities.Process;
using StartUp.Messages;

namespace DataServices.Actors
{
    public class ProcessActor : BaseActor<ProcessActor>
    {
        public ISystemProcess Process { get; }
       
        private readonly List<IProcessSystemMessage> msgQue = new List<IProcessSystemMessage>(); 
        private readonly IEnumerable<Processes.EventAction> _complexEvents = new List<Processes.EventAction>();
       

        private static Action<ProcessActor> ExitAction => (x) => x.Self.GracefulStop(TimeSpan.FromSeconds(10));
        List<ProcessExpectedEvent> ExitPredicate = new List<ProcessExpectedEvent>()
            {
                new ProcessExpectedEvent(1, typeof (SystemProcessCompleted), eventPredicate: (e) => e.Id == 1)
            };

        public ProcessActor(ISystemProcess process)
        {
            Process = process;
            Command<IProcessSystemMessage>(z => HandleProcessEvents(z));
            if(Processes.ProcessComplexEvents.Any(x => x.ProcessId == process.Id)) _complexEvents = Processes.ProcessComplexEvents.Where(x => x.ProcessId == process.Id);
            EventMessageBus.Current.Publish(new ServiceStarted<IProcessService>(process, SourceMessage), SourceMessage);
        }

        private void HandleProcessEvents(IProcessSystemMessage pe)
        {
            // Log the message 
            //TODO: Reenable event log
            // Persist(pe, (x) => msgQue.Add(x));

            // send out Process State Events

            msgQue.Add(pe);

            _complexEvents.Where(x => !x.Raised).ForEach(x => {
                                                                 if (!CheckExpectedEvents.Invoke(x.Events, msgQue))return;
                                                                  x.Raised = true;
                                                                  x.Action.Invoke(this);});
        }


        public IActorRef ActorRef()
        {
            return this.Self;
        }
    }




}