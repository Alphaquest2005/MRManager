using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
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
        private static readonly ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>> createContactAddress = new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                key: "CreateContactAddress",
                subject: s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                {
                    v => v.State?.Value?.Entity != null &&
                         v.State.Value.Entity.Id != 0 &&
                         v.CurrentAddress.Value.Id == 0 &&
                         !string.IsNullOrEmpty(v.CurrentAddress.Value.Address)
                         && !string.IsNullOrEmpty(v.CurrentAddress.Value.AddressType)
                },
                //TODO: Make a type to capture this info... i killing it here
                messageData: s =>
                {
                    var res = new Dictionary<string, object>();

                    if (!string.IsNullOrEmpty(s.CurrentAddress.Value.AddressType))
                        res.Add(nameof(s.CurrentAddress.Value.AddressType), s.CurrentAddress.Value.AddressType);
                    if (!string.IsNullOrEmpty(s.CurrentAddress.Value.Address))
                        res.Add(nameof(s.CurrentAddress.Value.Address), s.CurrentAddress.Value.Address);
                    if (!string.IsNullOrEmpty(s.CurrentAddress.Value.Parish))
                        res.Add(nameof(s.CurrentAddress.Value.Parish), s.CurrentAddress.Value.Parish);
                    if (!string.IsNullOrEmpty(s.CurrentAddress.Value.City))
                        res.Add(nameof(s.CurrentAddress.Value.City), s.CurrentAddress.Value.City);
                    if (!string.IsNullOrEmpty(s.CurrentAddress.Value.Country))
                        res.Add(nameof(s.CurrentAddress.Value.Country), s.CurrentAddress.Value.Country);
                    if (!string.IsNullOrEmpty(s.CurrentAddress.Value.State))
                        res.Add(nameof(s.CurrentAddress.Value.State), s.CurrentAddress.Value.State);

                    var msg = new ViewEventCommandParameter(
                        new object[]
                        {
                            s.State.Value.Entity.Id,
                            "Contact Address",
                            // "Address",
                            "General",
                            "Contact Information",
                            res
                        },
                        new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                        s.Source);
                    s.CurrentAddress.Value = new PersonAddressInfo() {Address = "Create New..."};
                    return msg;
                });

        public static ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>> GetCreateContactAddress()
        {
            return createContactAddress;
        }
    }
}