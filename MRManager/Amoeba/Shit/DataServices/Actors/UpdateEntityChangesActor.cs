using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using NHibernate.Linq;

namespace DataServices.Actors
{
   public class UpdateEntityChangesActor<T>: ReceiveActor where T : IEntity
   {
       private IDataContext dbContext;
       public UpdateEntityChangesActor(IDataContext ctx)
       {
           dbContext = ctx;
           Receive<EntityChanges<T>>(m => HandleEntityChanges(m.EntityId, m.Changes, m.Source));
        
       }

        void HandleEntityChanges(int entityId, Dictionary<string, dynamic> changes, MessageSource source)
        {
            using (var ctx = dbContext.Instance.OpenSession())
            using (var transaction = ctx.BeginTransaction())
            {
                var p = ctx.Query<T>().FirstOrDefault(x => x.Id == entityId);

                foreach (var c in changes)
                {
                    p.GetType().GetProperty(c.Key).SetValue(p, c.Value);
                }
                ctx.SaveOrUpdate(p);
                transaction.Commit();
                if (p != null) EventMessageBus.Current.Publish(new EntityUpdated<T>(p,source), new MessageSource(this.ToString()));
            }
        }
    }
}
