using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using NHibernate;
using DataInterfaces;
using NHibernate.Metadata;

namespace DataServices.Actors
{
   public class DataContextManager : ReceiveActor 
    {
       public DataContextManager(IDataContext dbContext)
       {
           try
           {
               var actorList = new Dictionary<string, Type>()
               {
                   {"Get{0}ByIdSupervisor", typeof (GetEntityByIdSupervisor<>)},
                   {"Update{0}ChangesSupervisor", typeof (UpdateEntityChangesSupervisor<>)},
                   {"Load{0}EntitySetSupervisor", typeof (LoadEntitySetSupervisor<>)},
                   {"Load{0}HeaderInfoSupervisor", typeof (LoadHeaderInfoSupervisor<>)},

               };

               foreach (var itm in actorList)
               {
                   foreach (var c in dbContext.Instance.GetAllClassMetadata())
                   {
                       CreateActors(c, itm.Value, itm.Key,dbContext);
                   }
               }
                // EventMessageBus.Current.Publish(new DataServiceStarted());
               
                    Context.ActorOf(Props.Create(typeof(GetMediaSupervisor), dbContext), "GetMediaSupervisor");
            }
           catch (Exception)
           {

               throw;
           }

       }

       private static void CreateActors(KeyValuePair<string, IClassMetadata> c, Type genericListType, string actorName, IDataContext dbContext)
       {
           var classType = c.Value.GetMappedClass(EntityMode.Poco).GetInterfaces().Last();
           var specificListType = genericListType.MakeGenericType(classType);
          
           Context.ActorOf(Props.Create(specificListType,dbContext), string.Format(actorName, classType.Name));
       }
    }
}
