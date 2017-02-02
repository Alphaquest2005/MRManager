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
    public class PatientVisitViewModelInfo
    {
        public static ViewModelInfo PatientVisitViewModel = new ViewModelInfo
            (
            3, new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IPatientVisitViewModel, IProcessStateListMessage<IPatientVisitInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientVisitViewModel, IProcessStateListMessage<IPatientVisitInfo>, bool>>(),
                    (v,e) => v.State.Value = e.State),

                
            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IPatientVisitViewModel, ViewStateLoaded<IPatientVisitViewModel,IProcessStateList<IPatientVisitInfo>>>(
                    key:"IPatientInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IPatientVisitViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                new ViewEventPublication<IPatientVisitViewModel, CurrentEntityChanged<IPatientVisitInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IPatientVisitViewModel, bool>>{},
                    messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IPatientVisitViewModel, LoadEntityViewSetWithChanges<IPatientVisitInfo,IPartialMatch>>(
                    key:"Search",
                    commandPredicate:new List<Func<IPatientVisitViewModel, bool>>
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

                new ViewEventCommand<IPatientVisitViewModel, ViewRowStateChanged<IPatientVisitInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IPatientVisitViewModel, bool>>
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
            typeof(IPatientVisitViewModel),
            typeof(IBodyViewModel));

        public static class ComplexActions
        {
            public static readonly ComplexEventAction UpdateState = new ComplexEventAction(
                key: "PatientVisit-1",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<IEntityViewSetWithChangesLoaded<IPatientVisitInfo>> (
                    "EntityViewSet", 3, e => e.EntitySet != null, expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                    processInfo: new StateEventInfo(3, Context.EntityView.Events.EntityViewSetLoaded))
                },
                expectedMessageType: typeof(IProcessStateMessage<IPatientVisitInfo>),
                action: ProcessActions.UpdateState,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));

            public static readonly ComplexEventAction RequestData = new ComplexEventAction(
                key: "PatientVisit-2",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<ICurrentEntityChanged<IPatientInfo>> (
                    "CurrentPatient", 3, e => e.Entity != null, expectedSourceType: new SourceType(typeof(IViewModel)),//todo: check this cuz it comes from viewmodel
                    processInfo: new StateEventInfo(2, Context.Process.Events.CurrentEntityChanged))
                },
                expectedMessageType: typeof(IProcessStateMessage<IPatientVisitInfo>),
                action: ProcessActions.RequestData,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));
        }

        public class ProcessActions
        {
            /// 
            public static IProcessAction RequestData => new ProcessAction(
               action:
                   cp =>
                       new LoadEntityViewSetWithChanges<IPatientVisitInfo, IExactMatch>(new Dictionary<string, dynamic>()
                                   {
                                        {nameof(IPatientVisitInfo.PatientId), cp.Messages["CurrentPatient"].Entity.Id },
                                   },
                           new StateCommandInfo(3, Context.EntityView.Commands.LoadEntityViewSetWithChanges),
                           cp.Actor.Process, cp.Actor.Source),
               processInfo:
                   cp =>
                       new StateCommandInfo(cp.Actor.Process.Id,
                           Context.EntityView.Commands.LoadEntityViewSetWithChanges),
               // take shortcut cud be IntialState
               expectedSourceType: new SourceType(typeof(IComplexEventService)));

            public static IProcessAction UpdateState => new ProcessAction(
                action:
                    cp =>
                    {
                        var ps = new ProcessStateList<IPatientVisitInfo>(
                            process: cp.Actor.Process,
                            entity: ((List<IPatientVisitInfo>)cp.Messages["EntityViewSet"].EntitySet).FirstOrDefault(),
                            entitySet: cp.Messages["EntityViewSet"].EntitySet,
                            selectedEntities: new List<IPatientVisitInfo>(),
                            stateInfo: new StateInfo(3, new State("Loaded IPatientVisitInfo Data", "LoadedIPatientVisitInfo", "")));
                        return new UpdateProcessStateList<IPatientVisitInfo>(
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