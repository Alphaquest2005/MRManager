using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using CommonMessages;
using EventAggregator;
using EventMessages;
using RevolutionEntities.Process;
using Utilities;

namespace DataServices.Actors
{
    public class ProcessSupervisor : BaseSupervisor<ProcessSupervisor>
    {
        

        public ProcessSupervisor()
        {
            EventMessageBus.Current.GetEvent<SystemProcessCompleted>(MsgSource).Subscribe(x => HandleProcessCompleted(x));
            Receive<SystemStarted>(x => HandleProcessViews(x));
        }

        private void HandleProcessCompleted(ProcessSystemMessage se)
        {
            var processSteps = Processes.ProcessInfos.Where(x => x.ParentProcessId == se.Process.Id);
            CreateProcesses(se, processSteps);
        }
        private void HandleProcessViews(ProcessSystemMessage se)
        {
            var processSteps = Processes.ProcessInfos.Where(x => x.Id == se.Process.Id);
            CreateProcesses(se, processSteps);
        }

        private void CreateProcesses(ProcessSystemMessage se, IEnumerable<IProcessInfo> processSteps)
        {
            foreach (var pe in processSteps.Select(p => new SystemProcessStarted(new SystemProcess(new Process(p.Id, p.ParentProcessId, p.Name, p.Description, p.Symbol, se.User), se.MachineInfo), se)))
            {
                try
                {
                    var childActor = Context.ActorOf(Props.Create<ProcessActor>(pe.Process), "ProcessActor-" + pe.Process.Name.GetSafeActorName());
                    EventMessageBus.Current.GetEvent<ProcessSystemMessage>(MsgSource)
                        .Where(x => x.Process.Id == se.Process.Id && x.MachineInfo.MachineName == pe.MachineInfo.MachineName)
                        .Subscribe(x => childActor.Tell(x));
                    EventMessageBus.Current.Publish(pe, MsgSource);
                }
                catch (Exception ex)
                {
                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: pe.GetType(),
                                                                        failedEventMessage: pe,
                                                                        expectedEventType: typeof(SystemProcessStarted),
                                                                        exception: ex,
                                                                        msg: se), MsgSource);
                }
                
            }
        }
    }

}