using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
using Actor.Interfaces;
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
                        v.State.Value = e.State;
                    }),
                new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentEntity.Value == v.EntitySet.FirstOrDefault(x => x.Id == e.Entity.Id)) return;
                        v.CurrentEntity.Value = v.EntitySet.FirstOrDefault(x => x.Id == e.Entity.Id);
                        
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

                new ViewEventSubscription<IQuestionaireViewModel, ICurrentEntityChanged<IInterviewInfo>>(
                    3,
                    e => e.Entity == null,
                    new List<Func<IQuestionaireViewModel, ICurrentEntityChanged<IInterviewInfo>, bool>>(),
                    (v,e) => v.CurrentEntity.Value = null),

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

                new ViewEventCommand<IQuestionaireViewModel, ICurrentEntityChanged<IQuestionResponseOptionInfo>>(
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

                new ViewEventCommand<IQuestionaireViewModel, IUpdateEntityViewWithChanges<IResponseInfo>>(
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
                new ViewEventCommand<IQuestionaireViewModel, IUpdateEntityViewWithChanges<IResponseInfo>>(
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

                new ViewEventCommand<IQuestionaireViewModel, IViewRowStateChanged<IQuestionResponseOptionInfo>>(
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


        public static class ComplexActions
        {
            public static readonly ComplexEventAction UpdatePatientResponseState = new ComplexEventAction(
                key: "308",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<IEntityViewSetWithChangesLoaded<IQuestionResponseOptionInfo>> (
                    "EntityViewSet", 3, e => e.EntitySet != null, expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                    processInfo: new StateEventInfo(3, Context.EntityView.Events.EntityViewSetLoaded))
                },
                expectedMessageType: typeof(IProcessStateMessage<IQuestionResponseOptionInfo>),
                action: ProcessActions.UpdatePatientResponseState,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));

            public static readonly ComplexEventAction RequestPatientResponses = new ComplexEventAction(
                key: "307",
                processId: 3,
                actionTrigger: ActionTrigger.Partial,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<ICurrentEntityChanged<IInterviewInfo>> (
                    "CurrentInterview", 3, e => e.Entity != null, expectedSourceType: new SourceType(typeof(IViewModel)),//todo: check this cuz it comes from viewmodel
                    processInfo: new StateEventInfo(3, Context.Process.Events.CurrentEntityChanged))
                },
                expectedMessageType: typeof(IProcessStateMessage<IQuestionResponseOptionInfo>),
                action: ProcessActions.RequestPatientResponses,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));
        }

        public class ProcessActions
        {
            public static IProcessAction RequestPatientResponses => new ProcessAction(
                action:
                    cp =>
                        new LoadEntityViewSetWithChanges<IQuestionResponseOptionInfo, IExactMatch>(new Dictionary<string, dynamic>()
                        {
                            {nameof(IQuestionResponseOptionInfo.InterviewId), cp.Messages["CurrentInterview"].Entity.Id },

                        },
                            new StateCommandInfo(3, Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                            cp.Actor.Process, cp.Actor.Source),
                processInfo:
                    cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService)));

            public static IProcessAction UpdatePatientResponseState => new ProcessAction(
                action:
                    cp =>
                    {
                        var ps = new ProcessStateList<IQuestionResponseOptionInfo>(
                            process: cp.Actor.Process,
                            entity: ((List<IQuestionResponseOptionInfo>)cp.Messages["EntityViewSet"].EntitySet).FirstOrDefault(),
                            entitySet: cp.Messages["EntityViewSet"].EntitySet,
                            selectedEntities: new List<IQuestionResponseOptionInfo>(),
                            stateInfo: new StateInfo(3, new State("Loaded IQuestionResponseOptionInfo Data", "IQuestionResponseOptionInfo", "")));
                        return new UpdateProcessStateList<IQuestionResponseOptionInfo>(
                            state: ps,
                            process: cp.Actor.Process,
                            processInfo: new StateCommandInfo(cp.Actor.Process.Id, Context.Process.Commands.UpdateState),
                            source: cp.Actor.Source);
                    },
                processInfo:
                    cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.Process.Commands.UpdateState),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService)));
        }
    }
}