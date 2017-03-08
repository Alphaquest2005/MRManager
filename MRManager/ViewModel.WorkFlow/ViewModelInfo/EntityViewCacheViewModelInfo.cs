using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SystemInterfaces;
using RevolutionEntities.ViewModels;
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
                viewInfo: new ViewInfo($"{typeof(TView).Name}CacheViewModel", "", ""),
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
                                Application.Current.Dispatcher.BeginInvoke(new Action(() => ReloadEntitySet(v, e)));
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
                                Application.Current.Dispatcher.BeginInvoke(new Action(() => UpdateEntitySet(v, e)));
                            }
                        }),

                     new ViewEventSubscription<IEntityViewCacheViewModel<TView>, ICurrentEntityChanged<TView>>(
                            3,
                            e => e != null,
                            new List<Func<IEntityViewCacheViewModel<TView>, ICurrentEntityChanged<TView>, bool>>(),
                            (v,e) => v.CurrentEntity.Value = e.Entity),


                },
                publications: new List<IViewModelEventPublication<IViewModel, IEvent>> {},
                commands: new List<IViewModelEventCommand<IViewModel, IEvent>> {},
                viewModelType: typeof (IEntityViewCacheViewModel<TView>),
                orientation: typeof (ICacheViewModel),
                priority:0);
        }

        private static void UpdateEntitySet(IEntityViewCacheViewModel<TView> cacheViewModel,
            IEntityViewWithChangesUpdated<TView> msg)
        {
            var existingEntity = cacheViewModel.EntitySet.Value.FirstOrDefault(x => x.Id == msg.Entity.Id);
            if (existingEntity != null) cacheViewModel.EntitySet.Value.Remove(existingEntity);

            cacheViewModel.EntitySet.Value.Add(msg.Entity);
            cacheViewModel.EntitySet.Value.Reset();

        }

        private static void ReloadEntitySet(IEntityViewCacheViewModel<TView> v, IEntityViewSetWithChangesLoaded<TView> e)
        {
            v.EntitySet.Value.Clear();
            v.EntitySet.Value.AddRange(e.EntitySet);
            v.EntitySet.Value.Reset();
        }

       
    }
}