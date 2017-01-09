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
        private readonly IEnumerable<IComplexEventAction> _complexEvents = new List<IComplexEventAction>();
        public ConcurrentDictionary<Type, IProcessStateMessage<IEntityId>> ProcessStateMessages { get; }= new ConcurrentDictionary<Type, IProcessStateMessage<IEntityId>>();

        private Func<ICreateComplexEventService, IProcessSystemMessage> CreateComplexEventService = inMsg =>
        {
            var childActor =
                Context.ActorOf(
                    Props.Create<ComplexEventActor>(inMsg.ComplexEventService.ComplexEventAction,
                        inMsg.ComplexEventService.Process),
                    "ComplexEventActor:-" + inMsg.ComplexEventService.ComplexEventAction.Key.GetSafeActorName());
            EventMessageBus.Current.GetEvent<IProcessSystemMessage>(inMsg.Source)
                .Where(
                    x =>
                        x.Process.Id == inMsg.Process.Id &&
                        x.MachineInfo.MachineName == inMsg.Process.MachineInfo.MachineName)
                .Subscribe(x => childActor.Tell(x));
            return new ComplexEventServiceCreated(inMsg.ComplexEventService, inMsg.Process, inMsg.Source);
        };

        public ProcessActor(ISystemProcess process)
        {
            Process = process;
            Command<IProcessSystemMessage>(z => HandleProcessEvents(z));
            if (Processes.ProcessComplexEvents.Any(x => x.ProcessId == process.Id))
            {
               
                _complexEvents = Processes.ProcessComplexEvents.Where(x => x.ProcessId == process.Id);
                StartActors(_complexEvents);
            }
            // start actor for each complex event
            
            Publish(new ServiceStarted<IProcessService>(this,process, Source));
            

            // start actor for each complex event

        }

        private void StartActors(IEnumerable<IComplexEventAction> complexEvents)
        {
            foreach (var cp in complexEvents)
            {
                var inMsg = new CreateComplexEventService(new ComplexEventService(cp, Process, Source), Process, Source);
                try
                {
                    var outMsg = CreateComplexEventService.Invoke(inMsg);
                    Publish(outMsg);


                }
                catch (Exception ex)
                {
                   PublishProcesError(inMsg, ex, typeof(IComplexEventServiceCreated));
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
            if(pe.GetType().GetGenericTypeDefinition() == typeof(IProcessStateMessage<>))
            {
                ProcessStateMessages.AddOrUpdate(pe.GetType(), (IProcessStateMessage<IEntityId>) pe, (k, v) => (IProcessStateMessage<IEntityId>) pe);
            }
            //_complexEvents.Where(cp => cp.Events.Any(e => pe.GetType().GetInterfaces().Any(z => z == e.EventType) 
            //                                              && e.EventPredicate.Invoke(pe)))
            //              .ForEach(x => x.Execute(new ComplexEventParameters(this,msgQue,pe)));
        }


        public IActorRef ActorRef => this.Self;
        
    }


}