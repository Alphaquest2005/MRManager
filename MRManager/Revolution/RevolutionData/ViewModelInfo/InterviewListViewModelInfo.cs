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
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewMessages;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class InterviewListViewModelInfo
    {
        public static readonly ViewModelInfo InterviewListViewModel = new ViewModelInfo
            (
            3, new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IInterviewListViewModel, IUpdateProcessStateList<IInterviewInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IInterviewListViewModel, IUpdateProcessStateList<IInterviewInfo>, bool>>(),
                    (v,e) => v.State.Value = e.State),

                new ViewEventSubscription<IInterviewListViewModel, ICurrentEntityChanged<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IInterviewListViewModel, ICurrentEntityChanged<IPatientSyntomInfo>, bool>>(),
                    (v,e) => v.CurrentPatientSyntom.Value = e.Entity),

            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IInterviewListViewModel, IViewStateLoaded<IInterviewListViewModel,IProcessStateList<IInterviewInfo>>>(
                    key:"IPatientInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IInterviewListViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                new ViewEventPublication<IInterviewListViewModel, ICurrentEntityChanged<IInterviewInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IInterviewListViewModel, bool>>{},
                    messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                new ViewEventPublication<IInterviewListViewModel, ICurrentEntityChanged<ISyntomMedicalSystemInfo>>(
                    key:"CurrentSystemChanged",
                    subject:v => v.CurrentMedicalSystem,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IInterviewListViewModel, bool>>{},
                    messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentMedicalSystem.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IInterviewListViewModel, ILoadEntityViewSetWithChanges<IInterviewInfo,IPartialMatch>>(
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

                new ViewEventCommand<IInterviewListViewModel, IViewRowStateChanged<IInterviewInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IInterviewListViewModel, bool>>
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
            typeof(IInterviewListViewModel),
            typeof(IBodyViewModel));

        public static class ComplexActions
        {
            public static readonly ComplexEventAction UpdateInterviewInfoState = new ComplexEventAction(
                key: "306",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<IEntityViewSetWithChangesLoaded<IInterviewInfo>> (
                    "EntityViewSet", 3, e => e.EntitySet != null, expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                    processInfo: new StateEventInfo(3, Context.EntityView.Events.EntityViewSetLoaded))
                },
                expectedMessageType: typeof(IProcessStateMessage<IInterviewInfo>),
                action: ProcessActions.UpdateInterviewInfoState,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));

            public static readonly ComplexEventAction IntializeInterviewInfoProcessState = new ComplexEventAction(
                key: "305",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent (key: "ProcessStarted",
                    processId: 3,
                    eventPredicate: e => e != null,
                    eventType: typeof (ISystemProcessStarted),
                    processInfo: new StateEventInfo(3,Context.Process.Events.ProcessStarted),
                    expectedSourceType:new SourceType(typeof(IComplexEventService)))

                },
                expectedMessageType: typeof(IProcessStateMessage<IInterviewInfo>),
                action: ProcessActions.IntializeInterviewInfoProcessState,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.CreateState));
        }

        public class ProcessActions
        {
            /// 
            public static IProcessAction IntializeInterviewInfoProcessState => new ProcessAction(
                action:
                    cp =>
                        new LoadEntityViewSetWithChanges<IInterviewInfo, IExactMatch>(new Dictionary<string, dynamic>(),
                            new StateCommandInfo(3, Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                            cp.Actor.Process, cp.Actor.Source),
                processInfo:
                    cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService)));

            public static IProcessAction UpdateInterviewInfoState => new ProcessAction(
                action:
                    cp =>
                    {
                        var ps = new ProcessStateList<IInterviewInfo>(
                            process: cp.Actor.Process,
                            entity: ((List<IInterviewInfo>)cp.Messages["EntityViewSet"].EntitySet).FirstOrDefault(),
                            entitySet: cp.Messages["EntityViewSet"].EntitySet,
                            selectedEntities: new List<IInterviewInfo>(),
                            stateInfo: new StateInfo(3, new State("Loaded IInterviewInfo Data", "LoadedIInterviewData", "")));
                        return new UpdateProcessStateList<IInterviewInfo>(
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