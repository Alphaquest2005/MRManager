using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using EF.Entities;
using Interfaces;
using JB.Collections.Reactive;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using Utilities;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class QuestionaireViewModelInfo
    {
        public static readonly ViewModelInfo QuestionairenaireViewViewModel = new ViewModelInfo
            (
            3,
            new ViewInfo("Questionaire", "", "Patient Responses"),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IQuestionaireViewModel, IUpdateProcessStateList<IQuestionResponseOptionInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionaireViewModel, IUpdateProcessStateList<IQuestionResponseOptionInfo>, bool>>(),
                    (v,e) => 
                    {
                        if (v.State.Value == e.State) return;
                        v.Questions = new ObservableList<IQuestionResponseOptionInfo>(e.State.EntitySet.ToList());
                        v.CurrentQuestion.Value = v.Questions.FirstOrDefault();
                        UpdateQuestionResponse(v);
                    }),
               
                new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentQuestion.Value != null && v.CurrentQuestion.Value == v.Questions.FirstOrDefault(x => x.Id == e.Entity.Id)) return;
                        v.CurrentQuestion.Value = v.Questions.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentQuestion.Value == null) v.CurrentQuestion.Value = v.Questions.FirstOrDefault();
                        UpdateQuestionResponse(v);
                    }),

                new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IPatientVisitInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IPatientVisitInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentPatientVisit == e.Entity) return;
                        v.CurrentPatientVisit = e.Entity;
                        UpdateQuestionResponse(v);
                    }),
                new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IPatientSyntomInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IPatientSyntomInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentPatientSyntom == e.Entity) return;
                        v.CurrentPatientSyntom = e.Entity;
                    }),

                new ViewEventSubscription<IQuestionaireViewModel, IEntityUpdated<IResponseOptions>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, IEntityUpdated<IResponseOptions>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentEntity?.Value?.Id == e.Entity.Id) return;
                        v.ChangeTracking.Add(nameof(IResponseInfo.ResponseOptionId), e.Entity.Id);
                    }),

                new ViewEventSubscription<IQuestionaireViewModel, IEntityUpdated<IPatientResponses>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, IEntityUpdated<IPatientResponses>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentEntity.Value.PatientResponseId == e.Entity.Id) return;
                        v.ChangeTracking.Add(nameof(IResponseOptionInfo.PatientResponseId), e.Entity.Id);
                    }),

                new ViewEventSubscription<IQuestionaireViewModel, IEntityViewWithChangesFound<IResponseOptionInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, IEntityViewWithChangesFound<IResponseOptionInfo>, bool>>(),
                    (v, e) =>
                    {
                        var itm = (ResponseOptionInfo)v.CurrentQuestion.Value.ResponseOptions.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (itm == null)
                        {
                            v.CurrentQuestion.Value.ResponseOptions.Add(e.Entity);
                        }
                        else
                        {
                            //var idx = v.CurrentQuestion.Value.ResponseOptions.TakeWhile(x => x.Id != e.Entity.Id).Count();
                            //v.CurrentQuestion.Value.ResponseOptions.Remove(itm);
                            //v.CurrentQuestion.Value.ResponseOptions.Insert(idx, e.Entity);
                            itm.Description = e.Entity.Description;
                        }
                        v.RowState.Value = RowState.Unchanged;
                    }),

                new ViewEventSubscription<IQuestionaireViewModel, IEntityViewWithChangesUpdated<IResponseOptionInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, IEntityViewWithChangesUpdated<IResponseOptionInfo>, bool>>(),
                    (v, e) =>
                    {
                        var itm = v.CurrentQuestion.Value.PatientResponses.FirstOrDefault(x => x.ResponseId == e.Entity.PatientResponseId);
                        if (itm == null)
                        {
                            v.CurrentQuestion.Value.PatientResponses.Add(e.Entity);
                        }
                        else
                        {
                            var idx = v.CurrentQuestion.Value.PatientResponses.IndexOf(itm);
                            v.CurrentQuestion.Value.PatientResponses.Remove(itm);
                            v.CurrentQuestion.Value.PatientResponses.Insert(idx, e.Entity);

                        }
                        v.RowState.Value = RowState.Unchanged;
                    }),

                new ViewEventSubscription<IQuestionaireViewModel, IEntityViewWithChangesFound<IQuestionResponseOptionInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionaireViewModel, IEntityViewWithChangesFound<IQuestionResponseOptionInfo>, bool>>(),
                    (v, e) =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {


                       var f = v.Questions.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentQuestion.Value == null || v.CurrentQuestion.Value.Id == e.Entity.Id) v.CurrentQuestion.Value = e.Entity;
                        if (f == null)
                        {
                            if (v.Questions.Any())
                            {
                                v.Questions.Insert(v.Questions.Count() - 1,e.Entity);
                            }
                            else
                            {
                                v.Questions.Add(e.Entity);
                            }
                            v.Questions.Reset();
                        }
                        else
                        {
                            //f = e.Entity;
                            var idx = v.Questions.IndexOf(f);
                            v.Questions.Remove(f);
                            v.Questions.Insert(idx, e.Entity);
                            v.Questions.Reset();
                        }
                        v.RowState.Value = RowState.Unchanged;
                        }));

                    }),

            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IQuestionaireViewModel, IViewStateLoaded<IQuestionaireViewModel,IProcessStateList<IQuestionResponseOptionInfo>>>(
                    key:"IPatientResponseInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IQuestionaireViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s =>
                    {
                        //if (s.CurrentQuestion?.Value == null)
                        //{
                        //    if (s.EntitySet.Value.Any()) s.EntitySet.Value.Clear();

                        //}
                        //else
                        //{

                        //    UpdateQuestionResponse(s);
                        //}

                        return new ViewEventPublicationParameter(new object[] {s, s.State.Value},
                            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    }),

                    
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionResponseOptionInfo>>(
                    key:"PreviousQuestion",
                    commandPredicate:new List<Func<IQuestionaireViewModel, bool>>
                    {
                        v => v.Questions.IndexOf(v.CurrentQuestion.Value) >= 0
                    },
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s =>
                    {
                        s.CurrentQuestion.Value = s.Questions.Previous(s.CurrentQuestion.Value);

                        return new ViewEventCommandParameter(
                            new object[] {s.CurrentQuestion.Value},
                            new StateCommandInfo(s.Process.Id,
                                Context.Process.Commands.CurrentEntityChanged), s.Process,
                            s.Source);
                    }),

                new ViewEventCommand<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionResponseOptionInfo>>(
                    key:"NextQuestion",
                    commandPredicate:new List<Func<IQuestionaireViewModel, bool>>
                    {
                        v => v.Questions.IndexOf(v.CurrentQuestion.Value) < v.Questions.Count
                    },
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s =>
                    {
                        s.CurrentQuestion.Value = s.Questions.Next(s.CurrentQuestion.Value);

                        return new ViewEventCommandParameter(
                            new object[] {s.CurrentQuestion.Value},
                            new StateCommandInfo(s.Process.Id,
                                Context.Process.Commands.CurrentEntityChanged), s.Process,
                            s.Source);
                    }),

                new ViewEventCommand<IQuestionaireViewModel, IViewRowStateChanged<IResponseOptionInfo>>(
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
                new ViewEventCommand<IQuestionaireViewModel, IUpdateEntityViewWithChanges<IResponseInfo>>(
                    key:"UpdateChanges",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionaireViewModel, bool>>
                    {
                        v => v.ChangeTracking.ContainsKey(nameof(IResponseInfo.Value))
                             && v.CurrentEntity.Value.Id != 0 && v.CurrentEntity.Value.PatientResponseId.GetValueOrDefault() != 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.CurrentEntity.Value.ResponseId.GetValueOrDefault(),
                                s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        return msg;
                    }),
                new ViewEventCommand<IQuestionaireViewModel, IUpdateEntityViewWithChanges<IResponseInfo>>(
                    key:"SaveResponse",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionaireViewModel, bool>>
                    {
                        v => v.ChangeTracking.ContainsKey(nameof(IResponseInfo.Value))
                            && v.ChangeTracking.ContainsKey(nameof(IResponseInfo.PatientResponseId))
                            &&(v.ChangeTracking.ContainsKey(nameof(IResponseInfo.ResponseOptionId)) || v.CurrentEntity.Value.Id != 0)
                        && v.CurrentEntity.Value.PatientResponseId.GetValueOrDefault()  == 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res = s.ChangeTracking;
                        if (s.CurrentEntity.Value.Id != 0)
                            res.Add(nameof(IResponseInfo.ResponseOptionId), s.CurrentEntity.Value.Id);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                0,
                                res.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        return msg;
                    }),

                new ViewEventCommand<IQuestionaireViewModel, IUpdateEntityWithChanges<IPatientResponses>>(
                    key:"CreatePatientResponse",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionaireViewModel, bool>>
                    {
                        v => v.ChangeTracking.ContainsKey(nameof(IResponseInfo.Value))
                            && !v.ChangeTracking.ContainsKey(nameof(IPatientResponses.PatientSyntomId)) // prevent resending
                            && v.CurrentEntity.Value.PatientResponseId.GetValueOrDefault() == 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {

                        if(!s.ChangeTracking.ContainsKey(nameof(IPatientResponses.QuestionId)))
                                s.ChangeTracking.Add(nameof(IPatientResponses.QuestionId), s.CurrentQuestion.Value.Id);
                        s.ChangeTracking.Add(nameof(IPatientResponses.PatientVisitId), s.CurrentPatientVisit.Id);
                        s.ChangeTracking.Add(nameof(IPatientResponses.PatientSyntomId), s.CurrentPatientSyntom.Id);

                        
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.CurrentEntity.Value.PatientResponseId.GetValueOrDefault(),
                                s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        
                        return msg;
                    }),
                new ViewEventCommand<IQuestionaireViewModel, IUpdateEntityWithChanges<IResponseOptions>>(
                    key:"CreateResponseOption",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionaireViewModel, bool>>
                    {
                        v => v.ChangeTracking.ContainsKey(nameof(IResponseOptions.Description)) 
                                && v.ChangeTracking[nameof(IResponseOptions.Description)] != ""
                               && !v.ChangeTracking.ContainsKey(nameof(IResponseOptions.QuestionId))
                              && v.CurrentEntity.Value.Id == 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res = s.ChangeTracking;
                        res.Add(nameof(IResponseOptions.QuestionId), s.CurrentQuestion.Value.Id);
                        if (!res.ContainsKey(nameof(IResponseOptions.QuestionResponseTypeId)))
                            res.Add(nameof(IResponseOptions.QuestionResponseTypeId),
                                s.CurrentEntity.Value.QuestionResponseTypeId);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.CurrentEntity.Value.Id,
                                res.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,s.Source);
                        //s.ChangeTracking.Clear();
                        return msg;
                    }),

                new ViewEventCommand<IQuestionaireViewModel, IUpdateEntityWithChanges<IResponseOptions>>(
                    key:"UpdateResponseOption",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionaireViewModel, bool>>
                    {
                        v => v.ChangeTracking.ContainsKey(nameof(IResponseOptions.Description))
                               && !v.ChangeTracking.ContainsKey(nameof(IResponseOptions.QuestionResponseTypeId))
                              && v.CurrentEntity.Value.Id != 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res = s.ChangeTracking;
                        
                        if (!res.ContainsKey(nameof(IResponseOptions.QuestionResponseTypeId)))
                            res.Add(nameof(IResponseOptions.QuestionResponseTypeId),
                                s.CurrentEntity.Value.QuestionResponseTypeId);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.CurrentEntity.Value.Id,
                                res.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,s.Source);
                        //s.ChangeTracking.Clear();
                        return msg;
                    }),




            },
            typeof(IQuestionaireViewModel),
            typeof(IBodyViewModel),6);

        private static void UpdateQuestionResponse(IQuestionaireViewModel s)
        {
            var resLst = new List<ResponseOptionInfo>();

            if (s.CurrentQuestion?.Value?.ResponseOptions != null)
            {
                resLst.AddRange(s.CurrentQuestion.Value.ResponseOptions.Select(x => (ResponseOptionInfo) x));
                if (s.CurrentPatientVisit != null)
                {
                    foreach (
                        var itm in
                            s.CurrentQuestion.Value.PatientResponses.Where(
                                x => x.PatientVisitId == s.CurrentPatientVisit.Id)
                        )
                    {
                        var res = resLst.First(x => x.Id == itm.Id);
                        res.Value = itm.Value;
                        res.ResponseId = itm.ResponseId;
                        res.PatientResponseId = itm.Id;
                    }
                }
            }
            if (s.CurrentQuestion?.Value == null) return;
            
                resLst.Add(new ResponseOptionInfo()
                {
                    Id = 0,
                    Description = "Edit to Create New Response Option",
                    QuestionId = s.CurrentQuestion.Value.Id,
                    QuestionResponseTypeId = 1
                });

                s.EntitySet.Value =
                    new ObservableList<IResponseOptionInfo>(
                        resLst.Select(x => (IResponseOptionInfo) x).ToList());
                s.NotifyPropertyChanged(nameof(s.EntitySet));
            
        }
    }
}