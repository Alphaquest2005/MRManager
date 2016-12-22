using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using CommonMessages;
using NHibernate;
using DataInterfaces;
using EFRepository;
using EventAggregator;
using NHibernate.Metadata;
using RevolutionEntities.Process;

namespace DataServices.Actors
{
   public class DataContextManager : ReceiveActor 
    {
       protected MessageSource msgSource => new MessageSource(this.ToString());
       static List<IActorRef> supervisors = new List<IActorRef>();
      

       public DataContextManager(IDataContext dbContext, Assembly dbContextAssembly, Assembly entityAssembly)
       {
           try
           {
               
               var machineInfo = MachineInfoData.MachineInfos.FirstOrDefault(x => x.MachineName == Environment.MachineName && x.Processors == Environment.ProcessorCount);
                if (machineInfo == null) return;
               var process = Processes.MessageProcesses.FirstOrDefault(x => x.ProcessId == 0);
               if (process == null) return;
               var systemProcess = new SystemProcess(process, machineInfo, new Agent("System"));
                var systemStartedMsg = new SystemStarted(systemProcess, msgSource);
                EF7DataContextBase.Initialize(dbContextAssembly,entityAssembly);


               var actorList = new Dictionary<string, Type>()
               {
                   {"{0}EntityDataServiceSupervisor", typeof (EntityDataServiceSupervisor<>)},
               };

               foreach (var itm in actorList)
               {
                   foreach (var c in dbContext.Instance.GetAllClassMetadata())
                   {
                       CreateActors(c, itm.Value, itm.Key,dbContext, systemProcess);
                   }
               }

                
                supervisors.Add(Context.ActorOf(Props.Create<ProcessSupervisor>(), "ProcessSupervisor"));
                supervisors.Add(Context.ActorOf(Props.Create<ViewModelSupervisor>(systemProcess), "ViewModelSupervisor"));

               foreach (var s in supervisors)
               {
                   s.Tell(systemStartedMsg);
               }
                
                EventMessageBus.Current.Publish(systemStartedMsg, msgSource);

            }
           catch (Exception)
           {

               throw;
           }

       }

       private static void CreateActors(KeyValuePair<string, IClassMetadata> c, Type genericListType, string actorName, IDataContext dbContext, ISystemProcess process)
       {
           var classType = c.Value.GetMappedClass(EntityMode.Poco).GetInterfaces().Last();
           var specificListType = genericListType.MakeGenericType(classType);
          
          supervisors.Add(Context.ActorOf(Props.Create(specificListType,dbContext, process), string.Format(actorName, classType.Name)));
       }
    }


}
