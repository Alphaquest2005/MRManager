using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
using Interfaces;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewMessages;
using ViewModel.Interfaces;

namespace RevolutionData
{
}

namespace RevolutionData
{
    public class FooterViewModelInfo
    {
        public static readonly ViewModelInfo FooterViewModel = new ViewModelInfo
            (
            3,
            new ViewInfo("Footer","",""), 
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IFooterViewModel, ICurrentEntityChanged<IPatientInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IFooterViewModel, ICurrentEntityChanged<IPatientInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentPatient.Value == e.Entity) return;
                        v.CurrentPatient.Value = e.Entity;
                        v.CurrentInterview.Value = null;
                        v.CurrentPatientSyntom.Value = null;
                        v.CurrentPatientVisit.Value = null;
                    }),

                new ViewEventSubscription<IFooterViewModel, ICurrentEntityChanged<IPatientVisitInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IFooterViewModel, ICurrentEntityChanged<IPatientVisitInfo>, bool>>()
                    {
                        (v, e) => (e.Entity == null) || (e.Entity != null && v.CurrentPatient.Value != null && e.Entity.PatientId == v.CurrentPatient.Value.Id)
                    },
                    (v, e) =>
                    {
                        if (v.CurrentPatientVisit.Value == e.Entity) return;
                        v.CurrentPatientVisit.Value = e.Entity;
                        v.CurrentPatientSyntom.Value = null;
                        v.CurrentInterview.Value = null;
                    }),

                new ViewEventSubscription<IFooterViewModel, ICurrentEntityChanged<IPatientSyntomInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IFooterViewModel, ICurrentEntityChanged<IPatientSyntomInfo>, bool>>()
                    {
                        (v, e) =>v.CurrentPatientVisit.Value != null && e.Entity.PatientVisitId == v.CurrentPatientVisit.Value.Id
                    },
                    (v, e) =>
                    {
                        if (v.CurrentPatientSyntom.Value == e.Entity) return;
                        v.CurrentPatientSyntom.Value = e.Entity;
                        v.CurrentInterview.Value = null;
                    }),

                new ViewEventSubscription<IFooterViewModel, ICurrentEntityChanged<IInterviewInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IFooterViewModel, ICurrentEntityChanged<IInterviewInfo>, bool>>()
                    {
                        //(v, e) => v.CurrentPatientSyntom.Value?.MedicalSystems != null && v.CurrentPatientSyntom.Value.MedicalSystems.Any(x => x.MedicalSystemId == e.Entity.SystemId)
                    },
                    (v, e) =>
                    {
                        if (v.CurrentInterview.Value == e.Entity) return;
                        v.CurrentInterview.Value = e.Entity;

                    }),
            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>{},
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IFooterViewModel, INavigateToView>(
                    key:"ViewHome",
                    commandPredicate:new List<Func<IFooterViewModel, bool>>{},
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s => new ViewEventCommandParameter(
                        new object[] {ViewMessageConst.Instance.ViewHome},
                        new StateCommandInfo(s.Process.Id,
                            Context.View.Commands.NavigateToView), s.Process,
                        s.Source)),

                new ViewEventCommand<IFooterViewModel, INavigateToView>(
                    key:"ViewPatientSummary",
                    commandPredicate:new List<Func<IFooterViewModel, bool>>{},
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s => new ViewEventCommandParameter(
                        new object[] {ViewMessageConst.Instance.ViewPatientSummary},
                        new StateCommandInfo(s.Process.Id,
                            Context.View.Commands.NavigateToView), s.Process,
                        s.Source)),

                new ViewEventCommand<IFooterViewModel, INavigateToView>(
                    key:"ViewPatientVisit",
                    commandPredicate:new List<Func<IFooterViewModel, bool>>{},
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s => new ViewEventCommandParameter(
                        new object[] {ViewMessageConst.Instance.ViewPatientVisit},
                        new StateCommandInfo(s.Process.Id,
                            Context.View.Commands.NavigateToView), s.Process,
                        s.Source)),

                new ViewEventCommand<IFooterViewModel, INavigateToView>(
                    key:"ViewPatientSyntom",
                    commandPredicate:new List<Func<IFooterViewModel, bool>>{},
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s => new ViewEventCommandParameter(
                        new object[] {ViewMessageConst.Instance.ViewPatientSyntom},
                        new StateCommandInfo(s.Process.Id,
                            Context.View.Commands.NavigateToView), s.Process,
                        s.Source)),

                new ViewEventCommand<IFooterViewModel, INavigateToView>(
                    key:"ViewInterview",
                    commandPredicate:new List<Func<IFooterViewModel, bool>>{},
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s => new ViewEventCommandParameter(
                        new object[] {ViewMessageConst.Instance.ViewInterview},
                        new StateCommandInfo(s.Process.Id,
                            Context.View.Commands.NavigateToView), s.Process,
                        s.Source)),

                new ViewEventCommand<IFooterViewModel, INavigateToView>(
                    key:"ViewPatientResponses",
                    commandPredicate:new List<Func<IFooterViewModel, bool>>{},
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s => new ViewEventCommandParameter(
                        new object[] {ViewMessageConst.Instance.ViewPatientResponses},
                        new StateCommandInfo(s.Process.Id,
                            Context.View.Commands.NavigateToView), s.Process,
                        s.Source)),
                   



            },
            typeof(IFooterViewModel),
            typeof(IFooterViewModel), 0);
    }
}