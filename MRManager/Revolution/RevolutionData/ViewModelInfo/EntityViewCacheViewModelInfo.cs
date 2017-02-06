using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using Actor.Interfaces;
using EventMessages.Commands;
using EventMessages.Events;
using Interfaces;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewMessages;
using ViewModel.Interfaces;

namespace RevolutionData
{
    
    public class EntityViewCacheViewModelInfo<TView> where TView : IEntityView
    {
        public static ViewModelInfo CacheViewModel(int processId)
        {
            return new ViewModelInfo
                (
                processId: processId,
                subscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {
                    new ViewEventSubscription<IEntityViewCacheViewModel<TView>, IEntityViewSetWithChangesLoaded<TView>>(
                        processId: processId,
                        eventPredicate: e => e.Changes.Count == 0,
                        actionPredicate: new List<Func<IEntityViewCacheViewModel<TView>, IEntityViewSetWithChangesLoaded<TView>, bool>>(),
                        action: (v, e) =>
                        {
                            if (Application.Current == null)
                            {
                                ReloadEntitySet(v, e);
                            }
                            else
                            {
                                Application.Current.Dispatcher.Invoke(() => ReloadEntitySet(v, e));
                            }
                        }),

                    new ViewEventSubscription<IEntityViewCacheViewModel<TView>, IEntityViewWithChangesUpdated<TView>>(
                        processId: processId,
                        eventPredicate: e => e.Changes.Count > 0,
                        actionPredicate: new List<Func<IEntityViewCacheViewModel<TView>, IEntityViewWithChangesUpdated<TView>, bool>>(),
                        action: (v, e) =>
                        {
                            if (Application.Current == null)
                            {
                                UpdateEntitySet(v, e);
                            }
                            else
                            {
                                Application.Current.Dispatcher.Invoke(() => UpdateEntitySet(v, e));
                            }
                        }),


                },
                publications: new List<IViewModelEventPublication<IViewModel, IEvent>> {},
                commands: new List<IViewModelEventCommand<IViewModel, IEvent>> {},
                viewModelType: typeof (IEntityViewCacheViewModel<TView>),
                orientation: typeof (ICacheViewModel));
        }

        private static void UpdateEntitySet(IEntityViewCacheViewModel<TView> cacheViewModel,
            IEntityViewWithChangesUpdated<TView> msg)
        {
            var existingEntity = cacheViewModel.EntitySet.FirstOrDefault(x => x.Id == msg.Entity.Id);
            if (existingEntity != null) cacheViewModel.EntitySet.Remove(existingEntity);

            cacheViewModel.EntitySet.Add(msg.Entity);
            cacheViewModel.EntitySet.Reset();

        }

        private static void ReloadEntitySet(IEntityViewCacheViewModel<TView> v, IEntityViewSetWithChangesLoaded<TView> e)
        {
            v.EntitySet.Clear();
            v.EntitySet.AddRange(e.EntitySet);
            v.EntitySet.Reset();
        }

        public static class ComplexActions
        {

            public static ComplexEventAction IntializeCache(int processId)
            {
                return new ComplexEventAction(
                    key: $"{typeof (TView).Name}EntityViewCache-1",
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

            
                /// 
                public static IProcessAction IntializeCacheAction => new ProcessAction(
                    action:
                        cp =>
                            new LoadEntityViewSetWithChanges<TView, IExactMatch>(new Dictionary<string, dynamic>(),
                                new StateCommandInfo(3, Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                                cp.Actor.Process, cp.Actor.Source),
                    processInfo:
                        cp =>
                            new StateCommandInfo(cp.Actor.Process.Id,
                                Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                    // take shortcut cud be IntialState
                    expectedSourceType: new SourceType(typeof (IComplexEventService)));

        }
    }
}