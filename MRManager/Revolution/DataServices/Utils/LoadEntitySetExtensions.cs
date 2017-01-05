using SystemInterfaces;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public static class LoadEntitySetExtensions
    {


        public static void LoadEntitySet<TEntity>(this ILoadEntitySet<TEntity> msg)
            where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.LoadEntitySet(msg);

        }

        public static void LoadEntitySet<TEntity>(this ILoadEntitySetWithFilter<TEntity> msg)
            where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.LoadEntitySetWithFilter(msg);

        }

        public static void LoadEntitySet<TEntity>(this ILoadEntitySetWithFilterWithIncludes<TEntity> msg) where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.LoadEntitySetWithFilterWithIncludes(msg);

        }
    }
}