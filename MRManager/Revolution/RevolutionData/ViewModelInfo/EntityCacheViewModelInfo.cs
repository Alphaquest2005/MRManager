using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SystemInterfaces;
using Actor.Interfaces;
using EventMessages.Commands;
using Interfaces;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace RevolutionData
{

    
    public class EntityCacheViewModelInfo<TEntity> where TEntity : IEntity
    {
        
        public static ViewModelInfo CacheViewModel(int processId)
        {
            return new ViewModelInfo
                (
                processId: processId,
                viewInfo: new ViewInfo($"{typeof(TEntity).Name}CacheViewModel","",""), 
                subscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {
                    new ViewEventSubscription<IEntityCacheViewModel<TEntity>, IEntitySetLoaded<TEntity>>(
                        processId: processId,
                        eventPredicate: e => e != null,
                        actionPredicate: new List<Func<IEntityCacheViewModel<TEntity>, IEntitySetLoaded<TEntity>, bool>>(),
                        action: (v, e) =>
                        {
                            if (Application.Current == null)
                            {
                                ReloadEntitySet(v, e);
                            }
                            else
                            {
                                Application.Current.Dispatcher.BeginInvoke(new Action(() => ReloadEntitySet(v, e)));
                            }
                        }),

                    new ViewEventSubscription<IEntityCacheViewModel<TEntity>, IEntityUpdated<TEntity>>(
                        processId: processId,
                        eventPredicate: e => e != null,
                        actionPredicate: new List<Func<IEntityCacheViewModel<TEntity>, IEntityUpdated<TEntity>, bool>>(),
                        action: (v, e) =>
                        {
                            if (Application.Current == null)
                            {
                                UpdateEntitySet(v, e);
                            }
                            else
                            {
                                Application.Current.Dispatcher.BeginInvoke(new Action(() => UpdateEntitySet(v, e)));
                            }
                        }),
                    new ViewEventSubscription<IEntityCacheViewModel<TEntity>, ICurrentEntityChanged<TEntity>>(
                            3,
                            e => e != null,
                            new List<Func<IEntityCacheViewModel<TEntity>, ICurrentEntityChanged<TEntity>, bool>>(),
                            (v,e) => v.CurrentEntity.Value = e.Entity),

                },
                publications: new List<IViewModelEventPublication<IViewModel, IEvent>> {},
                commands: new List<IViewModelEventCommand<IViewModel, IEvent>>
                {
                    
                },
                viewModelType: typeof(IEntityCacheViewModel<TEntity>),
                orientation: typeof (ICacheViewModel));
        }


       
        private static void UpdateEntitySet(IEntityCacheViewModel<TEntity> cacheViewModel,
            IEntityUpdated<TEntity> msg)
        {
            var existingEntity = cacheViewModel.EntitySet.FirstOrDefault(x => x.Id == msg.Entity.Id);
            if (existingEntity != null) cacheViewModel.EntitySet.Remove(existingEntity);

            cacheViewModel.EntitySet.Add(msg.Entity);
            cacheViewModel.EntitySet.Reset();

        }

        private static void ReloadEntitySet(IEntityCacheViewModel<TEntity> v, IEntitySetLoaded<TEntity> e)
        {
            v.EntitySet.Clear();
            v.EntitySet.AddRange(e.Entities);
            v.EntitySet.Reset();
        }


        /// 
        public static IProcessAction IntializeCacheAction => new ProcessAction(
            action: async cp =>
                    await Task.Run(() => new LoadEntitySet<TEntity>(
                        new StateCommandInfo(3, Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                        cp.Actor.Process, cp.Actor.Source)),
            processInfo:
                cp =>
                    new StateCommandInfo(cp.Actor.Process.Id,
                        Context.EntityView.Commands.LoadEntityViewSetWithChanges),
            // take shortcut cud be IntialState
            expectedSourceType: new SourceType(typeof (IComplexEventService)));

    public class ComplexActions
    {
        public static ComplexEventAction IntializeCache(int processId)
        {
            return new ComplexEventAction(
                key: $"{typeof(TEntity).Name}EntityCache-1",
                processId: processId,
                events: new List<IProcessExpectedEvent>
                {
                    new ProcessExpectedEvent(key: "ProcessStarted",
                        processId: processId,
                        eventPredicate: e => e != null,
                        eventType: typeof (ISystemProcessStarted),
                        processInfo: new StateEventInfo(processId, Context.Process.Events.ProcessStarted),
                        expectedSourceType: new SourceType(typeof (IComplexEventService)))

                },
                expectedMessageType: typeof (IProcessStateMessage<IInterviewInfo>),
                action: IntializeCacheAction,
                processInfo: new StateCommandInfo(processId, Context.Process.Commands.CreateState));
        }
    }

    }


}