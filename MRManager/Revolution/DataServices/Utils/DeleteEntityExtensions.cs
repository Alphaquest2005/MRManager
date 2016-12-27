using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public static class DeleteEntityExtensions
    {
        
        public static void DeleteEntity<T>(this DeleteEntity<T> msg, IDataContext dbContext, ISourceMessage source) where T : IEntity
        {

            using (var ctx = dbContext.Instance.OpenSession())
            using (var transaction = ctx.BeginTransaction())
            {
                var entity = ctx.Load<T>(msg.EntityId);
                ctx.Delete(entity);
                transaction.Commit();
                EventMessageBus.Current.Publish(new EntityDeleted<T>(msg.EntityId,msg.Process, source),source);
            }

        }

    }
}