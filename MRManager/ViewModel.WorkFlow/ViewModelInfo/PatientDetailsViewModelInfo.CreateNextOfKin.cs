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
        private static readonly ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>> createNextOfKin = new ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                key: "CreateNextOfKin",
                subject: s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),
                commandPredicate: new List<Func<IPatientDetailsViewModel, bool>>
                {
                    v => v.State?.Value?.Entity != null &&
                         v.State.Value.Entity.Id != 0 &&
                         v.CurrentNextOfKin.Value.Id == 0 &&
                         !string.IsNullOrEmpty(v.CurrentNextOfKin.Value.Name)
                         && !string.IsNullOrEmpty(v.CurrentNextOfKin.Value.Relationship)
                         && !string.IsNullOrEmpty(v.CurrentNextOfKin.Value.PhoneNumber)
                },
                //TODO: Make a type to capture this info... i killing it here
                messageData: s =>
                {
                    var res = new Dictionary<string, object>()
                    {
                        {nameof(INextOfKinInfo.PhoneNumber), s.CurrentNextOfKin.Value.PhoneNumber},
                        {nameof(INextOfKinInfo.Name), s.CurrentNextOfKin.Value.Name},
                        {nameof(INextOfKinInfo.Relationship), s.CurrentNextOfKin.Value.Relationship},
                        {nameof(INextOfKinInfo.Address), s.CurrentNextOfKin.Value.Address},
                    };
                    var msg = new ViewEventCommandParameter(
                        new object[]
                        {
                            s.State.Value.Entity.Id,
                            "NextOfKin",
                            "General",
                            "Contact Information",
                            res
                        },
                        new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                        s.Source);
                    s.CurrentNextOfKin.Value = new NextOfKinInfo() {Name = "Create New..."};
                    return msg;
                });

        public static ViewEventCommand<IPatientDetailsViewModel, IUpdatePatientEntityWithChanges<IPatients>> GetCreateNextOfKin()
        {
            return createNextOfKin;
        }
    }
}