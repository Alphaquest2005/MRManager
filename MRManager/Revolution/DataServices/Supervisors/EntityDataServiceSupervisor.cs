using System;
using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using Akka.IO;
using Akka.Routing;
using CommonMessages;
using Core.Common.UI;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using RevolutionEntities.Process;
using Utilities;
using ViewMessages;

namespace DataServices.Actors
{
    public class EntityDataServiceSupervisor<TEntity> : ReceiveActor where TEntity : class, IEntity
    {
        private static readonly Action<IDataContext, ISourceMessage, CreateEntity<TEntity>> CreateAction = (ctx, s, x) => x.CreateEntity(ctx, s);
        private static readonly Action<IDataContext, ISourceMessage, DeleteEntity<TEntity>> DeleteAction = (ctx, s, x) => x.DeleteEntity(ctx, s);
        private static readonly Action<IDataContext, ISourceMessage, EntityChanges<TEntity>> UpdateAction = (ctx, s, x) => x.UpdateEntity(ctx, s);
        private static readonly Action<IDataContext, ISourceMessage, GetEntityById<TEntity>> GetEntityByIdAction = (ctx, s, x) => x.GetEntity(ctx, s);
        private static readonly Action<IDataContext, ISourceMessage, GetEntityWithChanges<TEntity>> GetEntityWithChangesAction = (ctx, s, x) => x.GetEntity(ctx, s);

        private static readonly Action<IDataContext, ISourceMessage, LoadEntitySet<TEntity>> LoadEntitySet = (ctx, s, x) => x.LoadEntitySet(ctx, s);
        private static readonly Action<IDataContext, ISourceMessage, LoadEntitySetWithFilter<TEntity>> LoadEntitySetWithFilter = (ctx, s, x) => x.LoadEntitySet(ctx, s);
        private static readonly Action<IDataContext, ISourceMessage, LoadEntitySetWithFilterWithIncludes<TEntity>> LoadEntitySetWithFilterWithIncludes = (ctx, s, x) => x.LoadEntitySet(ctx, s);

        private static readonly Action<IDataContext, ISourceMessage, LoadEntityView<TEntity>> LoadEntityView = (ctx, s, x) => x.LoadEntityView(ctx, s);
        private static readonly Action<IDataContext, ISourceMessage, LoadEntityViewWithFilter<TEntity>> LoadEntityViewWithFilter = (ctx, s, x) => x.LoadEntityView(ctx, s);

        readonly Dictionary<Type, object> entityEvents =
            new Dictionary<Type, object>()
            {
                //{typeof (CreateEntity<TEntity>), CreateAction},
                //{typeof (DeleteEntity<TEntity>), DeleteAction},
                //{typeof (EntityChanges<TEntity>), UpdateAction},
                //{typeof (GetEntityById<TEntity>), GetEntityByIdAction},
                {typeof (GetEntityWithChanges<TEntity>), GetEntityWithChangesAction},

                //{typeof (LoadEntitySet<TEntity>), LoadEntitySet},
                //{typeof (LoadEntitySetWithFilter<TEntity>), LoadEntitySetWithFilter},
                //{typeof (LoadEntitySetWithFilterWithIncludes<TEntity>), LoadEntitySetWithFilterWithIncludes},

                //{typeof (LoadEntityView<TEntity>), LoadEntityView},
                //{typeof (LoadEntityViewWithFilter<TEntity>), LoadEntityViewWithFilter},
            };

        public EntityDataServiceSupervisor(IDataContext dbContext, ISystemProcess process)
        {
            foreach (var itm in entityEvents)
            {
                try
                {
                    this.GetType()
                        .GetMethod("CreateEntityActor")
                        .MakeGenericMethod(itm.Key)
                        .Invoke(this, new object[] {dbContext, itm.Value, process});
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

        public void CreateEntityActor<TEvent>(IDataContext dbContext, object action, ISystemProcess process)
        {
            /// Create Actor Per Event
            try
            {
                    Type actorType = typeof(EntityDataServiceActor<>).MakeGenericType(typeof(TEvent));
                    _childActor = Context.ActorOf(Props.Create(actorType, dbContext, action, process).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))),
                            "EntityDataServiceActor-" + typeof(TEvent).GetFriendlyName().Replace("<", "'").Replace(">", "'"));

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