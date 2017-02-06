﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reactive.Linq;
using SystemInterfaces;
using Actor.Interfaces;
using Akka.Actor;
using CommonMessages;
using EventAggregator;
using EventMessages;
using EventMessages.Commands;
using EventMessages.Events;
using RevolutionData;
using RevolutionEntities.Process;
using Utilities;
using Process = RevolutionEntities.Process.Process;

namespace DataServices.Actors
{
    public class ProcessSupervisor : BaseSupervisor<ProcessSupervisor>
    {
        private IUntypedActorContext ctx = null;

        //TODO: Track Actor Shutdown instead of just broadcast

        public ProcessSupervisor(bool autoRun)
        {
          ctx = Context;
            
        EventMessageBus.Current.GetEvent<IStartSystemProcess>(Source).Where(x => autoRun && x.ProcessToBeStartedId == Processes.NullProcess).Subscribe(x => StartParentProcess(x.Process.Id, x.User));
            EventMessageBus.Current.GetEvent<IStartSystemProcess>(Source).Where(x => !autoRun && x.ProcessToBeStartedId != Processes.NullProcess).Subscribe(x => StartProcess(x.ProcessToBeStartedId,x.User));
            Receive<ISystemStarted>(x => StartProcess(x.Process.Id,x.User ));
        }

     

        private void StartParentProcess(int processId, IUser user)
        {
            var processSteps = Processes.ProcessInfos.Where(x => x.ParentProcessId == processId);
            CreateProcesses(user, processSteps);
        }
        
        private void StartProcess(int processId, IUser user)
        {
            var processSteps = Processes.ProcessInfos.Where(x => x.Id == processId);
            CreateProcesses(user, processSteps);
        }

        private void CreateProcesses(IUser user, IEnumerable<IProcessInfo> processSteps)
        {
            foreach (var inMsg in processSteps.Select(p => new CreateProcessActor(new StateCommandInfo(p.Id, RevolutionData.Context.Actor.Commands.CreateActor), new SystemProcess(new Process(p.Id, p.ParentProcessId, p.Name, p.Description, p.Symbol, user), Source.MachineInfo),Source)))
            {

                try
                {
                    var actorName = "ProcessActor-" + inMsg.Process.Name.GetSafeActorName();
                    if (!Equals(ctx.Child(actorName), ActorRefs.Nobody)) return;
                    EventMessageBus.Current.Publish(inMsg, Source);
                    if(Processes.ProcessComplexEvents.All(x => x.ProcessId != inMsg.Process.Id)) throw new ApplicationException($"No Complex Events were created for this process:{inMsg.Process.Id}-{inMsg.Process.Name}");
                    

                    var childActor = ctx.ActorOf(Props.Create<ProcessActor>(inMsg), actorName);
                    EventMessageBus.Current.GetEvent<IProcessSystemMessage>(Source)
                        .Where(x => x.Process.Id == inMsg.Process.Id && x.MachineInfo.MachineName == inMsg.MachineInfo.MachineName)
                        .Subscribe(x => childActor.Tell(x));

                }
                catch (Exception ex)
                {
                    Debugger.Break();
                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: inMsg.GetType(),
                                                                        failedEventMessage: inMsg,
                                                                        expectedEventType: typeof(SystemProcessStarted),
                                                                        exception: ex,
                                                                        source: Source, processInfo: new StateEventInfo(inMsg.Process.Id, RevolutionData.Context.Process.Events.Error)), Source);
                }
                
            }
        }
    }


}