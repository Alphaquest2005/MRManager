﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using CommonMessages;
using EventAggregator;
using EventMessages;
using RevolutionData;
using RevolutionEntities.Process;
using Utilities;

namespace DataServices.Actors
{
    public class ProcessSupervisor : BaseSupervisor<ProcessSupervisor>
    {
        

        public ProcessSupervisor()
        {
            EventMessageBus.Current.GetEvent<ISystemProcessCompleted>(Source).Subscribe(x => HandleProcessCompleted(x));
            Receive<ISystemStarted>(x => HandleProcessViews(x));
        }

        private void HandleProcessCompleted(ISystemProcessCompleted se)
        {
            var processSteps = Processes.ProcessInfos.Where(x => x.ParentProcessId == se.Process.Id);
            CreateProcesses(se, processSteps);
        }
        private void HandleProcessViews(ISystemStarted se)
        {
            var processSteps = Processes.ProcessInfos.Where(x => x.Id == se.Process.Id);
            CreateProcesses(se, processSteps);
        }

        private void CreateProcesses(IProcessSystemMessage se, IEnumerable<IProcessInfo> processSteps)
        {
            foreach (var pe in processSteps.Select(p => new SystemProcessStarted(new SystemProcess(new Process(p.Id, p.ParentProcessId, p.Name, p.Description, p.Symbol, se.User), Source.MachineInfo),Source)))
            {
                try
                {
                    var childActor = Context.ActorOf(Props.Create<ProcessActor>(pe.Process), "ProcessActor-" + pe.Process.Name.GetSafeActorName());
                    EventMessageBus.Current.GetEvent<IProcessSystemMessage>(Source)
                        .Where(x => x.Process.Id == pe.Process.Id && x.MachineInfo.MachineName == pe.MachineInfo.MachineName)
                        .Subscribe(x => childActor.Tell(x));
                    EventMessageBus.Current.Publish(pe, Source);
                }
                catch (Exception ex)
                {
                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: pe.GetType(),
                                                                        failedEventMessage: pe,
                                                                        expectedEventType: typeof(SystemProcessStarted),
                                                                        exception: ex,
                                                                        source: Source), Source);
                }
                
            }
        }
    }

}