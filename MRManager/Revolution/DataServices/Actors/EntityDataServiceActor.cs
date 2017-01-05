using System;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using CommonMessages;
using EventAggregator;
using RevolutionEntities.Process;

namespace DataServices.Actors
{
    public interface IEntityDataServiceActor<TService>:IAgent
    {
    }

    public class EntityDataServiceActor<TService>: BaseActor<EntityDataServiceActor<TService>>, IEntityDataServiceActor<TService>
    {
        private Action<ISourceMessage, TService> Action { get; }
       
      
        
        public EntityDataServiceActor(Action<ISourceMessage, TService> action, ISystemProcess process) 
        {
            Action = action;
            Command<TService>(m => HandledEvent(m));
            EventMessageBus.Current.Publish(new ServiceStarted<IEntityDataServiceActor<TService>>(this,process,SourceMessage), SourceMessage);
        }

        
        private void HandledEvent(TService msg)
        {
            //TODO:Implement Logging
          // Persist(msg, x => { });//x => Action.Invoke(DbContext, SourceMessage, x)
           Action.Invoke(SourceMessage, msg);
        }
    }
}