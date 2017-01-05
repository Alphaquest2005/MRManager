using System;
using System.Linq;
using SystemInterfaces;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;


namespace DataServices.Actors
{
    public static class GetEntityExtensions
    {
       
        public static void GetEntity<TEntity>(this IGetEntityById<TEntity> msg) where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.GetEntityById(msg);
        }

        public static void GetEntity<TEntity>(this IGetEntityWithChanges<TEntity> msg) where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.GetEntityWithChanges(msg);
        }

        public static void GetEntity<TEntityView>(this IGetEntityViewById<TEntityView> msg) where TEntityView : IEntityId
        {

            EntityViewDataContext<TEntityView>.GetEntityById(msg);
        }

        public static void GetEntity<TEntityView>(this IGetEntityViewWithChanges<TEntityView> msg) where TEntityView : IEntityId
        {

            EntityViewDataContext<TEntityView>.GetEntityWithChanges(msg);
        }
    }
}