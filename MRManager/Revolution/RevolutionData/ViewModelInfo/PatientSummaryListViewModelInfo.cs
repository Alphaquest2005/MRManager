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
    public class PatientSummaryListViewModelInfo
    {
        public static readonly ViewModelInfo PatientSummaryListViewModel = new ViewModelInfo
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

                new ViewEventCommand<IPatientSummaryListViewModel, ViewRowStateChanged<IPatientInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IPatientSummaryListViewModel, bool>>
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
            typeof(IPatientSummaryListViewModel),
            typeof(IBodyViewModel));

        public static class ComplexActions
        {
            public static readonly ComplexEventAction UpdatePatientInfoState = new ComplexEventAction(
                key: "302",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<IEntityViewSetWithChangesLoaded<IPatientInfo>> (
                    "EntityViewSet", 3, e => e.EntitySet != null, expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                    processInfo: new StateEventInfo(2, Context.EntityView.Events.EntityViewSetLoaded))
                },
                expectedMessageType: typeof(IProcessStateMessage<IPatientInfo>),
                action: ProcessActions.PatientInfo.UpdatePatientInfoState,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));

            public static readonly ComplexEventAction IntializePatientInfoSummaryProcessState = new ComplexEventAction(

                key: "301",
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
                expectedMessageType: typeof(IProcessStateMessage<IPatientInfo>),
                action: ProcessActions.PatientInfo.IntializePatientInfoSummaryProcessState,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.CreateState));
        }
    }
}