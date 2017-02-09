using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
using Actor.Interfaces;
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
                

                

                new ViewEventSubscription<IPatientSyntomViewModel, IEntityFound<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSyntomViewModel, IEntityFound<IPatientSyntomInfo>, bool>>(),
                    (v, e) =>
                    {

                       var f = v.EntitySet.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentEntity.Value.Id == e.Entity.Id) v.CurrentEntity.Value = e.Entity;
                        if (f == null)
                        {
                            v.EntitySet.Insert(v.EntitySet.Count() - 1,e.Entity);

                        }
                        else
                        {
                            //f = e.Entity;
                            var idx = v.EntitySet.IndexOf(f);
                            v.EntitySet.Remove(f);
                            v.EntitySet.Insert(idx, e.Entity);
                            v.EntitySet.Reset();
                        }


                    }),
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

                new ViewEventCommand<IPatientSyntomViewModel, IUpdateEntityWithChanges<IPatientSyntoms>>(
                    key:"CreatePatientSyntom",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IPatientSyntomViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count == 4 
                        && (v.ChangeTracking.ContainsKey(nameof(IPatientSyntomInfo.Syntom)) 
                                && v.ChangeTracking[nameof(IPatientSyntomInfo.Syntom)] != null)
                        && v.CurrentEntity.Value.Id == 0

                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        v.ChangeTracking.Add(nameof(IPatientSyntomInfo.PatientVisitId), v.CurrentPatientVisit.Id);
                        var syntomId = v.ChangeTracking[nameof(IPatientSyntomInfo.Syntom)].Id;
                        v.ChangeTracking.Remove(nameof(IPatientSyntomInfo.Syntom));
                        v.ChangeTracking.Add(nameof(IPatientSyntomInfo.SyntomId), syntomId);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.CurrentEntity.Value.Id,
                                v.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),

                new ViewEventCommand<IPatientSyntomViewModel, IUpdateEntityViewWithChanges<IPatientSyntomInfo>>(
                    key:"EditPatientSyntom",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IPatientSyntomViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count(x => x.Value != null) == 1 && v.CurrentEntity.Value.Id != 0

                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        v.ChangeTracking.Add(nameof(IPatientSyntomInfo.PatientVisitId), v.CurrentPatientVisit.Id);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.CurrentEntity.Value.Id,
                                v.ChangeTracking.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),
                 new ViewEventCommand<IPatientSyntomViewModel, IUpdateEntityWithChanges<ISyntoms>>(
                    key:"CreateSyntom",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IPatientSyntomViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count == 2
                        && v.ChangeTracking.ContainsKey(nameof(IPatientSyntomInfo.SyntomName))
                        && v.ChangeTracking.ContainsKey(nameof(IPatientSyntomInfo.Syntom)) && v.ChangeTracking[nameof(IPatientSyntomInfo.Syntom)] == null
                        //&& v.ChangeTracking.ContainsKey(nameof(IPatientSyntomInfo.SyntomId))
                        //&& string.IsNullOrEmpty(v.ChangeTracking[nameof(IPatientSyntomInfo.SyntomId)])

                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        
                        var val = v.ChangeTracking[nameof(IPatientSyntomInfo.SyntomName)];
                        v.ChangeTracking.Remove(nameof(IPatientSyntomInfo.SyntomName));
                        
                        
                        v.ChangeTracking.Add(nameof(ISyntoms.Name),val);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                0,
                                v.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),

            },
            typeof(IPatientSyntomViewModel),
            typeof(IBodyViewModel));

        public class ComplexActions
        {
            public static IComplexEventAction UpdatePatientInfo = new ComplexEventAction(
                key: "UpdatePatientSyntom",
                processId: 3,
                events: new List<IProcessExpectedEvent>
                {
                new ProcessExpectedEvent<IEntityUpdated<IPatientSyntoms>> (processId: 3,
                    eventPredicate: e => e.Entity != null,
                    processInfo: new StateEventInfo(3, Context.Entity.Events.EntityUpdated),
                    expectedSourceType: new SourceType(typeof(IEntityRepository)),
                    key: "UpdatedPatientSyntom")
                },
                expectedMessageType: typeof(IProcessStateMessage<IPatientDetailsInfo>),
                action: GetPatientSyntom,
                processInfo: new StateCommandInfo(3, Context.Process.Commands.UpdateState));

            public static IProcessAction GetPatientSyntom => new ProcessAction(
                action: cp => new GetEntityViewById<IPatientSyntomInfo>(cp.Messages["UpdatedPatientSyntom"].Entity.Id,
                                new StateCommandInfo(cp.Actor.Process.Id, Context.EntityView.Commands.GetEntityView),
                                cp.Actor.Process, cp.Actor.Source),
                processInfo: cp =>
                        new StateCommandInfo(cp.Actor.Process.Id,
                            Context.EntityView.Commands.GetEntityView),
                // take shortcut cud be IntialState
                expectedSourceType: new SourceType(typeof(IComplexEventService))

                );
        }
    }
}