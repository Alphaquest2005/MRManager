using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SystemInterfaces;
using SystemMessages;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using RevolutionEntities.ViewModels;
using Utilities;
using ViewMessages;
using ViewModel.Interfaces;
using ViewModelInterfaces;
using ViewModels;

namespace DataServices.Actors
{
    public class ProcessViewModels
    {
        public static readonly List<IViewModelInfo> ProcessViewModelInfos = new List<IViewModelInfo>()
        {
            new ViewModelInfo
                (
                1,
                new List<IViewModelEventSubscription<IViewModel, IEvent>>()
                {   new ViewEventSubscription<ScreenModel, ViewModelCreated<IHeaderViewModel>>(
                    processId: 1,
                    eventPredicate: (e) => e != null,
                    actionPredicate: new List<Func<ScreenModel, ViewModelCreated<IHeaderViewModel>, bool>>
                    {
                        (s, e) => s.Process.Id != e.ViewModel.Process.Id
                    },
                    action: (s, e) => s.HeaderViewModels.Add(e.ViewModel)),
                    new ViewEventSubscription<ScreenModel, ViewModelCreated<ILeftViewModel>>(
                        processId: 1,
                        eventPredicate: (e) => e != null,
                        actionPredicate: new List<Func<ScreenModel, ViewModelCreated<ILeftViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id
                        },
                        action: (s, e) => s.LeftViewModels.Add(e.ViewModel)),
                    new ViewEventSubscription<ScreenModel, ViewModelCreated<IRightViewModel>>(
                        processId: 1,
                        eventPredicate: (e) => e != null,
                        actionPredicate: new List<Func<ScreenModel, ViewModelCreated<IRightViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id
                        },
                        action: (s, e) => s.RightViewModels.Add(e.ViewModel)),
                    new ViewEventSubscription<ScreenModel, ViewModelCreated<IFooterViewModel>>(
                        processId:1,
                        eventPredicate: (e) => e != null,
                        actionPredicate: new List<Func<ScreenModel, ViewModelCreated<IFooterViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id
                        },
                        action: (s, e) => s.FooterViewModels.Add(e.ViewModel)),
                    new ViewEventSubscription<ScreenModel, ViewModelCreated<IBodyViewModel>>(
                        processId: 1,
                        eventPredicate: (e) => e != null,
                        actionPredicate: new List<Func<ScreenModel, ViewModelCreated<IBodyViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id
                        },
                        action: (s, e) => s.BodyViewModels.Add(e.ViewModel)),
                },
                new List<IViewModelEventPublication<IViewModel, IEvent>>(),
                new List<IViewModelEventCommand<IViewModel, IEvent>>(), 
                typeof(ScreenModel)),

            ////////////////////////////////////User Login Screen/////////////////////////////////////////////////

            new ViewModelInfo
                (
                2,
                new List<IViewModelEventSubscription<IViewModel, IEvent>>()
                {
                   new ViewEventSubscription<LoginViewModel, ProcessStateMessage<UserSignIn>>(
                       processId:2,
                       eventPredicate: e => true,
                       actionPredicate: new List<Func<LoginViewModel, ProcessStateMessage<UserSignIn>, bool>>(),
                       action: (v,e) => v.CurrentEntity.Value = e.State.Entity
                       ),

                },new List<IViewModelEventPublication<IViewModel, IEvent>>()
                {
                    new ViewEventPublication<LoginViewModel, EntityChanges<UserSignIn>>(
                        subject: v => v.ChangeTracking.DictionaryChanges,
                        subjectPredicate: new List<Func<LoginViewModel, bool>>()
                                        {
                                            v => v.ChangeTracking.Keys.Contains("UserName") && v.ChangeTracking.Keys.Count == 1
                                        },
                        messageData:new List<Func<LoginViewModel, dynamic>>()
                                                        {
                                                            (s) => s.CurrentEntity.Value.Id,
                                                            (s) => s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                                                        }
                        )
                },
                new List<IViewModelEventCommand<IViewModel, IEvent>>()
                {
                    
                },
                typeof(LoginViewModel)),

            //////////////////////////////////////Entity ViewModels ///////////////////////////////////////////////


            //new WriteEntityViewModelInfo<IAddressCities>(
            //    processId: 3,
            //    viewModelType: typeof (WriteEntityViewModel<IAddressCities>),
            //    createEntityAction: () => new AddressCities()
            //    {
            //        CityId = CacheViewModel<ICities>.Instance.CurrentEntity.Id,
            //        Id = CacheViewModel<IAddresses>.Instance.CurrentEntity.Id,
            //        RowState = DataInterfaces.RowState.Added
            //    },
            //    createNullEntityAction: () => new AddressCities() {Id = EntityStates.NullEntity},
            //    viewModelEventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>
            //    {
            //        new ViewEventSubscription<WriteEntityViewModel<IAddressCities>, CurrentEntityChanged<ICities>>(
            //            processId: 3,
            //            eventPredicate: (e) => e.EntityId != EntityStates.NullEntity,
            //            actionPredicate: new List<Func<WriteEntityViewModel<IAddressCities>, CurrentEntityChanged<ICities>, bool>>
            //            {
            //                (s, e) => s.CurrentEntity.Id != e.EntityId
            //            },
            //            action: (s, e) =>
            //                s.FilterExpression =
            //                    new List<Expression<Func<IAddressCities, bool>>>() {x => x.CityId == e.EntityId}),


            //        new ViewEventSubscription<WriteEntityViewModel<IAddressCities>, CurrentEntityChanged<IAddresses>>(
            //            processId: 3,
            //            eventPredicate: (e) => e.EntityId != EntityStates.NullEntity,
            //            actionPredicate: new List<Func<WriteEntityViewModel<IAddressCities>, CurrentEntityChanged<IAddresses>, bool>>
            //            {
            //                (s, e) => s.CurrentEntity.Id != e.EntityId
            //            },
            //            action: (s, e) =>
            //                s.FilterExpression =
            //                    new List<Expression<Func<IAddressCities, bool>>>() {x => x.Id == e.EntityId})
            //    },
            //    viewModelEventPublications:new List<IViewModelEventPublication<IViewModel, IEvent>>(),
            //    viewModelCommands: new List<IViewModelEventCommand<IViewModel, IEvent>>()),
            //new ReadEntityViewModelInfo<IAddressCities>(processId: 3,
            //    viewModelType: typeof (CacheViewModel<IAddressCities>),
            //    viewModelEventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>
            //    {
            //        new ViewEventSubscription<CacheViewModel<IAddressCities>, EntitySetLoaded<IAddressCities>>(
            //            processId: 3,
            //            eventPredicate: (e) => e != null,
            //            actionPredicate: new List<Func<CacheViewModel<IAddressCities>, EntitySetLoaded<IAddressCities>, bool>>
            //            {
            //                (s, e) => s.Process.Id == e.Process.Id
            //            },
            //            action: (s, e) => s.HandleEntitySetLoaded(e.Entities)),
            //        new ViewEventSubscription<CacheViewModel<IAddressCities>, CurrentEntityUpdated<IAddressCities>>(
            //            processId: 3,
            //            eventPredicate: (e) => e != null,
            //            actionPredicate: new List<Func<CacheViewModel<IAddressCities>, CurrentEntityUpdated<IAddressCities>, bool>>
            //            {
            //                (s, e) => s.Process.Id == e.Process.Id
            //            },
            //            action: (s, e) => s.HandleCurrentEntityUpdated(e.Entity)),
            //        new ViewEventSubscription<CacheViewModel<IAddressCities>, CurrentEntityChanged<IAddressCities>>(
            //            processId: 3,
            //            eventPredicate: (e) => e != null,
            //            actionPredicate: new List<Func<CacheViewModel<IAddressCities>, CurrentEntityChanged<IAddressCities>, bool>>
            //            {
            //                (s, e) => s.Process.Id == e.Process.Id
            //            },
            //            action: (s, e) => s.HandleCurrentEntityChanged(e.EntityId)),
            //        new ViewEventSubscription<CacheViewModel<IAddressCities>, ServiceStarted<LoadEntitySet<IAddressCities>>>(
            //            processId:3,
            //            eventPredicate: (e) => e != null,
            //            actionPredicate: new List<Func<CacheViewModel<IAddressCities>, ServiceStarted<LoadEntitySet<IAddressCities>>, bool>>
            //            {
            //                (s, e) => s.Process.Id == e.Process.Id
            //            },
            //            action: (s, e) => EventMessageBus.Current.Publish(new LoadEntitySet<IAddressCities>(e.Process,s.MsgSource), s.MsgSource)),
            //        new ViewEventSubscription<CacheViewModel<IAddressCities>, EntityCreated<IAddressCities>>(
            //            processId: 3,
            //            eventPredicate: (e) => e != null,
            //            actionPredicate: new List<Func<CacheViewModel<IAddressCities>, EntityCreated<IAddressCities>, bool>>
            //            {
            //                (s, e) => s.Process.Id == e.Process.Id
            //            },
            //            action: (s, e) => s.HandleEntityCreated(e.Entity)),
            //        new ViewEventSubscription<CacheViewModel<IAddressCities>, EntityDeleted<IAddressCities>>(
            //            processId: 3,
            //            eventPredicate: (e) => e != null,
            //            actionPredicate: new List<Func<CacheViewModel<IAddressCities>, EntityDeleted<IAddressCities>, bool>>
            //            {
            //                (s, e) => s.Process.Id == e.Process.Id
            //            },
            //            action: (s, e) => s.HandleEntityDeleted(e.EntityId)),
                
            //    },
            //    viewModelEventPublications:new List<IViewModelEventPublication<IViewModel, IEvent>>(),
            //    viewModelCommands:new List<IViewModelEventCommand<IViewModel, IEvent>>()),
       
        };
    }
}