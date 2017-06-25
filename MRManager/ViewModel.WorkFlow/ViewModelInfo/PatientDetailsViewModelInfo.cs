using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
                        v.PhoneNumbers = new List<IPersonPhoneNumberInfo>(e.State.Entity.PhoneNumbers)
                        {
                            new PersonPhoneNumberInfo(){PhoneNumber = "Create New..."}
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

                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityListWithChanges<IPatients>>(
                    key:"CreatePhoneNumber",
                    subject:s => s.CurrentPhoneNumber,
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
                           // {nameof(IPersonPhoneNumberInfo.PhoneNumber), s.CurrentPhoneNumber.Value.PhoneNumber },
                           // {nameof(IPersonPhoneNumberInfo.PhoneType), s.CurrentPhoneNumber.Value.PhoneType },
                        };
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.State.Value.Entity.Id ,
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

                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityListWithChanges<IPatients>>(
                    key:"CreateContactAddress",
                    subject:s => s.CurrentAddress,
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
                        var res = new Dictionary<string,object>()
                        {
                            {s.CurrentAddress.Value.AddressType, s.CurrentAddress.Value.Address },
                            // {nameof(IPersonPhoneNumberInfo.PhoneNumber), s.CurrentPhoneNumber.Value.PhoneNumber },
                            // {nameof(IPersonPhoneNumberInfo.PhoneType), s.CurrentPhoneNumber.Value.PhoneType },
                        };
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                s.State.Value.Entity.Id ,
                                "Contact",
                                "Address",
                                "General",
                                "Contact Information",
                                res
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process, s.Source);
                        s.CurrentAddress.Value = new PersonAddressInfo(){Address = "Create New..."};
                        return msg;
                    }),

                new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityListWithChanges<IPatients>>(
                    key:"CreateNextOfKin",
                    subject:s => s.CurrentNextOfKin,
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
                                "Contact",
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