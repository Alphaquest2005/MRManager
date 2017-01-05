using System;
using SystemInterfaces;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public static class CreateEntityExtensions
    {
        
        public static void CreateEntity<TEntity>(this ICreateEntity<TEntity> msg) where TEntity: class, IEntity
        {
            EF7DataContext<TEntity>.Create(msg);
        }

    }


}