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

        private void HandleProcessCompleted(SystemProcessMessage se)
        {
            var processSteps = Processes.MessageProcesses.Where(x => x.ProcessId == se.Process.Id);
            CreateProcesses(se, processSteps);
        }
        private void HandleProcessViews(SystemProcessMessage se)
        {
            var processSteps = Processes.MessageProcesses.Where(x => x.Id == se.Process.Id);
            CreateProcesses(se, processSteps);
        }

        private void CreateProcesses(SystemProcessMessage se, IEnumerable<IProcess> processSteps)
        {
            foreach (var pe in processSteps.Select(p => new SystemProcessStarted(p, se.MachineInfo,se.Process.User, MsgSource)))
            {
                try
                {
                    var childActor = Context.ActorOf(Props.Create<ProcessActor>(se.Process), "ProcessActor-" + se.Process.Name.GetSafeActorName());
                    EventMessageBus.Current.GetEvent<SystemProcessMessage>(MsgSource)
                        .Where(x => x.Process.Id == se.Process.Id && x.MachineInfo.MachineName == se.Process.MachineInfo.MachineName)
                        .Subscribe(x => childActor.Tell(x));
                    EventMessageBus.Current.Publish(pe, MsgSource);
                }
                catch (Exception ex)
                {
                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: se.GetType(),
                                                                        failedEventMessage: se,
                                                                        expectedEventType: typeof(SystemProcessStarted),
                                                                        exception: ex,
                                                                        msgSource: MsgSource), MsgSource);
                }
                
            }
        }
    }

}