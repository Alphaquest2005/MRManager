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
    public class PatientDetailsViewModelInfo
    {
        public static readonly ViewModelInfo PatientDetailsViewModel = new ViewModelInfo
            (
            processId: 3,
            viewInfo: new ViewInfo("PatientDetails", "", "Patient Details"),
            subscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription
                    <IPatientDetailsViewModel,
                        IProcessStateMessage<IPatientDetailsInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientDetailsInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        if (v.CurrentPatient != null && v.CurrentPatient?.Id != e.State.Entity.Id) return;
                        if (v.State.Value?.Entity?.EntryDateTime >= e.State.Entity.EntryDateTime) return;
                        v.State.Value = e.State;
                        v.RowState.Value = RowState.Unchanged;
                    }),

                new ViewEventSubscription
                    <IPatientDetailsViewModel,
                        IProcessStateMessage<IPatientAddressesInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientAddressesInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        if (v.CurrentPatient != null && v.CurrentPatient?.Id != e.State.Entity.Id) return;
                        v.Addresses = e.State.Entity.Addresses;
                       
                    }),

                new ViewEventSubscription
                    <IPatientDetailsViewModel,
                        IProcessStateMessage<IPatientPhoneNumbersInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientPhoneNumbersInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        if (v.CurrentPatient != null && v.CurrentPatient?.Id != e.State.Entity.Id) return;
                        v.PhoneNumbers = e.State.Entity.PhoneNumbers;

                    }),

                new ViewEventSubscription
                    <IPatientDetailsViewModel,
                        IProcessStateMessage<IPatientNextOfKinsInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientNextOfKinsInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        if (v.CurrentPatient != null && v.CurrentPatient?.Id != e.State.Entity.Id) return;
                        v.NextOfKins = e.State.Entity.NextOfKins;

                    }),

                new ViewEventSubscription
                    <IPatientDetailsViewModel,
                        IProcessStateMessage<INonResidentInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<INonResidentInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        if (v.CurrentPatient != null && v.CurrentPatient?.Id != e.State.Entity.Id) return;
                        v.NonResidentInfo = e.State.Entity;

                    }),

                new ViewEventSubscription<IPatientDetailsViewModel, ICurrentEntityChanged<IPatientInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientDetailsViewModel, ICurrentEntityChanged<IPatientInfo>, bool>>(),
                    (v, e) =>
                    {
                        v.CurrentPatient = e.Entity;
                        v.Addresses = null;
                        v.PhoneNumbers = null;
                        v.NextOfKins = null;
                        v.NonResidentInfo = null;
                        v.State.Value = null;
                    }),

                new ViewEventSubscription<IPatientDetailsViewModel, IEntityFound<IPatientDetailsInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientDetailsViewModel, IEntityFound<IPatientDetailsInfo>, bool>>(),
                    (v, e) =>
                    {
                        v.State.Value = new ProcessState<IPatientDetailsInfo>(e.Process,e.Entity, new StateInfo(e.ProcessInfo.ProcessId, e.ProcessInfo.State));
                    }),




            },
            publications: new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IPatientDetailsViewModel, IViewStateLoaded<IPatientDetailsViewModel,IProcessState<IPatientDetailsInfo>>>(
                    key:"PatientDetailsViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),


            },
            commands: new List<IViewModelEventCommand<IViewModel,IEvent>>
            {
                new ViewEventCommand<IPatientDetailsViewModel, IViewRowStateChanged<IPatientDetailsInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IPatientDetailsViewModel, bool>>
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

                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key:"SavePatientDetails",
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count >= 1
                             && v.State.Value.Entity.Id != 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                ((dynamic)s).Id,
                                "Patient",
                                "General",
                                "Personal Information",
                                s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        s.State.Value = null;
                        
                        return msg;
                    }),

            },
            viewModelType: typeof(IPatientDetailsViewModel),
            orientation: typeof(IBodyViewModel),
            priority:1);

       
    }
}