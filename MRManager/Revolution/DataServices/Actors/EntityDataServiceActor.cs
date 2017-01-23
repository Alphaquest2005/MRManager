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
    public interface IEntityDataServiceActor<TService>:IAgent
    {
    }

    public class EntityDataServiceActor<TService>: BaseActor<EntityDataServiceActor<TService>>, IEntityDataServiceActor<TService> where TService:IProcessSystemMessage
    {
        private Action<ISystemSource,TService> Action { get; }
       
      
        
        public EntityDataServiceActor(ICreateEntityService msg) : base(msg.Process)
        {
            Action = (Action<ISystemSource,TService>)msg.Action;
            Command<TService>(m => HandledEvent(m));
            
            EventMessageBus.Current.Publish(new ServiceStarted<IEntityDataServiceActor<TService>>(this,new StateEventInfo(msg.Process.Id, RevolutionData.Context.Actor.Events.ActorStarted), msg.Process,Source), Source);
        }


        private void HandledEvent(TService msg)
        {
            //TODO:Implement Logging
            // Persist(msg, x => { });//x => Action.Invoke(DbContext, Source, x)
            try
            {
                Action.Invoke(Source,msg);
            }
            catch (Exception ex)
            {

                PublishProcesError(msg, ex, typeof (TService));
            }

        }
    }
}