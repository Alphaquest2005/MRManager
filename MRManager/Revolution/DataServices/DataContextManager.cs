using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using CommonMessages;
using NHibernate;
using DataInterfaces;
using EFRepository;
using EventAggregator;
using EventMessages;
using NHibernate.Metadata;
using RevolutionEntities.Process;
using StartUp.Messages;

namespace DataServices.Actors
{
    public class DataContextManager : ReceiveActor
    {

        protected SourceMessage SourceMessage
            =>
                new SourceMessage(new MessageSource(this.ToString()),
                    new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        static List<IActorRef> supervisors = new List<IActorRef>();


        public DataContextManager(IDataContext dbContext, Assembly dbContextAssembly, Assembly entityAssembly)
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
                EventMessageBus.Current.GetEvent<ServiceStarted<IProcessService>>(SourceMessage).Where(x => x.Process.Id == 1)//only start up process
                    .Subscribe(x => HandleProcessStarted(dbContext, x.Process, systemStartedMsg));

                var processSup = Context.ActorOf(Props.Create<ProcessSupervisor>(), "ProcessSupervisor");
                processSup.Tell(systemStartedMsg);


            }
            catch (Exception)
            {

                throw;
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

            return;
            var actorList = new Dictionary<string, Type>()
            {
                {"{0}EntityDataServiceSupervisor", typeof (EntityDataServiceSupervisor<>)},
            };

            foreach (var itm in actorList)
            {
                foreach (var c in dbContext.Instance.GetAllClassMetadata())
                {
                    CreateActors(c, itm.Value, itm.Key, dbContext, systemProcess, systemStartedMsg);
                }
            }


            //EventMessageBus.Current.Publish(systemStartedMsg, SourceMessage);
        }

        private void CreateActors(KeyValuePair<string, IClassMetadata> c, Type genericListType, string actorName,
            IDataContext dbContext, ISystemProcess process, IProcessSystemMessage systemStartedmsg)
        {

            var classType = c.Value.GetMappedClass(EntityMode.Poco).GetInterfaces().Last();
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
    }


}
