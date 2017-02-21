using System;
using System.Reactive.Linq;
using SystemInterfaces;
using Actor.Interfaces;
using Akka.Actor;
using CommonMessages;
using EventAggregator;
using EventMessages.Events;
using RevolutionData;
using RevolutionEntities.Process;

namespace DataServices.Actors
{
    public interface IEntityViewDataServiceActor<TService>:IAgent
    {
    }

    public class EntityViewDataServiceActor<TService>: BaseActor<EntityViewDataServiceActor<TService>>, IEntityViewDataServiceActor<TService> where TService:IProcessSystemMessage
    {
        private Action<TService> Action { get; }
       
      
        
        public EntityViewDataServiceActor(ICreateEntityViewService msg, IProcessSystemMessage firstMsg) : base(msg.Process)
        {
            Action = (Action<TService>) msg.Action;
           // Command<TService>(m => HandledEvent(m));
           if(firstMsg is TService) HandledEvent((TService)firstMsg);
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