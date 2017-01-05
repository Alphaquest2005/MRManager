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
       
        public static void GetEntity<TEntity>(this IGetEntityById<TEntity> msg, ISourceMessage msgSource ) where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.GetEntityById(msg.EntityId, msg.Process);
        }

        public static void GetEntity<TEntity>(this IGetEntityWithChanges<TEntity> msg, ISourceMessage msgSource) where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.GetEntityWithChanges(msg.EntityId, msg.Changes, msg.Process);
        }

    }
}