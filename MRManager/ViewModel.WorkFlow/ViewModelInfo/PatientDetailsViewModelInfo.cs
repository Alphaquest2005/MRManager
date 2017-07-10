using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using DomainMessages;
using EF.Entities;
using Interfaces;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public partial class PatientDetailsViewModelInfo
    {
        public static readonly ViewModelInfo PatientDetailsViewModel = new ViewModelInfo
            (
            processId: 3,
            viewInfo: new ViewInfo("PatientDetails", "", "Patient Details"),
            subscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription <IPatientDetailsViewModel,IProcessStateMessage<IPatientDetailsInfo>>(
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
                        v.Addresses = new List<IPersonAddressInfo>(e.State.Entity.Addresses)
                        {
                            new PersonAddressInfo(){Address = "Create New..."}
                        };
                        v.CurrentAddress.Value = v.Addresses.FirstOrDefault();
                        v.NotifyPropertyChanged(nameof(v.CurrentAddress));

                    }),

                new ViewEventSubscription <IPatientDetailsViewModel, IProcessStateMessage<IPatientForeignAddressesInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientForeignAddressesInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        if (v.NonResidentInfo != null && v.NonResidentInfo?.Id != e.State.Entity.Id) return;
                        v.ForeignAddresses = new List<IForeignAddressInfo>(e.State.Entity.Addresses)
                        {
                            new ForeignAddressInfo(){Addresslines = "Create New..."}
                        };
                        v.CurrentForeignAddress.Value = v.ForeignAddresses.FirstOrDefault();
                        v.NotifyPropertyChanged(nameof(v.CurrentForeignAddress));

                    }),

                new ViewEventSubscription
                    <IPatientDetailsViewModel,
                        IProcessStateMessage<IPatientPhoneNumbersInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientPhoneNumbersInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (v.CurrentPatient != null && v.CurrentPatient?.Id != e.State.Entity.Id) return;
                            v.PhoneNumbers = new List<IPersonPhoneNumberInfo>(e.State.Entity.PhoneNumbers)
                            {
                                new PersonPhoneNumberInfo() {PhoneNumber = "Create New..."}
                            };
                            v.CurrentPhoneNumber.Value = v.PhoneNumbers.FirstOrDefault();
                            v.NotifyPropertyChanged(nameof(v.CurrentPhoneNumber));
                        });


                    }),

                new ViewEventSubscription<IPatientDetailsViewModel,IProcessStateMessage<IPatientForeignPhoneNumbersInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientForeignPhoneNumbersInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        if (v.CurrentPatient != null && v.CurrentPatient?.Id != e.State.Entity.Id) return;
                        v.ForeignPhoneNumbers = new List<IForeignPhoneNumberInfo>(e.State.Entity.PhoneNumbers)
                        {
                            new ForeignPhoneNumberInfo(){ PhoneNumber = "Create New ..."}
                        };

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
                        v.NextOfKins = new List<INextOfKinInfo>(e.State.Entity.NextOfKins)
                        {
                            new NextOfKinInfo(){ Name = "Create New ..."}
                        }; 

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
                        v.ForeignAddresses = new List<IForeignAddressInfo>()
                        {
                            new ForeignAddressInfo(){ Addresslines = "Create New ..."}
                        };

                        v.ForeignPhoneNumbers = new List<IForeignPhoneNumberInfo>()
                        {
                            new ForeignPhoneNumberInfo(){ PhoneNumber = "Create New ..."}
                        };

                    }),

                new ViewEventSubscription<IPatientDetailsViewModel,IProcessStateMessage<IPatientForeignAddressesInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientForeignAddressesInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        if (v.CurrentPatient != null && v.CurrentPatient?.Id != e.State.Entity.Id) return;
                        v.ForeignAddresses = new List<IForeignAddressInfo>(e.State.Entity.Addresses)
                        {
                            new ForeignAddressInfo(){ Addresslines = "Create New ..."}
                        };

                    }),



                new ViewEventSubscription<IPatientDetailsViewModel, ICurrentEntityChanged<IPatientInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientDetailsViewModel, ICurrentEntityChanged<IPatientInfo>, bool>>(),
                    (v, e) =>
                    {

                        v.CurrentPatient = e.Entity;
                        v.State.Value = null;
                        v.Addresses = null;
                        v.PhoneNumbers = new List<IPersonPhoneNumberInfo>() {new PersonPhoneNumberInfo() {PhoneNumber = "Create New..."}};
                        v.NextOfKins = null;
                        v.NonResidentInfo = null;
                        v.ForeignAddresses = null;
                        v.ForeignPhoneNumbers = null;
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
                    key: "SavePatientDetails",
                    subject: s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
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
                                s.State.Value.Entity.Id,
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
                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key: "SaveNonResidentType",
                    subject: s => s.ChangeTracking
                        .DictionaryChanges, //Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count >= 1
                             && v.State.Value.Entity.Id != 0
                             && v.ChangeTracking.ContainsKey("Type")
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var msg1 = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.State.Value.Entity.Id,
                                "NonResident",
                                "General",
                                "Personal Information",
                                s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        s.State.Value = null;

                        return msg1;
                    }),
                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key: "SaveNonResidentInfo",
                    subject: s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res = new Dictionary<string, object>();

                        if (!string.IsNullOrEmpty(s.NonResidentInfo.BoatName))
                            res.Add(nameof(s.NonResidentInfo.BoatName), s.NonResidentInfo.BoatName);
                        if (!string.IsNullOrEmpty(s.NonResidentInfo.HotelName))
                            res.Add(nameof(s.NonResidentInfo.HotelName), s.NonResidentInfo.HotelName);
                        if (!string.IsNullOrEmpty(s.NonResidentInfo.Marina))
                            res.Add(nameof(s.NonResidentInfo.Marina), s.NonResidentInfo.Marina);
                        if (!string.IsNullOrEmpty(s.NonResidentInfo.School))
                            res.Add(nameof(s.NonResidentInfo.School), s.NonResidentInfo.School);
                        if (s.NonResidentInfo.ArrivalDate != null)
                            res.Add(nameof(s.NonResidentInfo.ArrivalDate), s.NonResidentInfo.ArrivalDate);
                        if (s.NonResidentInfo.DepartureDate != null)
                            res.Add(nameof(s.NonResidentInfo.DepartureDate), s.NonResidentInfo.DepartureDate);


                        var msg2 = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.State.Value.Entity.Id,
                                "NonResident",
                                "General",
                                "Personal Information",
                                res
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.NonResidentInfo = null;

                        return msg2;
                    }),
                GetCreatePhoneNumber(),
                GetCreateContactAddress(),
                GetCreateNextOfKin(),
                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityListWithChanges<IPatients>>(
                    key: "SaveForeignPhoneNumber",
                    subject: s => Observable
                        .Empty<ReactiveCommand<IViewModel, Unit>>(), //TODO:Try to findway to get change notification
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v => v.State?.Value?.Entity != null &&
                             v.State.Value.Entity.Id != 0 &&
                             v.CurrentForeignPhoneNumber.Value.Id == 0 &&
                             !string.IsNullOrEmpty(v.CurrentForeignPhoneNumber.Value.PhoneNumber)
                             && !string.IsNullOrEmpty(v.CurrentForeignPhoneNumber.Value.PhoneType)
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res1 = new Dictionary<string, object>()
                        {
                            {s.CurrentForeignPhoneNumber.Value.PhoneType, s.CurrentForeignPhoneNumber.Value.PhoneNumber},
                        };
                        var msg3 = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.State.Value.Entity.Id,
                                s.CurrentForeignPhoneNumber.Value.Id,
                                "NonResident",
                                "PhoneNumber",
                                "General",
                                "NonResident Information",
                                res1
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.CurrentForeignPhoneNumber.Value = new ForeignPhoneNumberInfo() {PhoneNumber = "Create New..."};
                        return msg3;
                    }),
                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key: "SaveForeignAddress",
                    subject: s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v => v.State?.Value?.Entity != null &&
                             v.State.Value.Entity.Id != 0 &&
                             v.CurrentForeignAddress.Value.Id == 0 &&
                             !string.IsNullOrEmpty(v.CurrentForeignAddress.Value.Address)
                             && !string.IsNullOrEmpty(v.CurrentForeignAddress.Value.AddressType)
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res2 = new Dictionary<string, object>();

                        if (!string.IsNullOrEmpty(s.CurrentForeignAddress.Value.AddressType))
                            res2.Add(nameof(s.CurrentForeignAddress.Value.AddressType),
                                s.CurrentForeignAddress.Value.AddressType);
                        if (!string.IsNullOrEmpty(s.CurrentForeignAddress.Value.Addresslines))
                            res2.Add(nameof(s.CurrentForeignAddress.Value.Addresslines), s.CurrentForeignAddress.Value.Addresslines);
                        if (!string.IsNullOrEmpty(s.CurrentForeignAddress.Value.ZipOrPostalCode))
                            res2.Add(nameof(s.CurrentForeignAddress.Value.ZipOrPostalCode), s.CurrentForeignAddress.Value.ZipOrPostalCode);
                        if (!string.IsNullOrEmpty(s.CurrentForeignAddress.Value.City))
                            res2.Add(nameof(s.CurrentForeignAddress.Value.City), s.CurrentForeignAddress.Value.City);
                        if (!string.IsNullOrEmpty(s.CurrentForeignAddress.Value.Country))
                            res2.Add(nameof(s.CurrentForeignAddress.Value.Country), s.CurrentForeignAddress.Value.Country);
                        if (!string.IsNullOrEmpty(s.CurrentForeignAddress.Value.State))
                            res2.Add(nameof(s.CurrentForeignAddress.Value.State), s.CurrentForeignAddress.Value.State);

                        var msg4 = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.State.Value.Entity.Id,
                                "NonResident Address",
                                "General",
                                "NonResident Information",
                                res2
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.CurrentForeignAddress.Value = new ForeignAddressInfo() {Addresslines = "Create New..."};
                        return msg4;
                    }),

            },
            viewModelType: typeof(IPatientDetailsViewModel),
            orientation: typeof(IBodyViewModel),
            priority:1);
    }
}