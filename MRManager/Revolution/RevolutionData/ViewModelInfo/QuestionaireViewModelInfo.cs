using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
using Actor.Interfaces;
using EF.Entities;
using EventMessages.Commands;
using EventMessages.Events;
using Interfaces;
using MoreLinq;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using Utilities;
using ViewMessages;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class QuestionaireViewModelInfo
    {
        public static readonly ViewModelInfo QuestionairenaireViewViewModel = new ViewModelInfo
            (
            3, new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IQuestionaireViewModel, IUpdateProcessStateList<IQuestionResponseOptionInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionaireViewModel, IUpdateProcessStateList<IQuestionResponseOptionInfo>, bool>>(),
                    (v,e) => 
                    {
                        if (v.State.Value == e.State) return;
                        v.Questions = e.State.EntitySet.ToList();
                    }),
               
                new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentQuestion.Value == v.Questions.FirstOrDefault(x => x.Id == e.Entity.Id)) return;
                        v.CurrentQuestion.Value = v.Questions.FirstOrDefault(x => x.Id == e.Entity.Id);
                        
                    }),

                new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IPatientVisitInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IPatientVisitInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentPatientVisit == e.Entity) return;
                        v.CurrentPatientVisit = e.Entity;
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
                        if (v.CurrentEntity.Value.Id == e.Entity.Id) return;
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
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                    
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
                new ViewEventCommand<IQuestionaireViewModel, IUpdateEntityWithChanges<IResponse>>(
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

                


            },
            typeof(IQuestionaireViewModel),
            typeof(IBodyViewModel));


    }
}