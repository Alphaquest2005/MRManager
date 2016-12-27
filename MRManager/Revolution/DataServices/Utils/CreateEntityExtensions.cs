using CommonMessages;
using DataInterfaces;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public static class CreateEntityExtensions
    {
        
        public static void CreateEntity<T>(this CreateEntity<T> msg, IDataContext dbContext, ISourceMessage source) where T:IEntity
        {
           
            using (var ctx = dbContext.Instance.OpenSession())
            using (var transaction = ctx.BeginTransaction())
            {
                ctx.SaveOrUpdate(msg.Entity);
                transaction.Commit();
                msg.Entity.RowState = RowState.Unchanged; // get nhibernate to reload entity to set RowState to loaded
               EventMessageBus.Current.Publish(new EntityCreated<T>(msg.Entity,msg.Process, source), source);
            }
            
        }

    }


}