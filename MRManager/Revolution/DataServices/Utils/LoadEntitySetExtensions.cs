using SystemInterfaces;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public static class LoadEntitySetExtensions
    {


        public static void LoadEntitySet<TEntity>(this ILoadEntitySet<TEntity> msg, ISourceMessage source)
            where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.LoadEntitySet(msg.Process);

        }

        public static void LoadEntitySet<TEntity>(this ILoadEntitySetWithFilter<TEntity> msg, ISourceMessage source)
            where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.LoadEntitySetWithFilter(msg.Filter, msg.Process);

        }

        public static void LoadEntitySet<TEntity>(this ILoadEntitySetWithFilterWithIncludes<TEntity> msg,
            ISourceMessage source) where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.LoadEntitySetWithFilterWithIncludes(msg.Filter, msg.Includes, msg.Process);

        }
    }
}