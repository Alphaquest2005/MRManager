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
using NHibernate;
using DataInterfaces;
using EFRepository;
using EventAggregator;
using EventMessages;
using NHibernate.Metadata;
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


        public ServiceManager(IDataContext dbContext, Assembly dbContextAssembly, Assembly entityAssembly)
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
                EF7DataContextBase.Initialize(dbContextAssembly, entityAssembly);

                 var processSup = Context.ActorOf(Props.Create<ProcessSupervisor>(), "ProcessSupervisor");
                processSup.Tell(systemStartedMsg);
                
                EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(SourceMessage).Where(x => x.Process.Id == 1)//only start up process
                    .Subscribe(x =>
                    {
                        EventMessageBus.Current.Publish(new ServiceStarted<IServiceManager>(this,systemProcess,SourceMessage), SourceMessage);
                        HandleProcessStarted(dbContext, x.Process, systemStartedMsg);
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

        private void HandleProcessStarted(IDataContext dbContext, ISystemProcess systemProcess, SystemStarted systemStartedMsg)
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
            };

            foreach (var itm in actorList)
            {
                foreach (var c in dbContext.Instance.GetAllClassMetadata().Where(x => x.Value.EntityName.Contains("UserSignIn")))
                {
                    CreateActors(c, itm.Value, itm.Key, dbContext, systemProcess, systemStartedMsg);
                }
            }


            //EventMessageBus.Current.Publish(systemStartedMsg, SourceMessage);
        }

        private void CreateActors(KeyValuePair<string, IClassMetadata> c, Type genericListType, string actorName,
            IDataContext dbContext, ISystemProcess process, IProcessSystemMessage systemStartedmsg)
        {

            var classType = c.Value.GetMappedClass(EntityMode.Poco).GetInterfaces().FirstOrDefault(x => x.Name.Contains(c.Value.EntityName.Substring(c.Value.EntityName.LastIndexOf('.')+1)));
            var specificListType = genericListType.MakeGenericType(classType);
            try
            {
                supervisors.Add(Context.ActorOf(Props.Create(specificListType, dbContext, process),
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

        public string UserId => this.SourceMessage.Source.Source;
    }


}
