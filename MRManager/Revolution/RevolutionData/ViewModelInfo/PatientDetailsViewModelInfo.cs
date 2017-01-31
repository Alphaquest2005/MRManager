using System;
using System.Collections.Generic;
using SystemInterfaces;
using Interfaces;
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
                new ViewEventPublication<IPatientDetailsViewModel, ViewStateLoaded<IPatientDetailsViewModel,IProcessState<IPatientDetailsInfo>>>(
                    key:"PatientDetailsViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IPatientDetailsViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),


            },
            commands: new List<IViewModelEventCommand<IViewModel,IEvent>>{},
            viewModelType: typeof(IPatientDetailsViewModel),
            orientation: typeof(IBodyViewModel));

        public static class ComplexActions
        {
            public static readonly ComplexEventAction UpdatePatientDetailsState = new ComplexEventAction(
                key: "304",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<IEntityFound<IPatientDetailsInfo>> (processId: 3,
                    eventPredicate: e => e.Entity != null,
                    processInfo: new StateEventInfo(3, Context.EntityView.Events.EntityViewFound),
                    expectedSourceType: new SourceType(typeof(IEntityViewRepository)),
                    key: "PatientDetailsInfo")
                },
                expectedMessageType: typeof(IProcessStateMessage<IPatientDetailsInfo>),
                action: ProcessActions.PatientInfo.UpdatePatientDetailsState,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));

            public static readonly ComplexEventAction RequestPatientDetails = new ComplexEventAction(
                key: "303",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<ICurrentEntityChanged<IPatientInfo>> (
                    "CurrentEntity", 3, e => e.Entity != null, expectedSourceType: new SourceType(typeof(IViewModel)),//todo: check this cuz it comes from viewmodel
                    processInfo: new StateEventInfo(2, Context.Process.Events.CurrentEntityChanged))
                },
                expectedMessageType: typeof(IProcessStateMessage<IPatientDetailsInfo>),
                action: ProcessActions.PatientInfo.RequestPatientDetails,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));
        }
    }
}