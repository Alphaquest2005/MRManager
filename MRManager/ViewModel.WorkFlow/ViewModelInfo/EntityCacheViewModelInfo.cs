using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SystemInterfaces;
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
                orientation: typeof (ICacheViewModel),
                priority:0);
        }


       
        private static void UpdateEntitySet(IEntityCacheViewModel<TEntity> cacheViewModel,
            IEntityUpdated<TEntity> msg)
        {
            var existingEntity = cacheViewModel.EntitySet.Value.FirstOrDefault(x => x.Id == msg.Entity.Id);
            if (existingEntity != null) cacheViewModel.EntitySet.Value.Remove(existingEntity);

            cacheViewModel.EntitySet.Value.Add(msg.Entity);
            cacheViewModel.EntitySet.Value.Reset();

        }

        private static void ReloadEntitySet(IEntityCacheViewModel<TEntity> v, IEntitySetLoaded<TEntity> e)
        {
            v.EntitySet.Value.Clear();
            v.EntitySet.Value.AddRange(e.Entities);
            v.EntitySet.Value.Reset();
        }


       

   

    }


}