using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using Akka.Actor;
using EventAggregator;
using EventMessages.Commands;
using EventMessages.Events;
using RevolutionData;
using RevolutionEntities.Process;
using Utilities;


namespace DataServices.Actors
{
    public class ProcessSupervisor : BaseSupervisor<ProcessSupervisor>
    {
        private IUntypedActorContext ctx = null;

        //TODO: Track Actor Shutdown instead of just broadcast

        public ProcessSupervisor(bool autoRun, ISystemStarted firstMsg, List<IProcessInfo> processInfos, List<IComplexEventAction> processComplexEvents) : base(firstMsg.Process)
        {

            ctx = Context;
            ProcessInfos = processInfos;
            ProcessComplexEvents = processComplexEvents;
            EventMessageBus.Current.GetEvent<IStartSystemProcess>(Source).Where(x => autoRun && x.ProcessToBeStartedId == ProcessActions.NullProcess).Subscribe(x => StartParentProcess(x.Process.Id, x.User));
            EventMessageBus.Current.GetEvent<IStartSystemProcess>(Source).Where(x => !autoRun && x.ProcessToBeStartedId != ProcessActions.NullProcess).Subscribe(x => StartProcess(x.ProcessToBeStartedId,x.User));
            StartProcess(firstMsg.Process.Id,firstMsg.User);
        }

     

        private void StartParentProcess(int processId, IUser user)
        {
            var processSteps = ProcessInfos.Where(x => x.ParentProcessId == processId);
            CreateProcesses(user, processSteps, processSteps.First().Id);
        }

        public List<IProcessInfo> ProcessInfos { get; }

        private void StartProcess(int processId, IUser user)
        {
            var processSteps = ProcessInfos.Where(x => x.Id == processId);
            CreateProcesses(user, processSteps, processId);
        }

        static ConcurrentDictionary<string,string> existingProcessActors = new ConcurrentDictionary<string, string>();

        private void CreateProcesses(IUser user, IEnumerable<IProcessInfo> processSteps, int processId)
        {
            Parallel.ForEach(
                processSteps.Select(
                    p =>
                        new CreateProcessActor(ProcessComplexEvents.Where(x => x.ProcessId == processId).ToList(),
                            new StateCommandInfo(p.Id, RevolutionData.Context.Actor.Commands.CreateActor),
                            new SystemProcess(
                                new RevolutionEntities.Process.Process(p.Id, p.ParentProcessId, p.Name, p.Description, p.Symbol, user),
                                Source.MachineInfo), Source)), new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount },
                (inMsg) =>
                {

                    try
                    {
                        var actorName = "ProcessActor-" + inMsg.Process.Name.GetSafeActorName();
                        if (!existingProcessActors.TryAdd(actorName, actorName)) return;


                        EventMessageBus.Current.Publish(inMsg, Source);
                        if (ProcessComplexEvents.All(x => x.ProcessId != inMsg.Process.Id))
                            throw new ApplicationException(
                                $"No Complex Events were created for this process:{inMsg.Process.Id}-{inMsg.Process.Name}");


                        Task.Run(() => { ctx.ActorOf(Props.Create<ProcessActor>(inMsg), actorName); })
                            .ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {
                        Debugger.Break();
                        EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: inMsg.GetType(),
                            failedEventMessage: inMsg,
                            expectedEventType: typeof (SystemProcessStarted),
                            exception: ex,
                            source: Source,
                            processInfo:
                                new StateEventInfo(inMsg.Process.Id, RevolutionData.Context.Process.Events.Error)),
                            Source);
                    }

                });
        }

        public List<IComplexEventAction> ProcessComplexEvents { get; }
    }


}