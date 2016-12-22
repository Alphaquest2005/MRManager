using System;
using SystemInterfaces;
using SystemMessages;
using CommonMessages;
using DataInterfaces;
using EventAggregator;

namespace DataServices.Actors
{
    public class EntityDataServiceActor<TService>: BaseActor<EntityDataServiceActor<TService>>
    {
        private Action<IDataContext, MessageSource, TService> Action { get; }
      
        internal IDataContext DbContext { get; }
        public EntityDataServiceActor(IDataContext ctx, Action<IDataContext,MessageSource,TService> action, ISystemProcess process) 
        {
            DbContext = ctx;
            Action = action;
            Command<TService>(m => HandledEvent(m));
            EventMessageBus.Current.Publish(new ServiceStarted<TService>(process,MsgSource), MsgSource);
        }

        
        private void HandledEvent(TService msg)
        {
           Persist(msg, x => Action.Invoke(DbContext, MsgSource, x));
           // Action.Invoke(DbContext, MsgSource, msg);
        }
    }
}