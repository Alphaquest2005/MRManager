﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using Akka.Actor;
using CommonMessages;
using DataServices.Utils;
using EventAggregator;
using EventMessages;
using MoreLinq;
using RevolutionData;
using RevolutionEntities.Process;
using StartUp.Messages;
using Utilities;
using IProcessService = Actor.Interfaces.IProcessService;
using ProcessStateInfo = RevolutionEntities.Process.ProcessStateInfo;

namespace DataServices.Actors
{


    public class ProcessActor : BaseActor<ProcessActor>, IProcessService
    {
        public ISystemProcess Process { get; }


        private ConcurrentQueue<IProcessSystemMessage> msgQue = new ConcurrentQueue<IProcessSystemMessage>();
        private ReadOnlyCollection<IComplexEventAction> _complexEvents;
        public ConcurrentDictionary<Type, IProcessStateMessage<IEntityId>> ProcessStateMessages { get; }= new ConcurrentDictionary<Type, IProcessStateMessage<IEntityId>>();
        private static IUntypedActorContext ctx = null;


        public ProcessActor(ICreateProcessActor msg)
        {
            ctx = Context;
            Process = msg.Process;
            EventMessageBus.Current.GetEvent<IProcessStateMessage<IEntityId>>(Source)
                .Where(x => x.Process.Id == msg.Process.Id)
                .Subscribe(x => SaveStateMessages(x));
            EventMessageBus.Current.GetEvent<IRequestProcessState>(Source)
                .Where(x => x.Process.Id == msg.Process.Id)
                .Subscribe(x => HandleRequestState(x));
            EventMessageBus.Current.GetEvent<IRequestProcessLog>(Source)
                .Where(x => x.Process.Id == msg.Process.Id)
                .Subscribe(x => HandleProcessLogRequest(x));
            EventMessageBus.Current.GetEvent<IComplexEventLogCreated>(Source)
                .Where(x => x.Process.Id == msg.Process.Id)
                .Subscribe(x => HandleComplexEventLog(x));

            EventMessageBus.Current.GetEvent<IServiceStarted<IComplexEventService>>(Source)
                .Where(x => x.Process.Id == msg.Process.Id)
                .Subscribe(x => NotifyServiceStarted(x));

            Command<IProcessSystemMessage>(z => HandleProcessEvents(z));

            
                _complexEvents =
                    new ReadOnlyCollection<IComplexEventAction>(
                        Processes.ProcessComplexEvents.Where(x => x.ProcessId == msg.Process.Id).ToList());
                StartActors(_complexEvents);
            
        }

        ConcurrentQueue<IServiceStarted<IComplexEventService>> startedComplexEventServices = new ConcurrentQueue<IServiceStarted<IComplexEventService>>();
        private void NotifyServiceStarted(IServiceStarted<IComplexEventService> service)
        {
            startedComplexEventServices.Enqueue(service);
            if (startedComplexEventServices.Count != _complexEvents.Count()) return;
            Publish(new ServiceStarted<IProcessService>(this,new StateEventInfo(Process.Id, RevolutionData.Context.Actor.Events.ActorStarted), Process, Source));
            startedComplexEventServices = new ConcurrentQueue<IServiceStarted<IComplexEventService>>();
        }


        ConcurrentQueue<IComplexEventLogCreated> complexEventLogs = new ConcurrentQueue<IComplexEventLogCreated>();
        private void HandleComplexEventLog(IComplexEventLogCreated complexEventLog)
        {
            complexEventLogs.Enqueue(complexEventLog);
            if (complexEventLogs.Count != _complexEvents.Count()) return;

            var msg = CreateProcessLog();
            Publish(msg);
            complexEventLogs = new ConcurrentQueue<IComplexEventLogCreated>();
        }

        private ProcessLogCreated CreateProcessLog()
        {
           var logs = new List<IComplexEventLog>(msgQue.ToImmutableList().CreatEventLogs(Source));

            var msg = new ProcessLogCreated(logs.OrderBy(x => x.Time),
                new StateEventInfo(Process.Id, RevolutionData.Context.Process.Events.LogCreated), Process, Source);
            return msg;
        }

        private void HandleProcessLogRequest(IRequestProcessLog requestProcessLog)
        {
            //Request logs from ComplexEventActors
           
            var msg = CreateProcessLog();
            Publish(msg);
        }

        private void HandleRequestState(IRequestProcessState requestProcessState)
        {
            foreach (var ps in ProcessStateMessages)
            {
                Publish(ps.Value);
            }
        }

        private void SaveStateMessages(IProcessStateMessage<IEntityId> pe)
        {

            ProcessStateMessages.AddOrUpdate(pe.GetType(), pe, (k, v) => pe);
            var msg = new ProcessStateUpdated(pe.GetType(), pe, new StateEventInfo(Process.Id, RevolutionData.Context.Process.Events.StateUpdated), Process, Source);
            Publish(msg);
        }

        private void StartActors(IEnumerable<IComplexEventAction> complexEvents)
        {
            Contract.Requires(complexEvents.Any() && complexEvents != null);
            foreach (var cp in complexEvents)
            {
                var inMsg = new CreateComplexEventService(new ComplexEventService(cp.Key,cp, Process, Source),new StateCommandInfo(Process.Id, RevolutionData.Context.Actor.Commands.StartActor), Process, Source);
                Publish(inMsg);
                try
                {
                    CreateComplexEventService.Invoke(inMsg);
                }
                catch (Exception ex)
                {
                   PublishProcesError(inMsg, ex, typeof(IServiceStarted<IComplexEventService>));
                }
            }
        }

        private void HandleProcessEvents(IProcessSystemMessage pe)
        {
            // Log the message 
            //TODO: Reenable event log
            //Persist(pe, x => { });//(x) => msgQue.Add(x)
            
            // send out Process State Events

            msgQue.Enqueue(pe);
           
        }


        public IActorRef ActorRef => this.Self;
                private Action<ICreateComplexEventService> CreateComplexEventService = inMsg =>
        {
            try
            {
                var childActor =
                    ctx.ActorOf(
                        Props.Create<ComplexEventActor>(inMsg),
                        "ComplexEventActor:-" + inMsg.ComplexEventService.ActorId.GetSafeActorName());
                EventMessageBus.Current.GetEvent<IProcessSystemMessage>(inMsg.Source)
                    .Where(
                        x =>
                            x.Process.Id == inMsg.Process.Id &&
                            x.MachineInfo.MachineName == inMsg.Process.MachineInfo.MachineName)
                    .Subscribe(x => childActor.Tell(x));
            }
            catch (Exception)
            {

                throw;
            }


        };
    }


}