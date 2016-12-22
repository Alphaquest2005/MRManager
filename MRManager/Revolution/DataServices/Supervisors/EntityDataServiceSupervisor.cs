using System;
using System.Collections.Generic;
using SystemInterfaces;
using Akka.Actor;
using Akka.Routing;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using Utilities;

namespace DataServices.Actors
{
    public class EntityDataServiceSupervisor<TEntity> : ReceiveActor where TEntity : IEntity
    {
        private static readonly Action<IDataContext, MessageSource, CreateEntity<TEntity>> CreateAction = (ctx, s, x) => x.CreateEntity(ctx, s);
        private static readonly Action<IDataContext, MessageSource, DeleteEntity<TEntity>> DeleteAction = (ctx, s, x) => x.DeleteEntity(ctx, s);
        private static readonly Action<IDataContext, MessageSource, EntityChanges<TEntity>> UpdateAction = (ctx, s, x) => x.UpdateEntity(ctx, s);
        private static readonly Action<IDataContext, MessageSource, GetEntityById<TEntity>> GetAction = (ctx, s, x) => x.GetEntity(ctx, s);

        private static readonly Action<IDataContext, MessageSource, LoadEntitySet<TEntity>> LoadEntitySet = (ctx, s, x) => x.LoadEntitySet(ctx, s);
        private static readonly Action<IDataContext, MessageSource, LoadEntitySetWithFilter<TEntity>> LoadEntitySetWithFilter = (ctx, s, x) => x.LoadEntitySet(ctx, s);
        private static readonly Action<IDataContext, MessageSource, LoadEntitySetWithFilterWithIncludes<TEntity>> LoadEntitySetWithFilterWithIncludes = (ctx, s, x) => x.LoadEntitySet(ctx, s);

        private static readonly Action<IDataContext, MessageSource, LoadEntityView<TEntity>> LoadEntityView = (ctx, s, x) => x.LoadEntityView(ctx, s);
        private static readonly Action<IDataContext, MessageSource, LoadEntityViewWithFilter<TEntity>> LoadEntityViewWithFilter = (ctx, s, x) => x.LoadEntityView(ctx, s);

        readonly Dictionary<Type, object> entityEvents =
            new Dictionary<Type, object>()
            {
                {typeof (CreateEntity<TEntity>), CreateAction},
                {typeof (DeleteEntity<TEntity>), DeleteAction},
                {typeof (EntityChanges<TEntity>), UpdateAction},
                {typeof (GetEntityById<TEntity>), GetAction},

                {typeof (LoadEntitySet<TEntity>), LoadEntitySet},
                {typeof (LoadEntitySetWithFilter<TEntity>), LoadEntitySetWithFilter},
                {typeof (LoadEntitySetWithFilterWithIncludes<TEntity>), LoadEntitySetWithFilterWithIncludes},

                {typeof (LoadEntityView<TEntity>), LoadEntityView},
                {typeof (LoadEntityViewWithFilter<TEntity>), LoadEntityViewWithFilter},
            };
        
        public EntityDataServiceSupervisor(IDataContext dbContext)
        {
            foreach (var itm in entityEvents)
            {
                this.GetType()
                    .GetMethod("CreateEntityActor")
                    .MakeGenericMethod(itm.Key)
                    .Invoke(this, new object[] {dbContext, itm.Value});
                //CreateEntityActor(itm.Key, DbContext, itm.Value);
            }
            
        }

        public void CreateEntityActor<TEvent>(IDataContext dbContext, object action, ISystemProcess process)
        {
            /// Create Actor Per Event
            Type actorType = typeof(EntityDataServiceActor<>).MakeGenericType(typeof(TEvent));
            _childActor = Context.ActorOf(Props.Create(actorType, dbContext, action, process).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))),
                    "EntityDataServiceActor-" + typeof(TEvent).GetFriendlyName().Replace("<", "'").Replace(">", "'"));

            EventMessageBus.Current.GetEvent<TEvent>(MsgSource).Subscribe(x => _childActor.Tell(x));
        }

        private IActorRef _childActor;
        MessageSource MsgSource => new MessageSource(this.ToString());

      

    }

}