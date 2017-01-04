using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using Akka.Actor;
using CommonMessages;
using EventAggregator;
using FluentValidation.Resources;
using NHibernate.Intercept;
using NHibernate.Util;
using RevolutionData;
using RevolutionEntities.Process;
using StartUp.Messages;

namespace DataServices.Actors
{


    public class ProcessActor : BaseActor<ProcessActor>, IProcessActor
    {
        public ISystemProcess Process { get; }
        
       
        private readonly List<IProcessSystemMessage> msgQue = new List<IProcessSystemMessage>(); 
        private readonly IEnumerable<IComplexEvent> _complexEvents = new List<IComplexEvent>();
        public ConcurrentDictionary<Type, IProcessStateMessage<IEntity>> ProcessStateMessages { get; }= new ConcurrentDictionary<Type, IProcessStateMessage<IEntity>>();  
       

        public ProcessActor(ISystemProcess process)
        {
            Process = process;
            Command<IProcessSystemMessage>(z => HandleProcessEvents(z));
            if(Processes.ProcessComplexEvents.Any(x => x.ProcessId == process.Id)) _complexEvents = Processes.ProcessComplexEvents.Where(x => x.ProcessId == process.Id);
            EventMessageBus.Current.Publish(new ServiceStarted<IProcessService>(this,process, SourceMessage), SourceMessage);
        }

        private void HandleProcessEvents(IProcessSystemMessage pe)
        {
            // Log the message 
            //TODO: Reenable event log
            //Persist(pe, x => { });//(x) => msgQue.Add(x)

            // send out Process State Events

            msgQue.Add(pe);
            _complexEvents.ForEach(x => x.Execute(new ComplexEventParameters(this,msgQue,pe)));
        }


        public IActorRef ActorRef => this.Self;
        
    }



}