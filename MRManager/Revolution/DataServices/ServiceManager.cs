using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
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

        public ISystemSource Source
            =>
                new Source(Guid.NewGuid(),"ServiceManager", new SourceType(typeof(IServiceManager)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        


        public ServiceManager(Assembly dbContextAssembly, Assembly entityAssembly, bool autoRun)
        {
            try
            {
                
                var machineInfo =
                    MachineInfoData.MachineInfos.FirstOrDefault(
                        x => x.MachineName == Environment.MachineName && x.Processors == Environment.ProcessorCount);
                if (machineInfo == null) return;
                var processInfo = Processes.ProcessInfos.FirstOrDefault(x => x.ParentProcessId == 0);
                if (processInfo == null) return;
                var systemProcess = new SystemProcess(new Process(processInfo, new Agent("System")), machineInfo);
                var systemStartedMsg = new SystemStarted(new StateEventInfo(systemProcess.Id, RevolutionData.Context.Process.Events.ProcessStarted), systemProcess, Source);

                

                EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(Source).Where(x => x.Process.Id == 1)//only start up process
                    .Subscribe(x =>
                    {
                        var child = Context.Child("ViewModelSupervisor");
                        if (!Equals(child, ActorRefs.Nobody)) return;



                        var viewsup = Context.ActorOf(Props.Create<ViewModelSupervisor>(systemProcess),
                            "ViewModelSupervisor");

                        EventMessageBus.Current.GetEvent<IServiceStarted<IViewModelSupervisor>>(Source)
                            .Subscribe(q =>
                            {
                                viewsup.Tell(systemStartedMsg);
                            });
                            

                        EventMessageBus.Current.Publish(new ServiceStarted<IServiceManager>(this,new StateEventInfo(systemProcess.Id, RevolutionData.Context.Actor.Events.ActorStarted), systemProcess,Source), Source);
                        
                    });

                Context.ActorOf(Props.Create<ProcessSupervisor>(autoRun), "ProcessSupervisor").Tell(systemStartedMsg);
                Context.ActorOf(Props.Create<EntityDataServiceManager>(), "EntityDataServiceManager").Tell(systemStartedMsg);
                Context.ActorOf(Props.Create<EntityViewDataServiceManager>(), "EntityViewDataServiceManager").Tell(systemStartedMsg);


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
