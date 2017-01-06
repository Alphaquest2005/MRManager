using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using Akka.Actor;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;
using RevolutionData;
using RevolutionEntities.Process;
using StartUp.Messages;

namespace DataServices.Actors
{


    public class ServiceManager : ReceiveActor, IServiceManager
    {

        public ISourceMessage SourceMessage
            =>
                new SourceMessage(new MessageSource(this.ToString()),
                    new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        static List<IActorRef> supervisors = new List<IActorRef>();


        public ServiceManager(Assembly dbContextAssembly, Assembly entityAssembly)
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
                var systemStartedMsg = new SystemStarted(systemProcess, SourceMessage);
               
                 var processSup = Context.ActorOf(Props.Create<ProcessSupervisor>(), "ProcessSupervisor");
                processSup.Tell(systemStartedMsg);
                
                EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(SourceMessage).Where(x => x.Process.Id == 1)//only start up process
                    .Subscribe(x =>
                    {
                        EventMessageBus.Current.Publish(new ServiceStarted<IServiceManager>(this,systemProcess,SourceMessage), SourceMessage);
                        HandleProcessStarted(x.Process, systemStartedMsg);
                    });
                
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ServiceStarted<IServiceManager>),
                    failedEventMessage: null,
                    expectedEventType: typeof(ServiceStarted<IServiceManager>),
                    exception: ex,
                    SourceMsg: SourceMessage), SourceMessage);

            }
        }

        private void HandleProcessStarted(ISystemProcess systemProcess, SystemStarted systemStartedMsg)
        {
            var child = Context.Child("ViewModelSupervisor");
            if (!Equals(child, ActorRefs.Nobody)) return;

            supervisors.Add(Context.ActorOf(Props.Create<ViewModelSupervisor>(systemProcess),
                "ViewModelSupervisor"));
            foreach (var s in supervisors)
            {
                s.Tell(systemStartedMsg);
            }

            
            var actorList = new Dictionary<string, Type>()
            {
                {"{0}EntityDataServiceSupervisor", typeof (EntityDataServiceSupervisor<>)},
                {"{0}EntityViewDataServiceSupervisor", typeof (EntityViewDataServiceSupervisor<>)},
            };

            foreach (var itm in actorList)
            {
                foreach (var c in EF7DataContextBase.EntityTypes.Where(x => x.GetInterfaces().Any(z => z == typeof(IEntity) ) && x.Name.Contains("Persons")))
                {
                    CreateEntityActors(c, itm.Value, itm.Key, systemProcess, systemStartedMsg);
                }
            }

            var viewActorList = new Dictionary<string, Type>()
            {
                {"{0}EntityViewDataServiceSupervisor", typeof (EntityViewDataServiceSupervisor<>)},
            };

            foreach (var itm in viewActorList)
            {
                foreach (var c in EF7DataContextBase.EntityTypes.Where(x => x.GetInterfaces().Any(z => z.IsGenericType && z.GetGenericTypeDefinition() == typeof(IEntityView<>)) && x.Name.Contains("SignInInfo")))
                {
                    CreateEntityViewActors(c, itm.Value, itm.Key, systemProcess, systemStartedMsg);
                }
            }


            //EventMessageBus.Current.Publish(systemStartedMsg, SourceMessage);
        }

        private void CreateEntityActors(Type c, Type genericListType, string actorName,
            ISystemProcess process, IProcessSystemMessage systemStartedmsg)
        {

            var classType = c.GetInterfaces().FirstOrDefault(x => x.Name.Contains(c.Name.Substring(c.Name.LastIndexOf('.')+1)));
            var specificListType = genericListType.MakeGenericType(classType);
            try
            {
                supervisors.Add(Context.ActorOf(Props.Create(specificListType, process),
                    string.Format(actorName, classType.Name)));
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: systemStartedmsg.GetType(),
                    failedEventMessage: systemStartedmsg,
                    expectedEventType: typeof (ServiceStarted<>).MakeGenericType(specificListType),
                    exception: ex,
                    SourceMsg: SourceMessage), SourceMessage);
            }

        }

        private void CreateEntityViewActors(Type c, Type genericListType, string actorName, ISystemProcess process, IProcessSystemMessage systemStartedmsg)
        {

            var classType = c.GetInterfaces().FirstOrDefault(x => x.Name.Contains(c.Name.Substring(c.Name.LastIndexOf('.') + 1)));
            var specificListType = genericListType.MakeGenericType(classType);
            try
            {
                supervisors.Add(Context.ActorOf(Props.Create(specificListType, process),
                    string.Format(actorName, classType.Name)));
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: systemStartedmsg.GetType(),
                    failedEventMessage: systemStartedmsg,
                    expectedEventType: typeof(ServiceStarted<>).MakeGenericType(specificListType),
                    exception: ex,
                    SourceMsg: SourceMessage), SourceMessage);
            }

        }

        public string UserId => this.SourceMessage.Source.Source;
    }


}
