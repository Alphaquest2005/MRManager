using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SystemInterfaces;
using EventMessages;
using Interfaces;
using JB.Reactive.ExtensionMethods;
using ReactiveUI;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class ProcessViewModels
    {
        public static readonly List<IViewModelInfo> ProcessViewModelInfos = new List<IViewModelInfo>()
        {
            new ViewModelInfo
                (
                1,
                new List<IViewModelEventSubscription<IViewModel, IEvent>>()
                {   new ViewEventSubscription<IScreenViewModel, IViewModelCreated<IViewModel>>(
                    processId: 1,
                    eventPredicate: (e) => e != null,
                    actionPredicate: new List<Func<IScreenViewModel, IViewModelCreated<IViewModel>, bool>>
                    {
                        (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(IHeaderViewModel)
                    },
                    action: (s, e) => s.HeaderViewModels.Add(e.ViewModel)),
                    new ViewEventSubscription<IScreenViewModel, IViewModelCreated<IViewModel>>(
                        processId: 1,
                        eventPredicate: (e) => e != null,
                        actionPredicate: new List<Func<IScreenViewModel, IViewModelCreated<IViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(ILeftViewModel)
                        },
                        action: (s, e) => s.LeftViewModels.Add(e.ViewModel)),
                    new ViewEventSubscription<IScreenViewModel, IViewModelCreated<IViewModel>>(
                        processId: 1,
                        eventPredicate: (e) => e != null,
                        actionPredicate: new List<Func<IScreenViewModel, IViewModelCreated<IViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(IRightViewModel)
                        },
                        action: (s, e) => s.RightViewModels.Add(e.ViewModel)),
                    new ViewEventSubscription<IScreenViewModel, IViewModelCreated<IViewModel>>(
                        processId:1,
                        eventPredicate: (e) => e != null,
                        actionPredicate: new List<Func<IScreenViewModel, IViewModelCreated<IViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(IFooterViewModel)
                        },
                        action: (s, e) => s.FooterViewModels.Add(e.ViewModel)),
                    new ViewEventSubscription<IScreenViewModel, IViewModelCreated<IViewModel>>(
                        processId: 1,
                        eventPredicate: (e) => e != null,
                        actionPredicate: new List<Func<IScreenViewModel, IViewModelCreated<IViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(IBodyViewModel)
                        },
                        action: (s, e) => Application.Current.Dispatcher.Invoke(() => s.BodyViewModels.Add(e.ViewModel))),
                },
                new List<IViewModelEventPublication<IViewModel, IEvent>>(),
                new List<IViewModelEventCommand<IViewModel, IEvent>>(),
                typeof(IScreenViewModel),
                typeof(IBodyViewModel)),

            ////////////////////////////////////User Login Screen/////////////////////////////////////////////////

            new ViewModelInfo
                (
                processId:2,
                subscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>()
                {
                   new ViewEventSubscription<ILoginViewModel, ProcessStateMessage<IUserSignIn>>(
                       processId:2,
                       eventPredicate: e => e != null,
                       actionPredicate: new List<Func<ILoginViewModel, ProcessStateMessage<IUserSignIn>, bool>>(),
                       action: (v,e) => v.CurrentEntity.Value = e.State.Entity
                       ),

                },
                publications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
                {
                    new ViewEventPublication<ILoginViewModel, GetEntityWithChanges<IUserSignIn>>(
                        subject: v => v.ChangeTracking.DictionaryChanges,
                        subjectPredicate: new List<Func<ILoginViewModel, bool>>()
                                        {
                                            v => v.ChangeTracking.Keys.Contains(nameof(v.CurrentEntity.Value.Username)) && v.ChangeTracking.Keys.Count == 1
                                        },
                        messageData:new List<Func<ILoginViewModel, dynamic>>()
                                        {
                                            (s) => s.CurrentEntity.Value.Id,
                                            (s) => s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                                        }
                        )
                },
                commands: new List<IViewModelEventCommand<IViewModel,IEvent>>()
                {
                    new ViewEventCommand<ILoginViewModel, GetEntityWithChanges<IUserSignIn>>("ValidateUserInfo",
                        v =>
                            v.ChangeTracking.WhenAny(x => x.Keys,
                                x => x.Value.Contains(nameof(v.CurrentEntity.Value.Password)) && x.Value.Contains(nameof(v.CurrentEntity.Value.Username))),
                        subject: (s) => ((ReactiveCommand<IViewModel, Unit>) s.Commands["ValidateUserInfo"]).AsObservable(),
                        subjectPredicate: new List<Func<ILoginViewModel, bool>>()
                        {
                            (v) => v.ChangeTracking.Keys.Count > 2
                        },
                        messageData: new List<Func<ILoginViewModel, dynamic>>()
                        {
                            (s) => s.CurrentEntity?.Value.Id,
                            (s) => s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                        }),

                },
                viewModelType: typeof(ILoginViewModel),
                orientation: typeof(IBodyViewModel)),

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
