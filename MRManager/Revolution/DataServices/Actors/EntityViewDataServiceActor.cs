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

    public class EntityViewDataServiceActor<TService>: BaseActor<EntityViewDataServiceActor<TService>>, IEntityViewDataServiceActor<TService> where TService:IProcessSystemMessage
    {
        private Action<TService> Action { get; }
       
      
        
        public EntityViewDataServiceActor(ICreateEntityViewService msg) 
        {
            Action = (Action<TService>) msg.Action;
            Command<TService>(m => HandledEvent(m));
            EventMessageBus.Current.GetEvent<TService>(Source).Subscribe(x => HandledEvent(x));
            EventMessageBus.Current.Publish(new ServiceStarted<IEntityViewDataServiceActor<TService>>(this,new StateEventInfo(msg.Process.Id, RevolutionData.Context.Actor.Events.ActorStarted), msg.Process,Source), Source);
        }

        
        private void HandledEvent(TService msg)
        {
            //TODO:Implement Logging
          // Persist(msg, x => { });//x => Action.Invoke(DbContext, Source, x)
            try
            {
                Action.Invoke(msg);
            }
            catch (Exception ex)
            {
                
               PublishProcesError(msg,ex, typeof(TService));
            }
           
        }
    }
}