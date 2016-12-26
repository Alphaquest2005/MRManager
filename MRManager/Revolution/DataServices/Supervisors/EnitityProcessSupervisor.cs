using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using RevolutionEntities.Process;
using Utilities;

namespace DataServices.Actors
{
    public class EntityProcessSupervisor<TEntity> : BaseSupervisor<EntityProcessSupervisor<TEntity>> where TEntity : IEntity
    {
        

        public EntityProcessSupervisor()
        {
            EventMessageBus.Current.GetEvent<SystemEntityProcessCompleted<TEntity>>(MsgSource).Subscribe(x => HandleProcessCompleted(x));
            Receive<SystemStarted>(x => HandleProcessViews(x));
        }

        private void HandleProcessViews(SystemStarted se)
        {
            var processSteps = Processes.ProcessInfos.OfType<IProcessInfo<TEntity>>().Where(x => x.ParentProcessId == se.Process.Id);
            CreateProcesses(se, processSteps);
        }

        private void HandleProcessCompleted(SystemEntityProcessCompleted<TEntity> se)
        {
            var processSteps = Processes.ProcessInfos.OfType<IProcessInfo<TEntity>>().Where(x => x.ParentProcessId == se.Process.Id);
            CreateProcesses(se, processSteps);
        }


        private void CreateProcesses(IProcessSystemMessage se, IEnumerable<IProcessInfo<TEntity>> processSteps)
        {
            foreach (var pe in processSteps.Select(p => new SystemProcessStarted(new SystemProcess(new Process(p.Id, p.ParentProcessId, p.Name, p.Description, p.Symbol, se.User),se.MachineInfo),se)))
            {
                try
                {
                    var childActor = Context.ActorOf(Props.Create<ProcessActor>(pe.Process), "ProcessActor-" + pe.Process.Name.GetSafeActorName());
                    EventMessageBus.Current.GetEvent<ProcessSystemMessage>(MsgSource)
                        .Where(x => x.Process.Id == pe.Process.Id && x.MachineInfo.MachineName == pe.MachineInfo.MachineName)
                        .Subscribe(x => childActor.Tell(x));
                    EventMessageBus.Current.Publish(pe, MsgSource);
                }
                catch (Exception ex)
                {
                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: pe.GetType(),
                                                                        failedEventMessage: new ProcessSystemMessage(pe.Process,pe),
                                                                        expectedEventType: typeof(SystemProcessStarted),
                                                                        exception: ex,
                                                                        msg: se), MsgSource);
                }
                
            }
        }
    }

}