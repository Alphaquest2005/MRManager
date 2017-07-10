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
        private static readonly ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityListWithChanges<IPatients>> createPhoneNumber = new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityListWithChanges<IPatients>>(
                key: "CreatePhoneNumber",
                subject: s => Observable
                    .Empty<ReactiveCommand<IViewModel, Unit>>(), //TODO:Try to findway to get change notification
                commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                {
                    v => v.State?.Value?.Entity != null &&
                         v.State.Value.Entity.Id != 0 &&
                         v.CurrentPhoneNumber.Value.Id == 0 &&
                         !string.IsNullOrEmpty(v.CurrentPhoneNumber.Value.PhoneNumber)
                         && !string.IsNullOrEmpty(v.CurrentPhoneNumber.Value.PhoneType)
                },
                //TODO: Make a type to capture this info... i killing it here
                messageData: s =>
                {
                    var res = new Dictionary<string, object>()
                    {
                        {s.CurrentPhoneNumber.Value.PhoneType, s.CurrentPhoneNumber.Value.PhoneNumber},
                    };
                    var msg = new ViewEventCommandParameter(
                        new object[]
                        {
                            s.State.Value.Entity.Id,
                            s.CurrentPhoneNumber.Value.Id,
                            "Contact",
                            "PhoneNumber",
                            "General",
                            "Contact Information",
                            res
                        },
                        new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                        s.Source);
                    s.CurrentPhoneNumber.Value = new PersonPhoneNumberInfo() {PhoneNumber = "Create New..."};
                    return msg;
                });

        public static ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityListWithChanges<IPatients>> GetCreatePhoneNumber()
        {
            return createPhoneNumber;
        }
    }
}