using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Windows;
using SystemInterfaces;
using EventMessages;
using Interfaces;
using JB.Reactive.ExtensionMethods;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewMessages;
using ViewModel.Interfaces;
using Utilities;

namespace RevolutionData
{
    public class ProcessViewModels
    {
        public static readonly List<IViewModelInfo> ProcessViewModelInfos = new List<IViewModelInfo>
        {
             new ViewModelInfo
                (
                0,// set to zero to prevent ViewActorInializing this view
                new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {   new ViewEventSubscription<IMainWindowViewModel, IViewModelCreated<IScreenModel>>(1, e => e != null, new List<Func<IMainWindowViewModel, IViewModelCreated<IScreenModel>, bool>>
                    {
                        (s, e) => s.Process.Id == e.ViewModel.Process.Id 
                    }, (s, e) =>
                    {
                        if (Application.Current == null)
                        {
                            s.BodyViewModels.Add(e.ViewModel);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                s.BodyViewModels.Add(e.ViewModel);
                            });
                        }
                    })
                },
                new List<IViewModelEventPublication<IViewModel, IEvent>>
                {
                       new ViewEventPublication<IMainWindowViewModel, ViewModelLoaded<IMainWindowViewModel,IScreenModel>>(
                            key:"ScreenModelinViewModel",
                            subject: v => v.BodyViewModels.CollectionChanges,
                            subjectPredicate: new List<Func<IMainWindowViewModel, bool>>
                                                       {
                                                           v => v.BodyViewModels.LastOrDefault() != null
                                                       },
                            messageData: s => new ViewEventPublicationParameter(new object[] {s, s.BodyViewModels.Last()},new StateEventInfo(s.Process.Id, Context.ViewModel.Events.ViewModelLoaded), s.Process, s.Source ))
                  }, 
                new List<IViewModelEventCommand<IViewModel, IEvent>>(),
                typeof(IMainWindowViewModel),
                typeof(IBodyViewModel)), 

            new ViewModelInfo
                (
                1,
                new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {   new ViewEventSubscription<IScreenModel, IViewModelCreated<IViewModel>>(1, e => e != null, new List<Func<IScreenModel, IViewModelCreated<IViewModel>, bool>>
                    {
                        (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(IHeaderViewModel)
                    }, (s, e) =>
                    {
                         if (Application.Current == null)
                        {
                            s.HeaderViewModels.Add(e.ViewModel);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(() => s.HeaderViewModels.Add(e.ViewModel));
                        }
                    }),
                    new ViewEventSubscription<IScreenModel, IViewModelCreated<IViewModel>>(1, e => e != null, new List<Func<IScreenModel, IViewModelCreated<IViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(ILeftViewModel)
                        }, (s, e) =>
                        {
                             if (Application.Current == null)
                        {
                            s.LeftViewModels.Add(e.ViewModel);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(() => s.LeftViewModels.Add(e.ViewModel));
                        }
                        }),
                    new ViewEventSubscription<IScreenModel, IViewModelCreated<IViewModel>>(1, e => e != null, new List<Func<IScreenModel, IViewModelCreated<IViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(IRightViewModel)
                        }, (s, e) =>
                        {
                             if (Application.Current == null)
                        {
                            s.RightViewModels.Add(e.ViewModel);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(() => s.RightViewModels.Add(e.ViewModel));
                        }
                        }),
                    new ViewEventSubscription<IScreenModel, IViewModelCreated<IViewModel>>(
                        1, e => e != null, new List<Func<IScreenModel, IViewModelCreated<IViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(IFooterViewModel)
                        }, (s, e) =>
                        {
                             if (Application.Current == null)
                        {
                            s.FooterViewModels.Add(e.ViewModel);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(() => s.FooterViewModels.Add(e.ViewModel));
                        }
                        }),
                    new ViewEventSubscription<IScreenModel, IViewModelCreated<IViewModel>>(1, e => e != null, new List<Func<IScreenModel, IViewModelCreated<IViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(IBodyViewModel)
                        }, (s, e) =>
                        {
                             if (Application.Current == null)
                        {
                            s.BodyViewModels.Add(e.ViewModel);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(() => s.BodyViewModels.Add(e.ViewModel));
                        }
                        })
                },
                new List<IViewModelEventPublication<IViewModel, IEvent>>
                {
                     new ViewEventPublication<IScreenModel, ViewModelLoaded<IScreenModel,IViewModel>>(
                         key:"ScreenModelBody",
                         subject:v => v.BodyViewModels.CollectionChanges,
                         subjectPredicate:new List<Func<IScreenModel, bool>>
                                             {
                                                 v => v.BodyViewModels.LastOrDefault() != null
                                             },
                        messageData:s => new ViewEventPublicationParameter(new object[] {s, s.BodyViewModels.Last()},new StateEventInfo(s.Process.Id, Context.ViewModel.Events.ViewModelLoaded),s.BodyViewModels.Last().Process,s.Source))
                },
                new List<IViewModelEventCommand<IViewModel, IEvent>>(),
                typeof(IScreenModel),
                typeof(IBodyViewModel)),

            ////////////////////////////////////User Login Screen/////////////////////////////////////////////////

            new ViewModelInfo
                (
                2, new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {
                   new ViewEventSubscription<ILoginViewModel, IProcessStateMessage<ISignInInfo>>(
                       2, e => e != null, new List<Func<ILoginViewModel, IProcessStateMessage<ISignInInfo>, bool>>(), (v,e) => v.State.Value = e.State
                       )

                }, new List<IViewModelEventPublication<IViewModel, IEvent>>
                {
                   


                     new ViewEventPublication<ILoginViewModel, ViewStateLoaded<ILoginViewModel,IProcessState<ISignInInfo>>>(
                         key:"ILoginModelViewStateLoaded", 
                         subject:v => v.State,
                         subjectPredicate:new List<Func<ILoginViewModel, bool>>
                                             {
                                                 v => v.State != null
                                             },
                         messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
                }, new List<IViewModelEventCommand<IViewModel,IEvent>>
                {
                   
                     new ViewEventCommand<ILoginViewModel, GetEntityViewWithChanges<ISignInInfo>>(
                        key:"UserName",
                        subject:v => v.ChangeTracking.DictionaryChanges,
                        commandPredicate: new List<Func<ILoginViewModel, bool>>
                                            {
                                                v => v.ChangeTracking.Keys.Contains(nameof(v.State.Value.Entity.Usersignin)) && v.ChangeTracking.Keys.Count == 1
                                            },
                        messageData: s => new ViewEventCommandParameter(new object[] {s.State.Value.Entity.Id,s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView),s.Process,s.Source)),

                    new ViewEventCommand<ILoginViewModel, GetEntityViewWithChanges<ISignInInfo>>(
                                key:"ValidateUserInfo",
                                commandPredicate:new List<Func<ILoginViewModel, bool>>
                                            {
                                                    v => v.ChangeTracking.Values.Contains(nameof(v.State.Value.Entity.Usersignin))
                                                    
                                            },
                                subject:s => s.Commands.GetValueOrNull("ValidateUserInfo").AsObservable(),
                               
                                messageData:s => new ViewEventCommandParameter(new object[] {s.State.Value.Entity.Id,s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView),s.Process,s.Source)),

                    

                }, typeof(ILoginViewModel), typeof(IBodyViewModel))

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
