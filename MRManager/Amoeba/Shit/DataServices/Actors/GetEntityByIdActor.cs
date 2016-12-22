using System.Linq;
using Akka.Actor;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using NHibernate.Linq;

namespace DataServices.Actors
{
   public class GetEntityByIdActor<T>: ReceiveActor where T : IEntity
    {
       private readonly IDataContext dbContext = null;
       public GetEntityByIdActor(IDataContext context)
        {
             
            Receive<GetEntityById<T>>(m => HandleGetEntityById(m.EntityId, m.Source));
           dbContext = context;
        }

        MessageSource msgSource => new MessageSource(this.ToString());

        private void HandleGetEntityById(int entityId, MessageSource source)
       {
            using (var ctx = dbContext.Instance.OpenSession())
            using (var transaction = ctx.BeginTransaction())
            {
                var p = ctx.Query<T>().FirstOrDefault(x => x.Id == entityId);
                if (p != null)
                {
                    EventMessageBus.Current.Publish(new EntityUpdated<T>(p, source), msgSource);
                }
                else
                {
                    EventMessageBus.Current.Publish(new EntityNotFound<T>(entityId, source), msgSource);
                }
            }
        }
    }
}
