using System;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using RevolutionEntities.Process;

namespace DataServices.Actors
{
    public interface IEntityDataServiceActor<TService>:IAgent
    {
    }

    public class EntityDataServiceActor<TService>: BaseActor<EntityDataServiceActor<TService>>, IEntityDataServiceActor<TService>
    {
        private Action<IDataContext, ISourceMessage, TService> Action { get; }
       
      
        internal IDataContext DbContext { get; }
        public EntityDataServiceActor(IDataContext ctx, Action<IDataContext, ISourceMessage, TService> action, ISystemProcess process) 
        {
            DbContext = ctx;
            Action = action;
            Command<TService>(m => HandledEvent(m));
            EventMessageBus.Current.Publish(new ServiceStarted<IEntityDataServiceActor<TService>>(this,process,SourceMessage), SourceMessage);
        }

        
        private void HandledEvent(TService msg)
        {
            //TODO:Implement Logging
          // Persist(msg, x => { });//x => Action.Invoke(DbContext, SourceMessage, x)
           Action.Invoke(DbContext, SourceMessage, msg);
        }
    }
}