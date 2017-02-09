using System;
using System.Collections.Generic;
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
    public class PatientDetailsViewModelInfo
    {
        public static readonly ViewModelInfo PatientDetailsViewModel = new ViewModelInfo
            (
            processId: 3,
            subscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription
                    <IPatientDetailsViewModel,
                        IProcessStateMessage<IPatientDetailsInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IPatientDetailsViewModel, IProcessStateMessage<IPatientDetailsInfo>, bool>>(),
                    action: (v, e) => v.State.Value = e.State),
                                   

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
            },
            viewModelType: typeof(IPatientDetailsViewModel),
            orientation: typeof(IBodyViewModel));

       
    }
}