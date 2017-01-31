using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
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
                new ViewEventSubscription<IQuestionaireViewModel, IProcessStateListMessage<IQuestionResponseOptionInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionaireViewModel, IProcessStateListMessage<IQuestionResponseOptionInfo>, bool>>(),
                    (v,e) => v.State.Value = e.State),
                new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>, bool>>(),
                    (v,e) => v.CurrentEntity.Value = v.EntitySet.FirstOrDefault(x => x.Id == e.Entity.Id)),
                new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IPatientInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IPatientInfo>, bool>>(),
                    (v,e) => v.CurrentPatient = e.Entity),

            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IQuestionaireViewModel, ViewStateLoaded<IQuestionaireViewModel,IProcessStateList<IQuestionResponseOptionInfo>>>(
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


                new ViewEventCommand<IQuestionaireViewModel, CurrentEntityChanged<IQuestionResponseOptionInfo>>(
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

                new ViewEventCommand<IQuestionaireViewModel, CurrentEntityChanged<IQuestionResponseOptionInfo>>(
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

                new ViewEventCommand<IQuestionaireViewModel, ViewRowStateChanged<IQuestionResponseOptionInfo>>(
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
            typeof(IBodyViewModel));
    }
}