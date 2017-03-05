using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SystemInterfaces;
using Akka.Actor;
using Akka.IO;
using Akka.Routing;
using Common;
using CommonMessages;
using EventAggregator;
using EventMessages;
using EventMessages.Commands;
using EventMessages.Events;
using RevolutionData;
using RevolutionEntities.Process;
using Utilities;
using ViewMessages;

namespace DataServices.Actors
{
    public class EntityViewDataServiceSupervisor<TEntityView> : BaseSupervisor<EntityViewDataServiceSupervisor<TEntityView>> where TEntityView : IEntityView
        
    {
       

        private static readonly Action<IGetEntityViewById<TEntityView>> GetEntityByIdAction = (x) => x.GetEntity();
        private static readonly Action<IGetEntityViewWithChanges<TEntityView>> GetEntityViewWithChangesAction = (x) => x.GetEntityViewWithChanges();
        private static readonly Action<IUpdateEntityViewWithChanges<TEntityView>> UpdateEntityViewWithChangesAction = (x) => x.UpdateEntityViewWithChanges();
        private static readonly Action<ILoadEntityViewSetWithChanges<TEntityView, IMatchType>> LoadEntityViewSetWithChangesAction = (x) => x.LoadEntityViewSetWithChanges();






        readonly Dictionary<Type, object> entityEvents =
            new Dictionary<Type, object>()
            {
                
                {typeof (IGetEntityViewById<TEntityView>), GetEntityByIdAction},
                {typeof (IGetEntityViewWithChanges<TEntityView>), GetEntityViewWithChangesAction},
                {typeof (IUpdateEntityViewWithChanges<TEntityView>), UpdateEntityViewWithChangesAction},
                {typeof (ILoadEntityViewSetWithChanges<TEntityView, IMatchType>), LoadEntityViewSetWithChangesAction},
                


            };
        private IUntypedActorContext ctx = null;
        public EntityViewDataServiceSupervisor(ISystemProcess process, IProcessSystemMessage msg):base(process)
        {
            
            try
            {
                ctx = Context;
                Parallel.ForEach(entityEvents, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, (itm) =>
                {
                    this.GetType()
                        .GetMethod("CreateEntityViewActor")
                        .MakeGenericMethod(itm.Key)
                        .Invoke(this, new object[] {itm.Value, process, msg});

                });
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void CreateEntityViewActor<TEvent>(object action, ISystemProcess process, IProcessSystemMessage msg) where TEvent : IMessage
        {
            Type actorType = typeof(EntityViewDataServiceActor<>).MakeGenericType(typeof(TEvent));
            var inMsg = new CreateEntityViewService(actorType, action, new StateCommandInfo(process.Id, RevolutionData.Context.Actor.Commands.StartActor), process,Source);
            EventMessageBus.Current.Publish(inMsg, Source);
            /// Create Actor Per Event
            try
            {


                Task.Run(() =>
                {
                    ctx.ActorOf(
                        Props.Create(inMsg.ActorType, inMsg, msg)
                            .WithRouter(new RoundRobinPool(1,
                                new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1,
                                    Environment.ProcessorCount))),
                        "EntityViewDataServiceActor-" +
                        typeof (TEvent).GetFriendlyName().Replace("<", "'").Replace(">", "'"));
                });

                //_childActor.Tell(msg);
            }
            catch (Exception ex)
            {
                Debugger.Break();
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: inMsg.GetType(),
                        failedEventMessage: inMsg,
                        expectedEventType: typeof(ServiceStarted<>),
                        exception: ex,
                        source: Source, processInfo: new StateEventInfo(process.Id, RevolutionData.Context.Process.Events.Error)), Source);
            }
            
        }

        private IActorRef _childActor;
        

      

    }


}