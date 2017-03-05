using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using EF.Entities;
using Interfaces;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class PatientVisitViewModelInfo
    {
        public static ViewModelInfo PatientVisitViewModel = new ViewModelInfo
            (
            3,
            new ViewInfo("PatientVisit", "", "Patient Visits"),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IPatientVisitViewModel, IUpdateProcessStateList<IPatientVisitInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientVisitViewModel, IUpdateProcessStateList<IPatientVisitInfo>, bool>>(),
                    (v,e) => 
                    {
                        if (v.State.Value == e.State) return;
                        v.State.Value = e.State;
                    }),
                new ViewEventSubscription<IPatientVisitViewModel, ICurrentEntityChanged<IPatientInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientVisitViewModel, ICurrentEntityChanged<IPatientInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentPatient == e.Entity) return;
                        v.CurrentPatient = e.Entity;
                    }),
                new ViewEventSubscription<IPatientVisitViewModel, IEntityViewWithChangesUpdated<IPatientVisitInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientVisitViewModel, IEntityViewWithChangesUpdated<IPatientVisitInfo>, bool>>(),
                    (v, e) =>
                    {

                       var f = v.EntitySet.Value.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentEntity.Value.Id == e.Entity.Id) v.CurrentEntity.Value = e.Entity;
                        if (f == null)
                        {
                            v.EntitySet.Value.Insert(v.EntitySet.Value.Count() - 1,e.Entity);

                        }
                        else
                        {
                            //f = e.Entity;
                            var idx = v.EntitySet.Value.IndexOf(f);
                            v.EntitySet.Value.Remove(f);
                            v.EntitySet.Value.Insert(idx, e.Entity);
                            v.EntitySet.Value.Reset();
                        }
                        v.RowState.Value = RowState.Unchanged;

                    }),


            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IPatientVisitViewModel, IViewStateLoaded<IPatientVisitViewModel,IProcessStateList<IPatientVisitInfo>>>(
                    key:"IPatientInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IPatientVisitViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s =>
                    {
                         Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            s.EntitySet.Value.Add(new PatientVisitInfo() {Purpose = "Create New..."});
                            s.NotifyPropertyChanged(nameof(s.EntitySet));
                        }));

                        return new ViewEventPublicationParameter(new object[] {s, s.State.Value},
                            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    }),

                new ViewEventPublication<IPatientVisitViewModel, ICurrentEntityChanged<IPatientVisitInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IPatientVisitViewModel, bool>>{},
                    messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded),s.Process,s.Source))
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IPatientVisitViewModel, ILoadEntityViewSetWithChanges<IPatientVisitInfo,IPartialMatch>>(
                    key:"Search",
                    commandPredicate:new List<Func<IPatientVisitViewModel, bool>>
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

                new ViewEventCommand<IPatientVisitViewModel, IViewRowStateChanged<IPatientVisitInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IPatientVisitViewModel, bool>>
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

                new ViewEventCommand<IPatientVisitViewModel, IUpdateEntityViewWithChanges<IPatientVisitInfo>>(
                    key:"CreatePatientVisit",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IPatientVisitViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count == 3 && v.CurrentEntity.Value.Id == 0

                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        v.ChangeTracking.Add(nameof(IPatientVisitInfo.PatientId), v.CurrentPatient.Id);
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

                new ViewEventCommand<IPatientVisitViewModel, IUpdateEntityViewWithChanges<IPatientVisitInfo>>(
                    key:"EditPatientVisit",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IPatientVisitViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count == 1 && v.CurrentEntity.Value.Id != 0

                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        v.ChangeTracking.Add(nameof(IPatientVisitInfo.PatientId), v.CurrentPatient.Id);
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

            },
            typeof(IPatientVisitViewModel),
            typeof(IBodyViewModel), 2);

    }
}