using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using EventMessages;
using EventMessages.Events;
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
                    new ViewEventSubscription<IScreenModel, IViewModelCreated<IViewModel>>(
                        1,
                        e => e != null,
                        new List<Func<IScreenModel, IViewModelCreated<IViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id && e.ViewModel.Orientation == typeof(ILeftViewModel)
                        },
                        (s, e) =>
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
                        messageData:s => new ViewEventPublicationParameter(new object[] {s, s.BodyViewModels.Last()},new StateEventInfo(s.Process.Id, Context.ViewModel.Events.ViewModelLoaded),s.BodyViewModels.Last().Process,s.Source)),

                    new ViewEventPublication<IScreenModel, ViewModelLoaded<IScreenModel,IViewModel>>(
                         key:"ScreenModelLeft",
                         subject:v => v.LeftViewModels.CollectionChanges,
                         subjectPredicate:new List<Func<IScreenModel, bool>>
                                             {
                                                 v => v.LeftViewModels.LastOrDefault() != null
                                             },
                        messageData:s => new ViewEventPublicationParameter(new object[] {s, s.LeftViewModels.Last()},new StateEventInfo(s.Process.Id, Context.ViewModel.Events.ViewModelLoaded),s.LeftViewModels.Last().Process,s.Source)),

                    new ViewEventPublication<IScreenModel, ViewModelLoaded<IScreenModel,IViewModel>>(
                         key:"ScreenModelHeader",
                         subject:v => v.HeaderViewModels.CollectionChanges,
                         subjectPredicate:new List<Func<IScreenModel, bool>>
                                             {
                                                 v => v.HeaderViewModels.LastOrDefault() != null
                                             },
                        messageData:s => new ViewEventPublicationParameter(new object[] {s, s.HeaderViewModels.Last()},new StateEventInfo(s.Process.Id, Context.ViewModel.Events.ViewModelLoaded),s.HeaderViewModels.Last().Process,s.Source)),

                    new ViewEventPublication<IScreenModel, ViewModelLoaded<IScreenModel,IViewModel>>(
                         key:"ScreenModelRight",
                         subject:v => v.RightViewModels.CollectionChanges,
                         subjectPredicate:new List<Func<IScreenModel, bool>>
                                             {
                                                 v => v.RightViewModels.LastOrDefault() != null
                                             },
                        messageData:s => new ViewEventPublicationParameter(new object[] {s, s.RightViewModels.Last()},new StateEventInfo(s.Process.Id, Context.ViewModel.Events.ViewModelLoaded),s.RightViewModels.Last().Process,s.Source)),

                    new ViewEventPublication<IScreenModel, ViewModelLoaded<IScreenModel,IViewModel>>(
                         key:"ScreenModelFooter",
                         subject:v => v.FooterViewModels.CollectionChanges,
                         subjectPredicate:new List<Func<IScreenModel, bool>>
                                             {
                                                 v => v.FooterViewModels.LastOrDefault() != null
                                             },
                        messageData:s => new ViewEventPublicationParameter(new object[] {s, s.FooterViewModels.Last()},new StateEventInfo(s.Process.Id, Context.ViewModel.Events.ViewModelLoaded),s.FooterViewModels.Last().Process,s.Source)),


                },
                new List<IViewModelEventCommand<IViewModel, IEvent>>(),
                typeof(IScreenModel),
                typeof(IBodyViewModel)),

            ////////////////////////////////////User Login Screen/////////////////////////////////////////////////

            new ViewModelInfo
                (
                2, new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {
                   new ViewEventSubscription<ISigninViewModel, IProcessStateMessage<ISignInInfo>>(
                       2, e => e != null, new List<Func<ISigninViewModel, IProcessStateMessage<ISignInInfo>, bool>>(), (v,e) => v.State.Value = e.State
                       )

                }, new List<IViewModelEventPublication<IViewModel, IEvent>>
                {
                   


                     new ViewEventPublication<ISigninViewModel, ViewStateLoaded<ISigninViewModel,IProcessState<ISignInInfo>>>(
                         key:"ILoginModelViewStateLoaded", 
                         subject:v => v.State,
                         subjectPredicate:new List<Func<ISigninViewModel, bool>>
                                             {
                                                 v => v.State != null
                                             },
                         messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
                }, new List<IViewModelEventCommand<IViewModel,IEvent>>
                {
                   
                     new ViewEventCommand<ISigninViewModel, GetEntityViewWithChanges<ISignInInfo>>(
                        key:"UserName",
                        subject:v => v.ChangeTracking.DictionaryChanges,
                        commandPredicate: new List<Func<ISigninViewModel, bool>>
                                            {
                                                v => v.ChangeTracking.Keys.Contains(nameof(ISignInInfo.Usersignin)) && v.ChangeTracking.Keys.Count == 1
                                            },
                        messageData: s => new ViewEventCommandParameter(new object[] {s.State.Value.Entity.Id,s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView),s.Process,s.Source)),

                    new ViewEventCommand<ISigninViewModel, GetEntityViewWithChanges<ISignInInfo>>(
                                key:"ValidateUserInfo",
                                commandPredicate:new List<Func<ISigninViewModel, bool>>
                                            {
                                                   // v => v.ChangeTracking.Keys.Contains(nameof(ISignInInfo.Usersignin))
                                                    
                                            },
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                               
                                messageData:s => new ViewEventCommandParameter(new object[] {s.State.Value.Entity.Id,s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView),s.Process,s.Source)),

                    

                }, typeof(ISigninViewModel), typeof(IBodyViewModel)),



            //////////////////////////////////////Patient Info ///////////////////////////////////////////////


           new ViewModelInfo
                (
                3, new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {
                   new ViewEventSubscription<IPatientSummaryListViewModel, IProcessStateListMessage<IPatientInfo>>(
                       3,
                       e => e != null,
                       new List<Func<IPatientSummaryListViewModel, IProcessStateListMessage<IPatientInfo>, bool>>(),
                       (v,e) => v.State.Value = e.State),

                },
                new List<IViewModelEventPublication<IViewModel, IEvent>>
                {
                    new ViewEventPublication<IPatientSummaryListViewModel, ViewStateLoaded<IPatientSummaryListViewModel,IProcessStateList<IPatientInfo>>>(
                         key:"IPatientInfoViewStateLoaded",
                         subject:v => v.State,
                         subjectPredicate:new List<Func<IPatientSummaryListViewModel, bool>>
                                             {
                                                 v => v.State != null
                                             },
                         messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                    new ViewEventPublication<IPatientSummaryListViewModel, CurrentEntityChanged<IPatientInfo>>(
                         key:"CurrentEntityChanged",
                         subject:v => v.WhenAnyValue(z => z.CurrentEntity),
                         subjectPredicate:new List<Func<IPatientSummaryListViewModel, bool>>{},
                         messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
                },
                new List<IViewModelEventCommand<IViewModel,IEvent>>
                {

                    
                    new ViewEventCommand<IPatientSummaryListViewModel, LoadEntityViewSetWithChanges<IPatientInfo>>(
                                key:"Search",
                                commandPredicate:new List<Func<IPatientSummaryListViewModel, bool>>
                                            {
                                                    v => v.ChangeTracking.Values.Count > 0

                                            },
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData:s => new ViewEventCommandParameter(new object[] {s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.LoadEntityViewSetWithChanges),s.Process,s.Source)),



                },
                typeof(IPatientSummaryListViewModel),
                typeof(ILeftViewModel))

        };
    }


}
