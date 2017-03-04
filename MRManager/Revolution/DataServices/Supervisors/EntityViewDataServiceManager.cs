using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SystemInterfaces;
using Akka.Actor;
using Akka.Actor.Dsl;
using Akka.IO;
using Akka.Routing;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;
using EventMessages.Events;
using RevolutionEntities.Process;
using Utilities;
using ViewMessages;

namespace DataServices.Actors
{
    public class EntityViewDataServiceManager : BaseSupervisor<EntityViewDataServiceManager>
    {
        private IUntypedActorContext ctx = null;
        public EntityViewDataServiceManager(ISystemProcess process) : base(process)
        {
            //Hack:for some fuckup reason context is lost
            ctx = Context;
            EventMessageBus.Current.GetEvent<IProcessSystemMessage>(Source).Where(x => x is IEntityViewRequest).Subscribe(x => handleEntityRequest((IEntityViewRequest)x));
           
        }

        private void handleEntityRequest(IEntityViewRequest entityRequest)
        {
            
            var classType =
                entityRequest.GetType()
                    .GetInterfaces()
                    .FirstOrDefault(x => x.GetInterfaces().Any(z => z.IsGenericType && z.GetGenericTypeDefinition() == typeof(IEntityViewRequest<>))).GenericTypeArguments[0];
            
           

            CreateEntityViewActors(classType, typeof(EntityViewDataServiceSupervisor<>), "{0}EntityViewDataServiceSupervisor", entityRequest.Process, entityRequest);
            
        }

        private void CreateEntityViewActors(Type classType, Type genericListType, string actorName, ISystemProcess process, IProcessSystemMessage msg)
        {
            var child = ctx.Child(string.Format(actorName, classType.Name));
            if (!Equals(child, ActorRefs.Nobody)) return;
            
            var specificListType = genericListType.MakeGenericType(classType);
            try
            {
                Task.Run(() => { ctx.ActorOf(Props.Create(specificListType, process, msg), string.Format(actorName, classType.Name)); }).ConfigureAwait(false);
                

            }
            catch (Exception ex)
            {
                Debugger.Break();
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                    failedEventMessage: msg,
                    expectedEventType: typeof(ServiceStarted<>).MakeGenericType(specificListType),
                    exception: ex,
                    source: Source, processInfo: new StateEventInfo(msg.Process.Id, RevolutionData.Context.Process.Events.Error)), Source);
            }

        }
    }

}