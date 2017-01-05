using System;
using SystemInterfaces;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public static class DeleteEntityExtensions
    {
        
        public static void DeleteEntity<TEntity>(this IDeleteEntity<TEntity> msg, ISourceMessage source) where TEntity : class, IEntity
        {
            EF7DataContext<TEntity>.Delete(msg.EntityId, msg.Process);
        }

    }
}