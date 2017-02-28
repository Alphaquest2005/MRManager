using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using Akka.Actor;
using Common;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;
using EventMessages.Events;
using RevolutionData;
using RevolutionEntities.Process;
using StartUp.Messages;
using ViewModel.Interfaces;
using Process = RevolutionEntities.Process.Process;


namespace DataServices.Actors
{


    public class ServiceManager : ReceiveActor, IServiceManager
    {

        public ISystemSource Source => new Source(Guid.NewGuid(),"ServiceManager", new SourceType(typeof(IServiceManager)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        


        public ServiceManager(Assembly dbContextAssembly, Assembly entityAssembly, bool autoRun)
        {
            try
            {
                var ctx = Context;
                var machineInfo =
                    MachineInfoData.MachineInfos.FirstOrDefault(
                        x => x.MachineName == Environment.MachineName && x.Processors == Environment.ProcessorCount);
                if (machineInfo == null) return;
                var processInfo = Processes.ProcessInfos.FirstOrDefault(x => x.ParentProcessId == 0);
                if (processInfo == null) return;
                var systemProcess = new SystemProcess(new Process(processInfo, new Agent("System")), machineInfo);
                var systemStartedMsg = new SystemStarted(new StateEventInfo(systemProcess.Id, RevolutionData.Context.Process.Events.ProcessStarted), systemProcess, Source);

                

                EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(Source).Where(x => x.Process.Id == 1)//only start up process
                    .Subscribe(async x =>
                    {
                        var child = Context.Child("ViewModelSupervisor");
                        if (!Equals(child, ActorRefs.Nobody)) return;

                       await Task.Run(() =>ctx.ActorOf(Props.Create<ViewModelSupervisor>(systemProcess, systemStartedMsg),"ViewModelSupervisor")).ConfigureAwait(false);
                       
                        await Task.Run(() => ctx.ActorOf(Props.Create<EntityDataServiceManager>(), "EntityDataServiceManager")).ConfigureAwait(false);
                        await Task.Run(() =>ctx.ActorOf(Props.Create<EntityViewDataServiceManager>(), "EntityViewDataServiceManager")).ConfigureAwait(false);


                        EventMessageBus.Current.Publish(
                            new ServiceStarted<IServiceManager>(this,
                                new StateEventInfo(systemProcess.Id, RevolutionData.Context.Actor.Events.ActorStarted),
                                systemProcess, Source), Source);

                    });

                Task.Run(() =>ctx.ActorOf(Props.Create<ProcessSupervisor>(autoRun, systemStartedMsg),"ProcessSupervisor")).ConfigureAwait(false);
                


            }
            catch (Exception ex)
            {
                Debugger.Break();
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ServiceStarted<IServiceManager>),
                    failedEventMessage: null,
                    expectedEventType: typeof(ServiceStarted<IServiceManager>),
                    exception: ex,
                    source: Source, processInfo: new StateEventInfo(1, RevolutionData.Context.Process.Events.Error)), Source);

            }
        }

        public string UserId => this.Source.SourceName;
    }


}
