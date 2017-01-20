using System;
using System.Collections.Generic;
using System.Threading;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using Akka.IO;
using Akka.Routing;
using Common;
using CommonMessages;
using EventAggregator;
using EventMessages;
using RevolutionData;
using RevolutionEntities.Process;
using Utilities;
using ViewMessages;

namespace DataServices.Actors
{
    public class EntityViewDataServiceSupervisor<TEntityView> : ReceiveActor, IProcessSource where TEntityView : IEntityView
//where TEntity : class, IEntity where TEntityView:IEntityView<TEntity>
    {
        public ISystemSource Source => new Source(Guid.NewGuid(), $"EntityViewSupervisor:<{typeof(TEntityView).GetFriendlyName()}>",new SourceType(typeof(EntityViewDataServiceSupervisor<TEntityView>)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        private static readonly Action<IGetEntityViewById<TEntityView>> GetEntityByIdAction = (x) => x.GetEntity();
        private static readonly Action<IGetEntityViewWithChanges<TEntityView>> GetEntityWithChangesAction = (x) => x.GetEntityViewWithChanges();
        private static readonly Action<ILoadEntityViewSetWithChanges<TEntityView>> LoadEntityViewSetWithChangesAction = (x) => x.LoadEntityViewSetWithChanges();

       

        readonly Dictionary<Type, object> entityEvents =
            new Dictionary<Type, object>()
            {
                
                
                {typeof (IGetEntityViewById<TEntityView>), GetEntityByIdAction},
                {typeof (IGetEntityViewWithChanges<TEntityView>), GetEntityWithChangesAction},
                {typeof (ILoadEntityViewSetWithChanges<TEntityView>), LoadEntityViewSetWithChangesAction},

            };

        public EntityViewDataServiceSupervisor(ISystemProcess process, IProcessSystemMessage msg)
        {
            try
            {
                 foreach (var itm in entityEvents)
                            {
                               this.GetType()
                                        .GetMethod("CreateEntityViewActor")
                                        .MakeGenericMethod(itm.Key)
                                        .Invoke(this, new object[] {itm.Value, process, msg});
               
                            }
            }
            catch (Exception)
            {
                
                throw;
            }
           

        }

        public void CreateEntityViewActor<TEvent>(object action, ISystemProcess process, IProcessSystemMessage msg) where TEvent : IMessage
        {
            Type actorType = typeof(EntityViewDataServiceActor<>).MakeGenericType(typeof(TEvent));
            var inMsg = new CreateEntityViewService(actorType, action, new StateCommandInfo(process.Id, RevolutionData.Context.Actor.Commands.StartActor), process,Source);
            EventMessageBus.Current.Publish(inMsg, Source);
            /// Create Actor Per Event
            try
            {
               
                
                _childActor = Context.ActorOf(Props.Create(inMsg.ActorType, inMsg).WithRouter(new RoundRobinPool(1, new DefaultResizer(1, Environment.ProcessorCount, 1, .2, .3, .1, Environment.ProcessorCount))),
                            "EntityViewDataServiceActor-" + typeof(TEvent).GetFriendlyName().Replace("<", "'").Replace(">", "'"));

                    EventMessageBus.Current.GetEvent<TEvent>(Source).Subscribe(x => _childActor.Tell(x));
               // Thread.Sleep(TimeSpan.FromMilliseconds(50));
                _childActor.Tell(msg);
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: inMsg.GetType(),
                        failedEventMessage: inMsg,
                        expectedEventType: typeof(ServiceStarted<>),
                        exception: ex,
                        source: Source, processInfo: new StateEventInfo(process.Id, RevolutionData.Context.Process.Events.Error)), Source);
            }
            
        }

        private IActorRef _childActor;
        

      

    }


}