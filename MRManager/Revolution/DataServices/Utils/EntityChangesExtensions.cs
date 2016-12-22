using Common;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public static class EntityChangesExtensions
    {
        public static void UpdateEntity<T>(this EntityChanges<T> msg, IDataContext dbContext, MessageSource source) where T : IEntity
        {

            using (var ctx = dbContext.Instance.OpenSession())
            using (var transaction = ctx.BeginTransaction())
            {

                var p = ctx.Load<T>(msg.EntityId);
                p.ApplyChanges(msg.Changes);
                ctx.SaveOrUpdate(p);
                transaction.Commit();
                EventMessageBus.Current.Publish(new EntityUpdated<T>(p,msg.Process, msg.Source), source);

            }

        }
    }
}