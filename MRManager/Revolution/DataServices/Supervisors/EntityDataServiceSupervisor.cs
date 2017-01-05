﻿using System;
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
    public class EntityDataServiceSupervisor<TEntity> : ReceiveActor where TEntity : class, IEntity
    {
        private static readonly Action<ICreateEntity<TEntity>> CreateAction = ( x) => x.CreateEntity();
        private static readonly Action<IDeleteEntity<TEntity>> DeleteAction = (x) => x.DeleteEntity();
        private static readonly Action<IUpdateEntity<TEntity>> UpdateAction = (x) => x.UpdateEntity();
        private static readonly Action<IGetEntityById<TEntity>> GetEntityByIdAction = (x) => x.GetEntity();
        private static readonly Action<ISourceMessage, IGetEntityWithChanges<TEntity>> GetEntityWithChangesAction = (s, x) => x.GetEntity();

        private static readonly Action<ISourceMessage, ILoadEntitySet<TEntity>> LoadEntitySet = (s, x) => x.LoadEntitySet();
        private static readonly Action<ISourceMessage, ILoadEntitySetWithFilter<TEntity>> LoadEntitySetWithFilter = (s, x) => x.LoadEntitySet();
        private static readonly Action<ISourceMessage, ILoadEntitySetWithFilterWithIncludes<TEntity>> LoadEntitySetWithFilterWithIncludes = (s, x) => x.LoadEntitySet();

        //TODO: Add EntityViews
        //private static readonly Action<ISourceMessage, LoadEntityView<TEntity>> LoadEntityView = (s, x) => LoadEntityView(s, x);
        //private static readonly Action<ISourceMessage, LoadEntityViewWithFilter<TEntity>> LoadEntityViewWithFilter = (s, x) => x.LoadEntityView(s);

        readonly Dictionary<Type, object> entityEvents =
            new Dictionary<Type, object>()
            {
                //{typeof (CreateEntity<TEntity>), CreateAction},
                //{typeof (DeleteEntity<TEntity>), DeleteAction},
                //{typeof (EntityChanges<TEntity>), UpdateAction},
                //{typeof (GetEntityById<TEntity>), GetEntityByIdAction},
                {typeof (IGetEntityWithChanges<TEntity>), GetEntityWithChangesAction},

                //{typeof (LoadEntitySet<TEntity>), LoadEntitySet},
                //{typeof (LoadEntitySetWithFilter<TEntity>), LoadEntitySetWithFilter},
                //{typeof (LoadEntitySetWithFilterWithIncludes<TEntity>), LoadEntitySetWithFilterWithIncludes},

                //{typeof (LoadEntityView<TEntity>), LoadEntityView},
                //{typeof (LoadEntityViewWithFilter<TEntity>), LoadEntityViewWithFilter},
            };

        public EntityDataServiceSupervisor(ISystemProcess process)
        {
            foreach (var itm in entityEvents)
            {
                try
                {
                    this.GetType()
                        .GetMethod("CreateEntityActor")
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

        public void CreateEntityActor<TEvent>(object action, ISystemProcess process) where TEvent : IMessage
        {
            /// Create Actor Per Event
            try
            {
                    Type actorType = typeof(EntityDataServiceActor<>).MakeGenericType(typeof(TEvent));
                    _childActor = Context.ActorOf(Props.Create(actorType, action, process).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))),
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