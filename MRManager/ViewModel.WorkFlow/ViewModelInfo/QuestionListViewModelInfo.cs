using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using EF.Entities;
using Interfaces;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class QuestionListViewModelInfo
    {
        public static readonly ViewModelInfo QuestionListViewModel = new ViewModelInfo
            (
            3,
            new ViewInfo("QuestionList", "", "Questions"),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IQuestionListViewModel, IUpdateProcessStateList<IQuestionInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionListViewModel, IUpdateProcessStateList<IQuestionInfo>, bool>>(),
                    (v,e) => 
                    {
                        if (v.State.Value == e.State) return;
                        v.State.Value = e.State;
                    }),

                new ViewEventSubscription<IQuestionListViewModel, ICurrentEntityChanged<IInterviewInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionListViewModel, ICurrentEntityChanged<IInterviewInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentInterview == e.Entity) return;
                        v.CurrentInterview = e.Entity;
                        
                    }),

                new ViewEventSubscription<IQuestionListViewModel, IEntityUpdated<IEntityAttributes>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionListViewModel, IEntityUpdated<IEntityAttributes>, bool>>(),
                    (v,e) => v.ChangeTracking.Add(nameof(IQuestionInfo.EntityAttributeId), e.Entity.Id)),

                new ViewEventSubscription<IQuestionListViewModel, IEntityViewWithChangesUpdated<IQuestionInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionListViewModel, IEntityViewWithChangesUpdated<IQuestionInfo>, bool>>(),
                    (v, e) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {


                            var f = v.EntitySet.Value.FirstOrDefault(x => x.Id == e.Entity.Id);
                            if (v.CurrentEntity.Value.Id == e.Entity.Id) v.CurrentEntity.Value = e.Entity;
                            if (f == null)
                            {
                                if (v.EntitySet.Value.Any())
                                {
                                    v.EntitySet.Value.Insert(v.EntitySet.Value.Count() - 1, e.Entity);
                                }
                                else
                                {
                                    v.EntitySet.Value.Add(e.Entity);
                                }
                                v.EntitySet.Value.Reset();
                            }
                            else
                            {
                                //f = e.Entity;
                                var idx = v.EntitySet.Value.IndexOf(f);
                                v.EntitySet.Value.Remove(f);
                                v.EntitySet.Value.Insert(idx, e.Entity);
                                v.EntitySet.Value.Reset();
                            }
                            v.RowState.Value = RowState.Unchanged;
                        });
                         }),



                    },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IQuestionListViewModel, IViewStateLoaded<IQuestionListViewModel,IProcessStateList<IQuestionInfo>>>(
                    key:"IPatientInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IQuestionListViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s =>
                    {
                         
                        return new ViewEventPublicationParameter(new object[] {s, s.State.Value},
                            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    }),

                new ViewEventPublication<IQuestionListViewModel, ICurrentEntityChanged<IQuestionInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IQuestionListViewModel, bool>>{},
                    messageData:s =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                         {
                              if (s.CurrentInterview == null)
                                {
                                    s.EntitySet.Value.Clear();
                                }
                                else
                                {
                                    if (s.EntitySet?.Value?.FirstOrDefault(x => x.Id == 0) == null)
                                    {
                                        //var res = entitySet.OrderBy(z => z.QuestionNumber).ToList();
                                        s.EntitySet.Value.Add(new QuestionInfo()

                                        {
                                            Id = 0,
                                            Description = "Edit to Create New Question",
                                            EntityAttributeId = 0,
                                            InterviewId = s.CurrentInterview.Id,
                                            Attribute = "Unspecified",
                                            Entity = "Unspecified",
                                            Type = "TextBox"
                                        });
                                    }
                                }
                                s.NotifyPropertyChanged(nameof(s.EntitySet));
                         }));
                        return new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},
                            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    })
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IQuestionListViewModel, ILoadEntityViewSetWithChanges<IQuestionInfo,IPartialMatch>>(
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

                new ViewEventCommand<IQuestionListViewModel, IViewRowStateChanged<IQuestionInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IQuestionListViewModel, bool>>
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

                new ViewEventCommand<IQuestionListViewModel, IUpdateEntityViewWithChanges<IQuestionInfo>>(
                    key:"SaveQuestion",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionListViewModel, bool>>
                    {
                        v => v.CurrentEntity?.Value?.Id != 0 && 
                                (v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.Description)) 
                                    || (v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.EntityAttributeId)) 
                                            &&  v.CurrentEntity?.Value?.EntityAttributeId != v.ChangeTracking[nameof(IQuestionInfo.EntityAttributeId)]))
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.CurrentEntity.Value.Id,
                                s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        return msg;
                    }),

               
                new ViewEventCommand<IQuestionListViewModel, IUpdateEntityViewWithChanges<IQuestionInfo>>(
                    key:"CreateQuestion",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionListViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count == 4
                             && v.CurrentEntity.Value.Id == 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        s.ChangeTracking.Add(nameof(IQuestionInfo.InterviewId), s.CurrentInterview.Id);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.CurrentEntity.Value.Id,
                                s.ChangeTracking.Where(x => x.Key == nameof(IQuestionInfo.Description)
                                            || x.Key == nameof(IQuestionInfo.InterviewId) || x.Key == nameof(IQuestionInfo.EntityAttributeId)).ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        return msg;
                    }),

                new ViewEventCommand<IQuestionListViewModel, IAddOrGetEntityWithChanges<IEntityAttributes>>(
                    key:"AddEnityAttribute",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionListViewModel, bool>>
                    {
                        v => v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.Entity))
                             && v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.Attribute))
                             && v.CurrentEntity.Value.EntityAttributeId == 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.ChangeTracking.Where(x => x.Key == nameof(IQuestionInfo.Entity) 
                                            || x.Key == nameof(IQuestionInfo.Attribute)).ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                         
                        return msg;
                    }),
                new ViewEventCommand<IQuestionListViewModel, IUpdateEntityWithChanges<IEntityAttributes>>(
                    key:"UpdateAttribute",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionListViewModel, bool>>
                    {
                        v => v.CurrentEntity.Value?.EntityAttributeId != 0
                             && (v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.Attribute)) || v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.Entity)))
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {

                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.CurrentEntity.Value.EntityAttributeId,
                                v.ChangeTracking.Where(x => x.Key == nameof(IQuestionInfo.Entity)
                                            || x.Key == nameof(IQuestionInfo.Attribute)).ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),

            },
            typeof(IQuestionListViewModel),
            typeof(IBodyViewModel),5);

    }
}