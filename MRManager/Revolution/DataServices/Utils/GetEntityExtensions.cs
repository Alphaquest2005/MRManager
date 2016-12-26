using System.Linq;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using NHibernate.Linq;

namespace DataServices.Actors
{
    public static class GetEntityExtensions
    {
       
        public static void GetEntity<T>(this GetEntityById<T> msg, IDataContext dbContext, MessageSource msgSource ) where T : IEntity
        {

            using (var ctx = dbContext.Instance.OpenSession())
            using (var transaction = ctx.BeginTransaction())
            {
                var p = ctx.Query<T>().FirstOrDefault(x => x.Id == msg.EntityId);
                if (p != null)
                {
                    EventMessageBus.Current.Publish(new EntityFound<T>(p,msg.Process, msg), msgSource);
                }
                else
                {
                    EventMessageBus.Current.Publish(new EntityNotFound<T>(msg.EntityId,msg.Process, msg), msgSource);
                }
                
            }

        }

    }
}