using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using Akka.IO;
using Akka.Routing;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;
using RevolutionEntities.Process;
using Utilities;
using ViewMessages;

namespace DataServices.Actors
{
    public class EntityDataServiceManager : BaseSupervisor<EntityDataServiceManager>
    {
        private IUntypedActorContext ctx = null;
        public EntityDataServiceManager()
        {
            ctx = Context;
            EventMessageBus.Current.GetEvent<IProcessSystemMessage>(Source).Where(x => x is IEntityRequest).Subscribe(x => handleEntityRequest((IEntityRequest)x));
        }

        private void handleEntityRequest(IEntityRequest entityRequest)
        {
            var classType =
               entityRequest.GetType()
                   .GetInterfaces()
                   .FirstOrDefault(x => x.GetInterfaces().Any(z => z.IsGenericType && z.GetGenericTypeDefinition() == typeof(IEntityRequest<>))).GenericTypeArguments[0];

          
                    CreateEntityActors(classType, typeof (EntityDataServiceSupervisor<>), "{0}EntityDataServiceSupervisor", entityRequest.Process, entityRequest);
            
        }

        private void CreateEntityActors(Type classType, Type genericListType, string actorName, ISystemProcess process, IProcessSystemMessage msg)
        {
            
            var specificListType = genericListType.MakeGenericType(classType);
            try
            {
                ctx.ActorOf(Props.Create(specificListType, process, msg), string.Format(actorName, classType.Name));
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                    failedEventMessage: msg,
                    expectedEventType: typeof(ServiceStarted<>).MakeGenericType(specificListType),
                    exception: ex,
                    source: Source, processInfo: new StateEventInfo(msg.Process.Id, RevolutionData.Context.Process.Events.Error)), Source);
            }

        }
    }

}