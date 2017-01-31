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
                new ViewEventSubscription<IPatientSyntomViewModel, IProcessStateListMessage<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSyntomViewModel, IProcessStateListMessage<IPatientSyntomInfo>, bool>>(),
                    (v,e) => v.State.Value = e.State),
                new ViewEventSubscription<IPatientSyntomViewModel, ICurrentEntityChanged<IPatientVisitInfo>>(
                    3,
                    e => e?.Entity != null,
                    new List<Func<IPatientSyntomViewModel, ICurrentEntityChanged<IPatientVisitInfo>, bool>>(),
                    (v,e) => v.CurrentPatientVisit = e.Entity),
            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IPatientSyntomViewModel, ViewStateLoaded<IPatientSyntomViewModel,IProcessStateList<IPatientSyntomInfo>>>(
                    key:"IPatientInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IPatientSyntomViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                new ViewEventPublication<IPatientSyntomViewModel, CurrentEntityChanged<IPatientSyntomInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IPatientSyntomViewModel, bool>>{},
                    messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IPatientSyntomViewModel, LoadEntityViewSetWithChanges<IPatientSyntomInfo,IPartialMatch>>(
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



            },
            typeof(IPatientSyntomViewModel),
            typeof(IBodyViewModel));
    }
}