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
    public class PatientSyntomViewModelInfo
    {
        public static ViewModelInfo PatientSyntomViewModel = new ViewModelInfo
            (
            3,
            new ViewInfo("PatientSyntom", "", "Patient Syntoms"),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IPatientSyntomViewModel, IUpdateProcessStateList<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSyntomViewModel, IUpdateProcessStateList<IPatientSyntomInfo>, bool>>(),
                    (v,e) =>
                    {
                        if (v.State.Value == e.State) return;
                        v.State.Value = e.State;
                    }),

                new ViewEventSubscription<IPatientSyntomViewModel, ICurrentEntityChanged<IPatientVisitInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSyntomViewModel, ICurrentEntityChanged<IPatientVisitInfo>, bool>>(),
                    (v, e) =>
                    {
                        if (v.CurrentPatientVisit.Value == e.Entity) return;
                        v.CurrentPatientVisit.Value = e.Entity;
                    }),



                new ViewEventSubscription<IPatientSyntomViewModel, IEntityViewWithChangesFound<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSyntomViewModel, IEntityViewWithChangesFound<IPatientSyntomInfo>, bool>>(),
                    (v, e) =>
                    {
                       Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {


                       var f = v.EntitySet.Value.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentEntity.Value.Id == e.Entity.Id) v.CurrentEntity.Value = e.Entity;
                        if (f == null)
                        {
                            v.EntitySet.Value.Insert(v.EntitySet.Value.Count() - 1,e.Entity);
                            v.EntitySet.Value.Reset();
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
                        }));

                    }),

                new ViewEventSubscription<IPatientSyntomViewModel, IEntityViewWithChangesFound<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSyntomViewModel, IEntityViewWithChangesFound<IPatientSyntomInfo>, bool>>(),
                    (v, e) =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {


                       var f = v.EntitySet.Value.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentEntity.Value.Id == e.Entity.Id) v.CurrentEntity.Value = e.Entity;
                        if (f == null)
                        {
                            v.EntitySet.Value.Insert(v.EntitySet.Value.Count() - 1,e.Entity);
                                v.EntitySet.Value.Reset();
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
                        }));

                    }),

                new ViewEventSubscription<IPatientSyntomViewModel, IEntityViewWithChangesUpdated<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSyntomViewModel, IEntityViewWithChangesUpdated<IPatientSyntomInfo>, bool>>(),
                    (v, e) =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {


                       var f = v.EntitySet.Value.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentEntity.Value.Id == e.Entity.Id) v.CurrentEntity.Value = e.Entity;
                        if (f == null)
                        {
                            v.EntitySet.Value.Insert(v.EntitySet.Value.Count() - 1,e.Entity);
                                v.EntitySet.Value.Reset();
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
                        }));

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
                    messageData:s =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (s.CurrentPatientVisit != null &&
                                (s.CurrentPatientVisit.Value?.Id != 0 &&
                                 s.CurrentPatientVisit.Value?.DateOfVisit.Date == DateTime.Today.Date))
                            {

                                s.EntitySet.Value.Add(new PatientSyntomInfo() {SyntomName = "Create New..."});
                                s.NotifyPropertyChanged(nameof(s.EntitySet));
                            }

                        }));
                       return new ViewEventPublicationParameter(new object[] {s, s.State.Value},
                            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    }),

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
                        var res = v.ChangeTracking;
                        res.Add(nameof(IPatientSyntomInfo.PatientVisitId), v.CurrentPatientVisit.Value.Id);
                        var syntomId = res[nameof(IPatientSyntomInfo.Syntom)].Id;
                        res.Remove(nameof(IPatientSyntomInfo.Syntom));
                        res.Add(nameof(IPatientSyntomInfo.SyntomId), syntomId);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.CurrentEntity.Value.Id,
                                res.ToDictionary(x => x.Key, x => x.Value)
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
                        v => v.ChangeTracking.Any()
                         &&((!v.ChangeTracking.ContainsKey(nameof(IPatientSyntomInfo.Syntom)) && !v.ChangeTracking.ContainsKey(nameof(IPatientSyntomInfo.SyntomName)))
                            || (v.ChangeTracking.ContainsKey(nameof(IPatientSyntomInfo.Syntom))
                                && v.ChangeTracking[nameof(IPatientSyntomInfo.Syntom)] != null))
                        && v.CurrentEntity.Value.Id != 0

                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        var res = v.ChangeTracking;
                        res.Add(nameof(IPatientSyntomInfo.PatientVisitId), v.CurrentPatientVisit.Value.Id);
                        if (res.ContainsKey(nameof(IPatientSyntomInfo.SyntomName)))
                            res.Remove(nameof(IPatientSyntomInfo.SyntomName));
                        if (res.ContainsKey(nameof(IPatientSyntomInfo.Syntom)))
                        {

                            var syntomId = v.ChangeTracking[nameof(IPatientSyntomInfo.Syntom)].Id;
                            res.Remove(nameof(IPatientSyntomInfo.Syntom));
                            res.Add(nameof(IPatientSyntomInfo.SyntomId), syntomId);
                        }

                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.CurrentEntity.Value.Id,
                                res.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),
                 new ViewEventCommand<IPatientSyntomViewModel, IAddOrGetEntityWithChanges<ISyntoms>>(
                    key:"CreateSyntom",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IPatientSyntomViewModel, bool>>
                    {
                        v => v.ChangeTracking.ContainsKey(nameof(IPatientSyntomInfo.SyntomName)) && v.ChangeTracking[nameof(IPatientSyntomInfo.SyntomName)] != ""
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        var res = v.ChangeTracking;

                        var val = res[nameof(IPatientSyntomInfo.SyntomName)];
                        res.Clear();

                        res.Add(nameof(ISyntoms.Name),val);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                res.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        
                        return msg;
                    }),

            },
            typeof(IPatientSyntomViewModel),
            typeof(IBodyViewModel),3);

      

    }
}