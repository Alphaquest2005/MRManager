using System;
using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using Akka.IO;
using Akka.Routing;
using Common;
using CommonMessages;
using EventAggregator;
using EventMessages;
using RevolutionData;
using RevolutionEntities.Process;
using Utilities;
using ViewMessages;

namespace DataServices.Actors
{
    public class EntityViewDataServiceSupervisor<TEntityView> : ReceiveActor, IProcessSource where TEntityView : IEntityId
//where TEntity : class, IEntity where TEntityView:IEntityView<TEntity>
    {
        public ISystemSource Source => new Source(Guid.NewGuid(), $"EntityViewSupervisor:<{typeof(TEntityView).GetFriendlyName()}>",new SourceType(typeof(EntityViewDataServiceSupervisor<TEntityView>)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        private static readonly Action<IGetEntityViewById<TEntityView>> GetEntityByIdAction = (x) => x.GetEntity();
        private static readonly Action<IGetEntityViewWithChanges<TEntityView>> GetEntityWithChangesAction = (x) => x.GetEntity();

        //private static readonly Action<ISystemSource, ILoadEntitySet<TEntity>> LoadEntitySet = (s, x) => x.LoadEntitySet();
        //private static readonly Action<ISystemSource, ILoadEntitySetWithFilter<TEntity>> LoadEntitySetWithFilter = (s, x) => x.LoadEntitySet();
        //private static readonly Action<ISystemSource, ILoadEntitySetWithFilterWithIncludes<TEntity>> LoadEntitySetWithFilterWithIncludes = (s, x) => x.LoadEntitySet();

        //TODO: Add EntityViews
        //private static readonly Action<ISystemSource, LoadEntityView<TEntity>> LoadEntityView = (s, x) => LoadEntityView(s, x);
        //private static readonly Action<ISystemSource, LoadEntityViewWithFilter<TEntity>> LoadEntityViewWithFilter = (s, x) => x.LoadEntityView(s);

        readonly Dictionary<Type, object> entityEvents =
            new Dictionary<Type, object>()
            {
                
                //{typeof (GetEntityById<TEntity>), GetEntityByIdAction},
                {typeof (IGetEntityViewById<TEntityView>), GetEntityByIdAction},
                {typeof (IGetEntityViewWithChanges<TEntityView>), GetEntityWithChangesAction},

                //{typeof (LoadEntitySet<TEntity>), LoadEntitySet},
                //{typeof (LoadEntitySetWithFilter<TEntity>), LoadEntitySetWithFilter},
                //{typeof (LoadEntitySetWithFilterWithIncludes<TEntity>), LoadEntitySetWithFilterWithIncludes},

                //{typeof (LoadEntityView<TEntity>), LoadEntityView},
                //{typeof (LoadEntityViewWithFilter<TEntity>), LoadEntityViewWithFilter},
            };

        public EntityViewDataServiceSupervisor(ISystemProcess process)
        {
            foreach (var itm in entityEvents)
            {
               this.GetType()
                        .GetMethod("CreateEntityViewActor")
                        .MakeGenericMethod(itm.Key)
                        .Invoke(this, new object[] {itm.Value, process});
               
            }

        }

        public void CreateEntityViewActor<TEvent>(object action, ISystemProcess process) where TEvent : IMessage
        {
            Type actorType = typeof(EntityViewDataServiceActor<>).MakeGenericType(typeof(TEvent));
            var inMsg = new CreateEntityViewService(actorType, action, new StateCommandInfo(process.Id, StateCommands.CreateService, StateEvents.ServiceCreated), process,Source);
            EventMessageBus.Current.Publish(inMsg, Source);
            /// Create Actor Per Event
            try
            {
               
                
                _childActor = Context.ActorOf(Props.Create(inMsg.ActorType, inMsg.Action, inMsg.Process).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))),
                            "EntityViewDataServiceActor-" + typeof(TEvent).GetFriendlyName().Replace("<", "'").Replace(">", "'"));

                    EventMessageBus.Current.GetEvent<TEvent>(Source).Subscribe(x => _childActor.Tell(x));
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: inMsg.GetType(),
                        failedEventMessage: inMsg,
                        expectedEventType: typeof(ServiceStarted<>),
                        exception: ex,
                        source: Source), Source);
            }
            
        }

        private IActorRef _childActor;
        

      

    }


}