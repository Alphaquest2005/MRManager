using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using EventMessages;
using EventMessages.Commands;
using EventMessages.Events;
using Interfaces;
using JB.Reactive.ExtensionMethods;
using MoreLinq;
using Reactive.Bindings;
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
                {
                     new ViewEventSubscription<IScreenModel, NavigateToView>(
                        1,
                        e => e != null,
                        new List<Func<IScreenModel, NavigateToView, bool>>{},
                        (s, e) =>
                        {
                            s.Slider.BringIntoView(e.View);
                        }),

                    new ViewEventSubscription<IScreenModel, IViewModelCreated<IViewModel>>(1, e => e != null, new List<Func<IScreenModel, IViewModelCreated<IViewModel>, bool>>
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
                        }),

                    new ViewEventSubscription<IScreenModel, ICleanUpSystemProcess>(
                        1,
                        e => e != null,
                        new List<Func<IScreenModel, ICleanUpSystemProcess, bool>>{},
                        (s, e) =>
                        {
                            if (Application.Current == null)
                            {
                                s.BodyViewModels.RemoveRange(s.BodyViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                                s.LeftViewModels.RemoveRange(s.LeftViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                                s.HeaderViewModels.RemoveRange(s.HeaderViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                                s.RightViewModels.RemoveRange(s.RightViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                                s.FooterViewModels.RemoveRange(s.FooterViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                            }
                            else
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    s.BodyViewModels.RemoveRange(s.BodyViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                                    s.BodyViewModels.Reset();
                                    s.LeftViewModels.RemoveRange(s.LeftViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                                    s.LeftViewModels.Reset();
                                    s.HeaderViewModels.RemoveRange(s.HeaderViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                                    s.HeaderViewModels.Reset();
                                    s.RightViewModels.RemoveRange(s.RightViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                                    s.RightViewModels.Reset();
                                    s.FooterViewModels.RemoveRange(s.FooterViewModels.Where(x => x.Process.Id == e.ProcessToBeCleanedUpId));
                                    s.FooterViewModels.Reset();
                                });
                            }
                        }),
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
                                                   v => v.ChangeTracking.Keys.Contains(nameof(ISignInInfo.Usersignin))
                                                    
                                            },
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                               
                                messageData:s => new ViewEventCommandParameter(new object[] {s.State.Value.Entity.Id,s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView),s.Process,s.Source)),

                    

                }, typeof(ISigninViewModel), typeof(IBodyViewModel)),



            //////////////////////////////////////Patient Info ///////////////////////////////////////////////

            //////////////////Summary List
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
                         subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                         subjectPredicate:new List<Func<IPatientSummaryListViewModel, bool>>{},
                         messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
                },
                new List<IViewModelEventCommand<IViewModel,IEvent>>
                {


                    new ViewEventCommand<IPatientSummaryListViewModel, LoadEntityViewSetWithChanges<IPatientInfo,IPartialMatch>>(
                                key:"Search",
                                commandPredicate:new List<Func<IPatientSummaryListViewModel, bool>>
                                            {
                                                    v => v.ChangeTracking.Values.Count > 0

                                            },
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                    //ToDo: bad practise
                                    if (!string.IsNullOrEmpty(((dynamic)s).Field) && !string.IsNullOrEmpty(((dynamic) s).Value))
                                    {
                                        s.ChangeTracking.AddOrUpdate(((dynamic) s).Field, ((dynamic) s).Value);
                                    }

                                    return new ViewEventCommandParameter(
                                        new object[] {s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.EntityView.Commands.LoadEntityViewSetWithChanges), s.Process,
                                        s.Source);
                                }),



                },
                typeof(IPatientSummaryListViewModel),
                typeof(ILeftViewModel)),

              //////////////////Patient Details
           new ViewModelInfo
                (
                processId: 3,
                subscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>
                                {
                                   new ViewEventSubscription
                                                <IPatientDetailsViewModel,
                                                IProcessStateMessage<IPatientDetailsInfo>>(
                                                   processId: 3,
                                                   eventPredicate: e => e != null,
                                                   actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientDetailsInfo>, bool>>(),
                                                   action: (v, e) => v.State.Value = e.State),
                                   

                                },
                publications: new List<IViewModelEventPublication<IViewModel, IEvent>>
                                {
                                    new ViewEventPublication<IPatientDetailsViewModel, ViewStateLoaded<IPatientDetailsViewModel,IProcessState<IPatientDetailsInfo>>>(
                                         key:"PatientDetailsViewStateLoaded",
                                         subject:v => v.State,
                                         subjectPredicate:new List<Func<IPatientDetailsViewModel, bool>>
                                                             {
                                                                 v => v.State != null
                                                             },
                                         messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),


                                },
                commands: new List<IViewModelEventCommand<IViewModel,IEvent>>{},
                viewModelType: typeof(IPatientDetailsViewModel),
                orientation: typeof(IBodyViewModel)),

             //////////////////InterviewInfo List
           new ViewModelInfo
                (
                3, new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {
                   new ViewEventSubscription<IInterviewListViewModel, IProcessStateListMessage<IInterviewInfo>>(
                       3,
                       e => e != null,
                       new List<Func<IInterviewListViewModel, IProcessStateListMessage<IInterviewInfo>, bool>>(),
                       (v,e) => v.State.Value = e.State),

                },
                new List<IViewModelEventPublication<IViewModel, IEvent>>
                {
                    new ViewEventPublication<IInterviewListViewModel, ViewStateLoaded<IInterviewListViewModel,IProcessStateList<IInterviewInfo>>>(
                         key:"IPatientInfoViewStateLoaded",
                         subject:v => v.State,
                         subjectPredicate:new List<Func<IInterviewListViewModel, bool>>
                                             {
                                                 v => v.State != null
                                             },
                         messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                    new ViewEventPublication<IInterviewListViewModel, CurrentEntityChanged<IInterviewInfo>>(
                         key:"CurrentEntityChanged",
                         subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                         subjectPredicate:new List<Func<IInterviewListViewModel, bool>>{},
                         messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
                },
                new List<IViewModelEventCommand<IViewModel,IEvent>>
                {


                    new ViewEventCommand<IInterviewListViewModel, LoadEntityViewSetWithChanges<IInterviewInfo,IPartialMatch>>(
                                key:"Search",
                                commandPredicate:new List<Func<IInterviewListViewModel, bool>>
                                            {
                                                    v => v.ChangeTracking.Values.Count > 0

                                            },
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                    //ToDo: bad practise
                                    if (!string.IsNullOrEmpty(((dynamic)s).Field) && !string.IsNullOrEmpty(((dynamic) s).Value))
                                    {
                                        s.ChangeTracking.AddOrUpdate(((dynamic) s).Field, ((dynamic) s).Value);
                                    }

                                    return new ViewEventCommandParameter(
                                        new object[] {s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.EntityView.Commands.LoadEntityViewSetWithChanges), s.Process,
                                        s.Source);
                                }),



                },
                typeof(IInterviewListViewModel),
                typeof(IBodyViewModel)),

               //////////////////Qustionaire ViewModel List
               /// 
           new ViewModelInfo
                (
                3, new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {
                   new ViewEventSubscription<IQuestionaireViewModel, IProcessStateListMessage<IPatientResponseInfo>>(
                       3,
                       e => e != null,
                       new List<Func<IQuestionaireViewModel, IProcessStateListMessage<IPatientResponseInfo>, bool>>(),
                       (v,e) => v.State.Value = e.State),
                   new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>>(
                       3,
                       e => e?.Entity != null,
                       new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>, bool>>(),
                       (v,e) => v.CurrentEntity.Value = v.EntitySet.FirstOrDefault(x => x.QuestionId == e.Entity.Id)),

                },
                new List<IViewModelEventPublication<IViewModel, IEvent>>
                {
                    new ViewEventPublication<IQuestionaireViewModel, ViewStateLoaded<IQuestionaireViewModel,IProcessStateList<IPatientResponseInfo>>>(
                         key:"IPatientResponseInfoViewStateLoaded",
                         subject:v => v.State,
                         subjectPredicate:new List<Func<IQuestionaireViewModel, bool>>
                                             {
                                                 v => v.State != null
                                             },
                         messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                    
                },
                new List<IViewModelEventCommand<IViewModel,IEvent>>
                {


                    new ViewEventCommand<IQuestionaireViewModel, CurrentEntityChanged<IPatientResponseInfo>>(
                                key:"PreviousQuestion",
                                commandPredicate:new List<Func<IQuestionaireViewModel, bool>>
                                {
                                    v => v.EntitySet.IndexOf(v.CurrentEntity.Value) >= 0
                                },
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                    s.CurrentEntity.Value = s.State.Value.EntitySet.Previous(s.CurrentEntity.Value);

                                    return new ViewEventCommandParameter(
                                        new object[] {s.CurrentEntity.Value},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.Process.Commands.CurrentEntityChanged), s.Process,
                                        s.Source);
                                }),

                    new ViewEventCommand<IQuestionaireViewModel, CurrentEntityChanged<IPatientResponseInfo>>(
                                key:"NextQuestion",
                                commandPredicate:new List<Func<IQuestionaireViewModel, bool>>
                                {
                                    v => v.EntitySet.IndexOf(v.CurrentEntity.Value) < v.EntitySet.Count
                                },
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                    s.CurrentEntity.Value = s.State.Value.EntitySet.Next(s.CurrentEntity.Value);

                                    return new ViewEventCommandParameter(
                                        new object[] {s.CurrentEntity.Value},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.Process.Commands.CurrentEntityChanged), s.Process,
                                        s.Source);
                                }),

                     new ViewEventCommand<IQuestionaireViewModel, UpdateEntityViewWithChanges<IResponseInfo>>(
                        key:"SaveChanges",
                        subject:v => v.ChangeTracking.DictionaryChanges,
                        commandPredicate: new List<Func<IQuestionaireViewModel, bool>>
                                            {
                                                v => v.ChangeTracking.Count == 2 
                                                        && v.ChangeTracking.ContainsKey(nameof(IResponseInfo.Id)) 
                                                        && v.ChangeTracking[nameof(IResponseInfo.Id)] != 0
                                            },
                        //TODO: Make a type to capture this info... i killing it here
                        messageData: s =>
                        {
                           var msg = new ViewEventCommandParameter(
                                new object[]
                                {
                                    s.ChangeTracking.First().Value,
                                    s.ChangeTracking.TakeLast(1).ToDictionary(x => x.Key, x => x.Value)
                                },
                                new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                                s.Source);
                            s.ChangeTracking.Clear();
                            return msg;
                        }),
                      new ViewEventCommand<IQuestionaireViewModel, UpdateEntityViewWithChanges<IResponseInfo>>(
                        key:"CreateEntity",
                        subject:v => v.ChangeTracking.DictionaryChanges,
                        commandPredicate: new List<Func<IQuestionaireViewModel, bool>>
                                            {
                                                v => v.ChangeTracking.Count == 4 && v.ChangeTracking.ContainsKey(nameof(IResponseInfo.Id))
                                                        && v.ChangeTracking[nameof(IResponseInfo.Id)] == 0
                                            },
                        //TODO: Make a type to capture this info... i killing it here
                        messageData: s =>
                        {
                            var msg = new ViewEventCommandParameter(
                                new object[]
                                {
                                    s.ChangeTracking.First().Value,
                                    s.ChangeTracking.Skip(1).ToDictionary(x => x.Key, x => x.Value)
                                },
                                new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,s.Source);
                            s.ChangeTracking.Clear();
                            return msg;
                        }),

                    new ViewEventCommand<IQuestionaireViewModel, ViewRowStateChanged<IPatientResponseInfo>>(
                                key:"EditEntity",
                                commandPredicate:new List<Func<IQuestionaireViewModel, bool>>
                                {
                                    v => v.CurrentEntity != null
                                },
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                    s.RowState.Value = s.RowState.Value != RowState.Modified?RowState.Modified: RowState.Unchanged;//new ReactiveProperty<RowState>(rowstate != RowState.Modified?RowState.Modified: RowState.Unchanged);

                                    return new ViewEventCommandParameter(
                                        new object[] {s,s.RowState.Value},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.Process.Commands.CurrentEntityChanged), s.Process,
                                        s.Source);
                                }),


                },
                typeof(IQuestionaireViewModel),
                typeof(IBodyViewModel)),

                          //////////////////Header ViewModel
               /// 
           new ViewModelInfo
                (
                3,
                new List<IViewModelEventSubscription<IViewModel, IEvent>>{},
                new List<IViewModelEventPublication<IViewModel, IEvent>>{},
                new List<IViewModelEventCommand<IViewModel,IEvent>>
                {


                    new ViewEventCommand<IHeaderViewModel, NavigateToView>(
                                key:"ViewHome",
                                commandPredicate:new List<Func<IHeaderViewModel, bool>>{},
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                   return new ViewEventCommandParameter(
                                        new object[] {ViewMessageConst.Instance.ViewHome},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.View.Commands.NavigateToView), s.Process,
                                        s.Source);
                                }),

                    new ViewEventCommand<IHeaderViewModel, NavigateToView>(
                                key:"ViewPatientInfo",
                                commandPredicate:new List<Func<IHeaderViewModel, bool>>{},
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                   return new ViewEventCommandParameter(
                                        new object[] {ViewMessageConst.Instance.ViewPatientInfo},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.View.Commands.NavigateToView), s.Process,
                                        s.Source);
                                }),
                    new ViewEventCommand<IHeaderViewModel, NavigateToView>(
                                key:"ViewVitals",
                                commandPredicate:new List<Func<IHeaderViewModel, bool>>{},
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                   return new ViewEventCommandParameter(
                                        new object[] {ViewMessageConst.Instance.ViewVitals},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.View.Commands.NavigateToView), s.Process,
                                        s.Source);
                                }),

                    new ViewEventCommand<IHeaderViewModel, NavigateToView>(
                                key:"ViewPatientResponses",
                                commandPredicate:new List<Func<IHeaderViewModel, bool>>{},
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                   return new ViewEventCommandParameter(
                                        new object[] {ViewMessageConst.Instance.ViewPatientResponses},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.View.Commands.NavigateToView), s.Process,
                                        s.Source);
                                }),
                   



                },
                typeof(IHeaderViewModel),
                typeof(IHeaderViewModel)),

                        //////////////////Question List
                        /// 
                        /// 
                        /// 
           new ViewModelInfo
                (
                3, new List<IViewModelEventSubscription<IViewModel, IEvent>>
                {
                   new ViewEventSubscription<IQuestionListViewModel, IProcessStateListMessage<IQuestionInfo>>(
                       3,
                       e => e != null,
                       new List<Func<IQuestionListViewModel, IProcessStateListMessage<IQuestionInfo>, bool>>(),
                       (v,e) => v.State.Value = e.State),

                },
                new List<IViewModelEventPublication<IViewModel, IEvent>>
                {
                    new ViewEventPublication<IQuestionListViewModel, ViewStateLoaded<IQuestionListViewModel,IProcessStateList<IQuestionInfo>>>(
                         key:"IPatientInfoViewStateLoaded",
                         subject:v => v.State,
                         subjectPredicate:new List<Func<IQuestionListViewModel, bool>>
                                             {
                                                 v => v.State != null
                                             },
                         messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                    new ViewEventPublication<IQuestionListViewModel, CurrentEntityChanged<IQuestionInfo>>(
                         key:"CurrentEntityChanged",
                         subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                         subjectPredicate:new List<Func<IQuestionListViewModel, bool>>{},
                         messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
                },
                new List<IViewModelEventCommand<IViewModel,IEvent>>
                {


                    new ViewEventCommand<IQuestionListViewModel, LoadEntityViewSetWithChanges<IQuestionInfo,IPartialMatch>>(
                                key:"Search",
                                commandPredicate:new List<Func<IQuestionListViewModel, bool>>
                                            {
                                                    v => v.ChangeTracking.Values.Count > 0

                                            },
                                subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                                messageData: s =>
                                {
                                    //ToDo: bad practise
                                    if (!string.IsNullOrEmpty(((dynamic)s).Field) && !string.IsNullOrEmpty(((dynamic) s).Value))
                                    {
                                        s.ChangeTracking.AddOrUpdate(((dynamic) s).Field, ((dynamic) s).Value);
                                    }

                                    return new ViewEventCommandParameter(
                                        new object[] {s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},
                                        new StateCommandInfo(s.Process.Id,
                                            Context.EntityView.Commands.LoadEntityViewSetWithChanges), s.Process,
                                        s.Source);
                                }),



                },
                typeof(IQuestionListViewModel),
                typeof(IBodyViewModel)),
        };
    }


}
