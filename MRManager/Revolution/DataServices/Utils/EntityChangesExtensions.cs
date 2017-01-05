using System;
using SystemInterfaces;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public static class EntityChangesExtensions
    {
        public static void UpdateEntity<TEntity>(this IUpdateEntity<TEntity> msg) where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.Update(msg);
            

        }
    }
}