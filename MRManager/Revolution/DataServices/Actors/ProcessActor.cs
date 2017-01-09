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
using RevolutionData;
using RevolutionEntities.Process;
using StartUp.Messages;
using Utilities;
using IProcessService = Actor.Interfaces.IProcessService;

namespace DataServices.Actors
{


    public class ProcessActor : BaseActor<ProcessActor>, IProcessService
    {
        public ISystemProcess Process { get; }
        
       
        private readonly List<IProcessSystemMessage> msgQue = new List<IProcessSystemMessage>(); 
        private readonly IEnumerable<IComplexEvent> _complexEvents = new List<IComplexEvent>();
        public ConcurrentDictionary<Type, IProcessStateMessage<IEntityId>> ProcessStateMessages { get; }= new ConcurrentDictionary<Type, IProcessStateMessage<IEntityId>>();  
       

        public ProcessActor(ISystemProcess process)
        {
            Process = process;
            Command<IProcessSystemMessage>(z => HandleProcessEvents(z));
            if(Processes.ProcessComplexEvents.Any(x => x.ProcessId == process.Id)) _complexEvents = Processes.ProcessComplexEvents.Where(x => x.ProcessId == process.Id);
            // start actor for each complex event
            StartActors(_complexEvents);

            EventMessageBus.Current.Publish(new ServiceStarted<IProcessService>(this,process, Source), Source);

            // start actor for each complex event

        }

        private void StartActors(IEnumerable<IComplexEvent> complexEvents)
        {
            foreach (var cp in complexEvents)
            {
                try
                {
                    var childActor = Context.ActorOf(Props.Create<ComplexEventActor>(cp), "ComplexEventActor:-" + cp.Key.GetSafeActorName());
                    EventMessageBus.Current.GetEvent<IProcessSystemMessage>(Source)
                        .Where(x => x.Process.Id == Process.Id && x.MachineInfo.MachineName == Process.MachineInfo.MachineName)
                        .Subscribe(x => childActor.Tell(x));
                    
                }
                catch (Exception ex)
                {
                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ICreateComplexEventActor),
                                                                        failedEventMessage: pe,
                                                                        expectedEventType: typeof(SystemProcessStarted),
                                                                        exception: ex,
                                                                        source: Source), Source);
                }
            }
        }

        private void HandleProcessEvents(IProcessSystemMessage pe)
        {
            // Log the message 
            //TODO: Reenable event log
            //Persist(pe, x => { });//(x) => msgQue.Add(x)
            
            // send out Process State Events

            msgQue.Add(pe);
            //_complexEvents.Where(cp => cp.Events.Any(e => pe.GetType().GetInterfaces().Any(z => z == e.EventType) 
            //                                              && e.EventPredicate.Invoke(pe)))
            //              .ForEach(x => x.Execute(new ComplexEventParameters(this,msgQue,pe)));
        }


        public IActorRef ActorRef => this.Self;
        
    }

 


}