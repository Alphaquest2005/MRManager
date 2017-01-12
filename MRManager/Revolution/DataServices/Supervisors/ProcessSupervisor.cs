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
            foreach (var inMsg in processSteps.Select(p => new StartSystemProcess(new StateCommandInfo(p.Id, RevolutionData.Context.Process.Commands.StartProcess), new SystemProcess(new Process(p.Id, p.ParentProcessId, p.Name, p.Description, p.Symbol, se.User), Source.MachineInfo),Source)))
            {

                try
                {
                    EventMessageBus.Current.Publish(inMsg, Source);

                    var childActor = Context.ActorOf(Props.Create<ProcessActor>(inMsg.Process), "ProcessActor-" + inMsg.Process.Name.GetSafeActorName());
                    EventMessageBus.Current.GetEvent<IProcessSystemMessage>(Source)
                        .Where(x => x.Process.Id == inMsg.Process.Id && x.MachineInfo.MachineName == inMsg.MachineInfo.MachineName)
                        .Subscribe(x => childActor.Tell(x));

                    var outMsg = new SystemProcessStarted(new StateEventInfo(inMsg.Process.Id, RevolutionData.Context.Process.Events.ProcessStarted),inMsg.Process,Source);
                    //actor Won't instantiate fast enough to catch eventbus publish
                    childActor.Tell(outMsg);
                    EventMessageBus.Current.Publish(outMsg, Source);
                }
                catch (Exception ex)
                {
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