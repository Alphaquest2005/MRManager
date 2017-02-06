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
using ViewMessages;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class QuestionListViewModelInfo
    {
        public static readonly ViewModelInfo QuestionListViewModel = new ViewModelInfo
            (
            3, new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IQuestionListViewModel, IUpdateProcessStateList<IQuestionInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionListViewModel, IUpdateProcessStateList<IQuestionInfo>, bool>>(),
                    (v,e) => v.State.Value = e.State),

                new ViewEventSubscription<IQuestionListViewModel, ICurrentEntityChanged<IInterviewInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IQuestionListViewModel, ICurrentEntityChanged<IInterviewInfo>, bool>>(),
                    (v,e) => v.CurrentInterview = e.Entity),

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
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                new ViewEventPublication<IQuestionListViewModel, ICurrentEntityChanged<IQuestionInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IQuestionListViewModel, bool>>{},
                    messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
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
                        v => v.ChangeTracking.Count == 2
                             && v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.Description))
                             && v.ChangeTracking[nameof(IQuestionInfo.Id)] != 0
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

                new ViewEventCommand<IQuestionListViewModel, IUpdateEntityViewWithChanges<IQuestionInfo>>(
                    key:"UpdateQuestion",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionListViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count == 2
                             && v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.Description))
                             && v.ChangeTracking[nameof(IQuestionInfo.Id)] != 0
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
                        v => v.ChangeTracking.Count > 1
                             && v.ChangeTracking[nameof(IQuestionInfo.Id)] == 0
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
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        return msg;
                    }),

                new ViewEventCommand<IQuestionListViewModel, IUpdateEntityWithChanges<IEntityAttributes>>(
                    key:"UpdateEnityAttribute",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IQuestionListViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count > 1
                             && v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.EntityAttributeId))
                             && !v.ChangeTracking.ContainsKey(nameof(IQuestionInfo.Id))
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
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        return msg;
                    }),


            },
            typeof(IQuestionListViewModel),
            typeof(IBodyViewModel));


        public static class ComplexActions
        {
            public static readonly ComplexEventAction UpdateQuestionListState = new ComplexEventAction(
                key: "310",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<IEntityViewSetWithChangesLoaded<IQuestionInfo>> (
                    "EntityViewSet", 3, e => e.EntitySet != null, expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                    processInfo: new StateEventInfo(3, Context.EntityView.Events.EntityViewSetLoaded))
                },
                expectedMessageType: typeof(IProcessStateMessage<IQuestionInfo>),
                action: ProcessActions.PatientInfo.UpdateQuestionListState,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));

            public static readonly ComplexEventAction RequestQuestionList = new ComplexEventAction(
                key: "311",
                processId: 3,
                actionTrigger: ActionTrigger.Partial,
                events: new List<IProcessExpectedEvent>
                {       new ProcessExpectedEvent<ICurrentEntityChanged<IInterviewInfo>> (
                "CurrentInterview", 3, e => e.Entity != null, expectedSourceType: new SourceType(typeof(IViewModel)),//todo: check this cuz it comes from viewmodel
                processInfo: new StateEventInfo(3, Context.Process.Events.CurrentEntityChanged))
                },
                expectedMessageType: typeof(IProcessStateMessage<IPatientResponseInfo>),
                action: ProcessActions.PatientInfo.RequestQuestionList,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));
        }
    }
}