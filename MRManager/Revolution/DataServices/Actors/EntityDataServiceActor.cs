using System;
using SystemInterfaces;
using SystemMessages;
using CommonMessages;
using DataInterfaces;
using EventAggregator;
using RevolutionEntities.Process;

namespace DataServices.Actors
{
    public class EntityDataServiceActor<TService>: BaseActor<EntityDataServiceActor<TService>>
    {
        private Action<IDataContext, ISourceMessage, TService> Action { get; }
       
      
        internal IDataContext DbContext { get; }
        public EntityDataServiceActor(IDataContext ctx, Action<IDataContext, ISourceMessage, TService> action, ISystemProcess process) 
        {
            DbContext = ctx;
            Action = action;
            Command<TService>(m => HandledEvent(m));
            EventMessageBus.Current.Publish(new ServiceStarted<TService>(process,SourceMessage), SourceMessage);
        }

        
        private void HandledEvent(TService msg)
        {
           Persist(msg, x => Action.Invoke(DbContext, SourceMessage, x));
           // Action.Invoke(DbContext, MsgSource, msg);
        }
    }
}