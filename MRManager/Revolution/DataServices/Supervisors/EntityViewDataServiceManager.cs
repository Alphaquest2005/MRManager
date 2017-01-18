using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
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
    public class EntityViewDataServiceManager : BaseSupervisor<EntityViewDataServiceManager>
    {
        public EntityViewDataServiceManager()
        {
            EventMessageBus.Current.GetEvent<IProcessSystemMessage>(Source).Where(x => x is IEntityViewRequest).Subscribe(x => handleEntityRequest((IEntityViewRequest)x));
        }

        private void handleEntityRequest(IEntityViewRequest entityRequest)
        {
            var classType =
                entityRequest.GetType()
                    .GetInterfaces()
                    .FirstOrDefault(x => x.GetInterfaces().Any(z => z.IsGenericType && z.GetGenericTypeDefinition() == typeof(IEntityViewRequest<>))).GenericTypeArguments[0];
            
           // var c = EF7DataContextBase.EntityTypes.FirstOrDefault(x => x.GetInterfaces().Any(z => z.IsGenericType && z.GetGenericTypeDefinition() == typeof(IEntityView<>)) && x.Name.Contains("SignInInfo"));

            CreateEntityViewActors(classType, typeof(EntityViewDataServiceSupervisor<>), "{0}EntityViewDataServiceSupervisor", entityRequest.Process, entityRequest);
            
        }

        private void CreateEntityViewActors(Type classType, Type genericListType, string actorName, ISystemProcess process, IProcessSystemMessage msg)
        {

           // var classType = c.GetInterfaces().FirstOrDefault(x => x.Name.Contains(c.Name.Substring(c.Name.LastIndexOf('.') + 1)));
            var specificListType = genericListType.MakeGenericType(classType);
            try
            {
                Context.ActorOf(Props.Create(specificListType, process, msg), string.Format(actorName, classType.Name));
                
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