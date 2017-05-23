using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
using DomainMessages;
using Interfaces;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class PatientVitalsViewModelInfo
    {
        public static readonly ViewModelInfo PatientVitalsViewModel = new ViewModelInfo
            (
            processId: 3,
            viewInfo: new ViewInfo("PatientVitals", "", "Vitals"),
            subscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription
                    <IPatientVitalsViewModel,
                        IProcessStateMessage<IPatientVitalsInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientVitalsViewModel, IProcessStateMessage<IPatientVitalsInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        if (v.CurrentPatient != null && v.CurrentPatient?.Id != e.State.Entity.Id) return;
                        if (v.State.Value?.Entity?.EntryDateTime >= e.State.Entity.EntryDateTime) return;
                        v.State.Value = e.State;
                        v.RowState.Value = RowState.Unchanged;
                    }),

                
                new ViewEventSubscription<IPatientVitalsViewModel, ICurrentEntityChanged<IPatientInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientVitalsViewModel, ICurrentEntityChanged<IPatientInfo>, bool>>(),
                    (v, e) =>
                    {
                        v.CurrentPatient = e.Entity;
                       
                    }),

                new ViewEventSubscription<IPatientVitalsViewModel, IEntityFound<IPatientVitalsInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientVitalsViewModel, IEntityFound<IPatientVitalsInfo>, bool>>(),
                    (v, e) =>
                    {
                        v.State.Value = new ProcessState<IPatientVitalsInfo>(e.Process,e.Entity, new StateInfo(e.ProcessInfo.ProcessId, e.ProcessInfo.State));
                    }),




            },
            publications: new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IPatientVitalsViewModel, IViewStateLoaded<IPatientVitalsViewModel,IProcessState<IPatientVitalsInfo>>>(
                    key:"PatientVitalsViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IPatientVitalsViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),


            },
            commands: new List<IViewModelEventCommand<IViewModel, IEvent>>
            {
                new ViewEventCommand<IPatientVitalsViewModel, IViewRowStateChanged<IPatientVitalsInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IPatientVitalsViewModel, bool>>
                    {
                        //v => v. != null
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

                new ViewEventCommand<IPatientVitalsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key:"SavePatientVitals",
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                    commandPredicate: new List<Func<IPatientVitalsViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count >= 1
                             && v.CurrentPatient.Id != 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                               ((dynamic)s).Id,
                                "Vitals",
                                "General",
                                "Vitals",
                                s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        s.State.Value = null;

                        return msg;
                    }),

            },
            viewModelType: typeof(IPatientVitalsViewModel),
            orientation: typeof(IBodyViewModel),
            priority: 1);


    }
}