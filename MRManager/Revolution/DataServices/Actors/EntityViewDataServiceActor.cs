using System;
using SystemInterfaces;
using SystemMessages;
using Actor.Interfaces;
using CommonMessages;
using EventAggregator;
using RevolutionEntities.Process;

namespace DataServices.Actors
{
    public interface IEntityViewDataServiceActor<TService>:IAgent
    {
    }

    public class EntityViewDataServiceActor<TService>: BaseActor<EntityViewDataServiceActor<TService>>, IEntityViewDataServiceActor<TService>
    {
        private Action<TService> Action { get; }
       
      
        
        public EntityViewDataServiceActor(Action<TService> action, ISystemProcess process) 
        {
            Action = action;
            Command<TService>(m => HandledEvent(m));
            EventMessageBus.Current.Publish(new ServiceStarted<IEntityViewDataServiceActor<TService>>(this,process,SourceMessage), SourceMessage);
        }

        
        private void HandledEvent(TService msg)
        {
            //TODO:Implement Logging
          // Persist(msg, x => { });//x => Action.Invoke(DbContext, SourceMessage, x)
           Action.Invoke(msg);
        }
    }
}