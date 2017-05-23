using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SystemInterfaces;
using Akka.Actor;
using Akka.Event;
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
    public class EntityDataServiceManager : BaseSupervisor<EntityDataServiceManager>
    {
       
        private IUntypedActorContext ctx = null;
        public EntityDataServiceManager(ISystemProcess process) : base(process)
        {
            ctx = Context;
            EventMessageBus.Current.GetEvent<IProcessSystemMessage>(Source).Where(x => x is IEntityRequest).Subscribe(x => handleEntityRequest((IEntityRequest)x));
        }

        private void handleEntityRequest(IEntityRequest entityRequest)
        {
            try
            {
                var type = entityRequest.GetType()
                    .GetInterfaces()
                    .FirstOrDefault(
                        x =>
                            x.GetInterfaces()
                                .Any(
                                    z =>
                                        z.IsGenericType && z.GetGenericTypeDefinition() == typeof (IEntityRequest<>)));

                Type classType;
                if (type != null)
                {
                    classType = type.GenericTypeArguments[0];
                }
                else
                {
                    classType = entityRequest.ViewType;
                }


                CreateEntityActors(classType, typeof (EntityDataServiceSupervisor<>), "{0}EntityDataServiceSupervisor",
                        entityRequest.Process, entityRequest);
                
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void CreateEntityActors(Type classType, Type genericListType, string actorName, ISystemProcess process, IProcessSystemMessage msg)
        {
            var child = ctx.Child(string.Format(actorName, classType.Name));
            if (!Equals(child, ActorRefs.Nobody)) return;
            var specificListType = genericListType.MakeGenericType(classType);
            try
            {
                Task.Run(() => { ctx.ActorOf(Props.Create(specificListType, process, msg), string.Format(actorName, classType.Name)); });
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