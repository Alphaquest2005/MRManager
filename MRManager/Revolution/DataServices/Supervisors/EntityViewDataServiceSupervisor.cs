using System;
using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using Akka.IO;
using Akka.Routing;
using CommonMessages;
using EventAggregator;
using EventMessages;
using RevolutionEntities.Process;
using Utilities;
using ViewMessages;

namespace DataServices.Actors
{
    public class EntityViewDataServiceSupervisor<TEntityView> : ReceiveActor where TEntityView : IEntityId
//where TEntity : class, IEntity where TEntityView:IEntityView<TEntity>
    {
        
        private static readonly Action<IGetEntityViewById<TEntityView>> GetEntityByIdAction = (x) => x.GetEntity();
        private static readonly Action<IGetEntityViewWithChanges<TEntityView>> GetEntityWithChangesAction = (x) => x.GetEntity();

        //private static readonly Action<ISourceMessage, ILoadEntitySet<TEntity>> LoadEntitySet = (s, x) => x.LoadEntitySet();
        //private static readonly Action<ISourceMessage, ILoadEntitySetWithFilter<TEntity>> LoadEntitySetWithFilter = (s, x) => x.LoadEntitySet();
        //private static readonly Action<ISourceMessage, ILoadEntitySetWithFilterWithIncludes<TEntity>> LoadEntitySetWithFilterWithIncludes = (s, x) => x.LoadEntitySet();

        //TODO: Add EntityViews
        //private static readonly Action<ISourceMessage, LoadEntityView<TEntity>> LoadEntityView = (s, x) => LoadEntityView(s, x);
        //private static readonly Action<ISourceMessage, LoadEntityViewWithFilter<TEntity>> LoadEntityViewWithFilter = (s, x) => x.LoadEntityView(s);

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
                try
                {
                    this.GetType()
                        .GetMethod("CreateEntityViewActor")
                        .MakeGenericMethod(itm.Key)
                        .Invoke(this, new object[] {itm.Value, process});
                }
                catch (Exception ex)
                {
                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: itm.Key,
                        failedEventMessage: new ProcessSystemMessage(process, SourceMessage), 
                        expectedEventType: typeof (ServiceStarted<>),
                        exception: ex,
                        SourceMsg: SourceMessage),SourceMessage);
                }
            }

        }

        public void CreateEntityViewActor<TEvent>(object action, ISystemProcess process) where TEvent : IMessage
        {
            /// Create Actor Per Event
            try
            {
                    Type actorType = typeof(EntityViewDataServiceActor<>).MakeGenericType(typeof(TEvent));
                    _childActor = Context.ActorOf(Props.Create(actorType, action, process).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))),
                            "EntityViewDataServiceActor-" + typeof(TEvent).GetFriendlyName().Replace("<", "'").Replace(">", "'"));

                    EventMessageBus.Current.GetEvent<TEvent>(SourceMessage).Subscribe(x => _childActor.Tell(x));
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ServiceStarted<TEvent>),
                        failedEventMessage: new ProcessSystemMessage(process, SourceMessage),
                        expectedEventType: typeof(ServiceStarted<>),
                        exception: ex,
                        SourceMsg: SourceMessage), SourceMessage);
            }
            
        }

        private IActorRef _childActor;
        SourceMessage SourceMessage => new SourceMessage(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

      

    }

}