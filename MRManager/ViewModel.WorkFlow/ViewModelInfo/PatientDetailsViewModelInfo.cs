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
    public class PatientDetailsViewModelInfo
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
                    key:"SaveNonResidentType",
                    subject:s => s.ChangeTracking.DictionaryChanges, //Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count >= 1
                             && v.State.Value.Entity.Id != 0 
                             && v.ChangeTracking.ContainsKey("Type")
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var msg = new ViewEventCommandParameter(
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

                        return msg;
                    }),


                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key:"SaveNonResidentInfo",
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res = new Dictionary<string, object>();

                        if (!string.IsNullOrEmpty(s.NonResidentInfo.BoatName)) res.Add(nameof(s.NonResidentInfo.BoatName), s.NonResidentInfo.BoatName);
                        if (!string.IsNullOrEmpty(s.NonResidentInfo.HotelName)) res.Add(nameof(s.NonResidentInfo.HotelName), s.NonResidentInfo.HotelName);
                        if (!string.IsNullOrEmpty(s.NonResidentInfo.Marina)) res.Add(nameof(s.NonResidentInfo.Marina), s.NonResidentInfo.Marina);
                        if (!string.IsNullOrEmpty(s.NonResidentInfo.School)) res.Add(nameof(s.NonResidentInfo.School), s.NonResidentInfo.School);
                        if (s.NonResidentInfo.ArrivalDate != null) res.Add(nameof(s.NonResidentInfo.ArrivalDate), s.NonResidentInfo.ArrivalDate);
                        if (s.NonResidentInfo.DepartureDate != null) res.Add(nameof(s.NonResidentInfo.DepartureDate), s.NonResidentInfo.DepartureDate);


                        var msg = new ViewEventCommandParameter(
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

                        return msg;
                    }),

                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityListWithChanges<IPatients>>(
                    key:"CreatePhoneNumber",
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),//TODO:Try to findway to get change notification
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v =>  v.State?.Value?.Entity != null &&
                              v.State.Value.Entity.Id != 0 &&
                              v.CurrentPhoneNumber.Value.Id == 0 &&
                        !string.IsNullOrEmpty(v.CurrentPhoneNumber.Value.PhoneNumber)
                        && !string.IsNullOrEmpty(v.CurrentPhoneNumber.Value.PhoneType)
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res = new Dictionary<string,object>()
                        {
                            {s.CurrentPhoneNumber.Value.PhoneType, s.CurrentPhoneNumber.Value.PhoneNumber },
                        };
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.State.Value.Entity.Id ,
                                s.CurrentPhoneNumber.Value.Id ,
                                "Contact",
                                "PhoneNumber",
                                "General",
                                "Contact Information",
                                res
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process, s.Source);
                        s.CurrentPhoneNumber.Value = new PersonPhoneNumberInfo(){PhoneNumber = "Create New..."};
                            return msg;
                    }),

               

                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key:"CreateContactAddress",
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v =>  v.State?.Value?.Entity != null &&
                              v.State.Value.Entity.Id != 0 &&
                              v.CurrentAddress.Value.Id == 0 &&
                              !string.IsNullOrEmpty(v.CurrentAddress.Value.Address)
                              && !string.IsNullOrEmpty(v.CurrentAddress.Value.AddressType)
                             
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res = new Dictionary<string, object>();

                        if (!string.IsNullOrEmpty(s.CurrentAddress.Value.AddressType)) res.Add(nameof(s.CurrentAddress.Value.AddressType), s.CurrentAddress.Value.AddressType);
                        if (!string.IsNullOrEmpty(s.CurrentAddress.Value.Address)) res.Add(nameof(s.CurrentAddress.Value.Address), s.CurrentAddress.Value.Address);
                        if (!string.IsNullOrEmpty(s.CurrentAddress.Value.Parish)) res.Add(nameof(s.CurrentAddress.Value.Parish), s.CurrentAddress.Value.Parish);
                        if (!string.IsNullOrEmpty(s.CurrentAddress.Value.City)) res.Add(nameof(s.CurrentAddress.Value.City), s.CurrentAddress.Value.City);
                        if (!string.IsNullOrEmpty(s.CurrentAddress.Value.Country)) res.Add(nameof(s.CurrentAddress.Value.Country), s.CurrentAddress.Value.Country);
                        if (!string.IsNullOrEmpty(s.CurrentAddress.Value.State)) res.Add(nameof(s.CurrentAddress.Value.State), s.CurrentAddress.Value.State);
                       
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.State.Value.Entity.Id ,
                                "Contact Address",
                               // "Address",
                                "General",
                                "Contact Information",
                                res
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process, s.Source);
                        s.CurrentAddress.Value = new PersonAddressInfo(){Address = "Create New..."};
                        return msg;
                    }),

                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key:"CreateNextOfKin",
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                    commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v =>  v.State?.Value?.Entity != null &&
                              v.State.Value.Entity.Id != 0 &&
                              v.CurrentNextOfKin.Value.Id == 0 &&
                              !string.IsNullOrEmpty(v.CurrentNextOfKin.Value.Name)
                              && !string.IsNullOrEmpty(v.CurrentNextOfKin.Value.Relationship)
                              && !string.IsNullOrEmpty(v.CurrentNextOfKin.Value.PhoneNumber)

                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var res = new Dictionary<string,object>()
                        {
                            {nameof(INextOfKinInfo.PhoneNumber), s.CurrentNextOfKin.Value.PhoneNumber },
                            {nameof(INextOfKinInfo.Name), s.CurrentNextOfKin.Value.Name },
                            {nameof(INextOfKinInfo.Relationship), s.CurrentNextOfKin.Value.Relationship },
                            {nameof(INextOfKinInfo.Address), s.CurrentNextOfKin.Value.Address },
                        };
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.State.Value.Entity.Id ,
                                "NextOfKin",
                                "General",
                                "Contact Information",
                                res
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process, s.Source);
                        s.CurrentNextOfKin.Value = new NextOfKinInfo(){Name = "Create New..."};
                        return msg;
                    }),

            },
            viewModelType: typeof(IPatientDetailsViewModel),
            orientation: typeof(IBodyViewModel),
            priority:1);

       
    }
}