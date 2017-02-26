using System;
using System.Linq;
using SystemInterfaces;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;


namespace DataServices.Actors
{
    public static class GetEntityExtensions
    {
       
        public static void GetEntity<TEntity>(this IGetEntityById<TEntity> msg) where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.GetEntityById(msg);
        }

        public static void GetEntity<TEntity>(this IGetEntityWithChanges<TEntity> msg) where TEntity : class, IEntity
        {

            EF7DataContext<TEntity>.GetEntityWithChanges(msg);
        }

        public static void GetEntity<TEntityView>(this IGetEntityViewById<TEntityView> msg) where TEntityView : IEntityView
        {

            EntityViewDataContext<TEntityView>.GetEntityViewById(msg);
        }

        public static void GetEntityFromPatientResponse<TEntityView>(this IGetEntityFromPatientResponse<TEntityView> msg) where TEntityView : IEntityView
        {

            EntityViewDataContext<TEntityView>.GetEntityFromPatientResponse(msg);
        }

        public static void GetEntityViewWithChanges<TEntityView>(this IGetEntityViewWithChanges<TEntityView> msg) where TEntityView : IEntityView
        {
            EntityViewDataContext<TEntityView>.GetEntityViewWithChanges(msg);
        }

        public static void UpdateEntityViewWithChanges<TEntityView>(this IUpdateEntityViewWithChanges<TEntityView> msg) where TEntityView : IEntityView
        {
            EntityViewDataContext<TEntityView>.UpdateEntityViewWithChanges(msg);
        }

        public static void LoadEntityViewSetWithChanges<TEntityView>(this ILoadEntityViewSetWithChanges<TEntityView,IMatchType> msg) where TEntityView : IEntityView
        {
            EntityViewDataContext<TEntityView>.LoadEntityViewSetWithChanges(msg);
        }

        public static void LoadPulledEntityViewSetWithChanges<TEntityView>(this ILoadPulledEntityViewSetWithChanges<TEntityView, IMatchType> msg) where TEntityView : IEntityView
        {
            EntityViewDataContext<TEntityView>.LoadPulledEntityViewSetWithChanges(msg);
        }

        public static void UpdatePulledEntityWithChanges<TEntity>(this IUpdatePatientEntityWithChanges<TEntity> msg) where TEntity : IEntity
        {
            PulledEntityDataContext.UpdatePulledEntityWithChanges(msg);
        }


    }
}