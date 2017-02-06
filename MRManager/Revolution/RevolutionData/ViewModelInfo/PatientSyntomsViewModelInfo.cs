using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
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
    public class PatientSyntomViewModelInfo
    {
        public static ViewModelInfo PatientSyntomViewModel = new ViewModelInfo
            (
            3, new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IPatientSyntomViewModel, IUpdateProcessStateList<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSyntomViewModel, IUpdateProcessStateList<IPatientSyntomInfo>, bool>>(),
                    (v,e) => v.State.Value = e.State),
                new ViewEventSubscription<IPatientSyntomViewModel, ICurrentEntityChanged<IPatientVisitInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSyntomViewModel, ICurrentEntityChanged<IPatientVisitInfo>, bool>>(),
                    (v,e) => v.CurrentPatientVisit = e.Entity),
            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IPatientSyntomViewModel, IViewStateLoaded<IPatientSyntomViewModel,IProcessStateList<IPatientSyntomInfo>>>(
                    key:"IPatientInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IPatientSyntomViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                new ViewEventPublication<IPatientSyntomViewModel, ICurrentEntityChanged<IPatientSyntomInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IPatientSyntomViewModel, bool>>{},
                    messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IPatientSyntomViewModel, ILoadEntityViewSetWithChanges<IPatientSyntomInfo,IPartialMatch>>(
                    key:"Search",
                    commandPredicate:new List<Func<IPatientSyntomViewModel, bool>>
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

                new ViewEventCommand<IPatientSyntomViewModel, IViewRowStateChanged<IPatientSyntomInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IPatientSyntomViewModel, bool>>
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
            typeof(IPatientSyntomViewModel),
            typeof(IBodyViewModel));
    }
}