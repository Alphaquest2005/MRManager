using System;
using System.Collections.Generic;
using System.Threading;
using SystemInterfaces;
using Akka.Actor;
using Akka.IO;
using Akka.Routing;
using CommonMessages;
using EventAggregator;
using EventMessages;
using EventMessages.Commands;
using RevolutionEntities.Process;
using Utilities;
using ViewMessages;

namespace DataServices.Actors
{
    public class EntityDataServiceSupervisor<TEntity> : BaseSupervisor<EntityDataServiceSupervisor<TEntity>> where TEntity : class, IEntity
    {

        private static readonly Action<ICreateEntity<TEntity>> CreateAction = ( x) => x.CreateEntity();
        private static readonly Action<IDeleteEntity<TEntity>> DeleteAction = (x) => x.DeleteEntity();
        private static readonly Action<IUpdateEntity<TEntity>> UpdateAction = (x) => x.UpdateEntity();
        private static readonly Action<IGetEntityById<TEntity>> GetEntityByIdAction = (x) => x.GetEntity();
        private static readonly Action<ISystemSource, IGetEntityWithChanges<TEntity>> GetEntityWithChangesAction = (s, x) => x.GetEntity();

        private static readonly Action<ISystemSource, ILoadEntitySet<TEntity>> LoadEntitySet = (s, x) => x.LoadEntitySet();
        private static readonly Action<ISystemSource, ILoadEntitySetWithFilter<TEntity>> LoadEntitySetWithFilter = (s, x) => x.LoadEntitySet();
        private static readonly Action<ISystemSource, ILoadEntitySetWithFilterWithIncludes<TEntity>> LoadEntitySetWithFilterWithIncludes = (s, x) => x.LoadEntitySet();

        
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

            };
        private IUntypedActorContext ctx = null;
        public EntityDataServiceSupervisor(ISystemProcess process, IProcessSystemMessage msg)
        {
            ctx = Context;
            foreach (var itm in entityEvents)
            {
              this.GetType()
                        .GetMethod("CreateEntityActor")
                        .MakeGenericMethod(itm.Key)
                        .Invoke(this, new object[] {itm.Value, process, msg});
            }

        }

        public void CreateEntityActor<TEvent>(object action, ISystemProcess process, IProcessSystemMessage msg) where TEvent : IMessage
        {
            /// Create Actor Per Event
            Type actorType = typeof(EntityDataServiceActor<>).MakeGenericType(typeof(TEvent));
            var inMsg = new CreateEntityService(actorType,action, new StateCommandInfo(process.Id, RevolutionData.Context.Actor.Commands.StartActor),process,Source );
            try
            {
                
                    
                    _childActor = ctx.ActorOf(Props.Create(actorType, inMsg).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))),
                            "EntityDataServiceActor-" + typeof(TEvent).GetFriendlyName().Replace("<", "'").Replace(">", "'"));

                    EventMessageBus.Current.GetEvent<TEvent>(Source).Subscribe(x => _childActor.Tell(x));
                //Thread.Sleep(TimeSpan.FromMilliseconds(50));
                _childActor.Tell(msg);

            }
            catch (Exception ex)
            {
                //ToDo: This seems like a good way... getting the expected event type 
                PublishProcesError(inMsg, ex, inMsg.ProcessInfo.State.ExpectedEvent.GetType());
            }
            
        }

        private IActorRef _childActor;
       

      

    }

}