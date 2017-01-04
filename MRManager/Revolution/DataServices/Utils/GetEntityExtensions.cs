using System;
using System.Linq;
using System.Linq.Dynamic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using NHibernate.Linq;

namespace DataServices.Actors
{
    public static class GetEntityExtensions
    {
       
        public static void GetEntity<T>(this GetEntityById<T> msg, IDataContext dbContext, ISourceMessage msgSource ) where T : IEntity
        {

            using (var ctx = dbContext.Instance.OpenSession())
            using (var transaction = ctx.BeginTransaction())
            {
                var p = ctx.Query<T>().FirstOrDefault(x => x.Id == msg.EntityId);
                if (p != null)
                {
                    EventMessageBus.Current.Publish(new EntityFound<T>(p,msg.Process, msgSource), msgSource);
                }
                else
                {
                    EventMessageBus.Current.Publish(new EntityNotFound<T>(msg.EntityId,msg.Process, msgSource), msgSource);
                }
                
            }

        }

        public static void GetEntity<TEntity>(this IGetEntityWithChanges<TEntity> msg, IDataContext dbContext, ISourceMessage msgSource) where TEntity : class, IEntity
        {
           
            using (var ctx = dbContext.Instance.OpenSession())
            using (var transaction = ctx.BeginTransaction())
            {

                try
                {
                    var whereStr = msg.Changes.Aggregate("", (str, itm) => str + ($"{itm.Key} == \"{itm.Value}\" &&"));
                    whereStr = whereStr.TrimEnd('&');
                    if (string.IsNullOrEmpty(whereStr)) return;
                    var p = ctx.Query<TEntity>().Where(whereStr).FirstOrDefault();
                    if (p != null)
                    {
                        EventMessageBus.Current.Publish(
                            new EntityWithChangesFound<TEntity>(p, msg.Changes, msg.Process, msgSource), msgSource);
                    }
                    else
                    {
                        EventMessageBus.Current.Publish(
                            new EntityNotFound<TEntity>(msg.EntityId, msg.Process, msgSource), msgSource);
                    }
                }
                catch (Exception ex)
                {

                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(IGetEntityWithChanges<TEntity>),
                                                                         failedEventMessage: msg,
                                                                         expectedEventType: typeof(IEntityWithChangesFound<TEntity>),
                                                                         exception: ex,
                                                                         SourceMsg: msgSource), msgSource);
                }


            }

        }

    }
}