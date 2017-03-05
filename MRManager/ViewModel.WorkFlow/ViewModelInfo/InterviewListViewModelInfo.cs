using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using EF.Entities;
using Interfaces;
using JB.Collections.Reactive;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace ViewModel.WorkFlow.ViewModelInfo
{
    public class InterviewListViewModelInfo
    {
        public static readonly RevolutionEntities.ViewModels.ViewModelInfo InterviewListViewModel = new RevolutionEntities.ViewModels.ViewModelInfo
            (
            3,
            new ViewInfo("Interview", "", "Interviews"),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IInterviewListViewModel, IUpdateProcessStateList<ISyntomMedicalSystemInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IInterviewListViewModel, IUpdateProcessStateList<ISyntomMedicalSystemInfo>, bool>>(),
                    (v, e) =>
                    {
                        if(!v.Systems.Value.SequenceEqual(e.State.EntitySet.ToList()))
                        v.Systems.Value = new ObservableList<ISyntomMedicalSystemInfo>(e.State.EntitySet.ToList());
                        v.Systems.Value.Add(new SyntomMedicalSystemInfo() { System = "Create New..." });
                    }),

                new ViewEventSubscription<IInterviewListViewModel, ICurrentEntityChanged<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IInterviewListViewModel, ICurrentEntityChanged<IPatientSyntomInfo>, bool>>(),
                    (v, e) => { if (v.CurrentPatientSyntom.Value != e.Entity) v.CurrentPatientSyntom.Value = e.Entity; }),


                new ViewEventSubscription<IInterviewListViewModel, IEntityViewWithChangesUpdated<ISyntomMedicalSystemInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IInterviewListViewModel, IEntityViewWithChangesUpdated<ISyntomMedicalSystemInfo>, bool>>(),
                    (v, e) =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {


                       var f = v.Systems.Value.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentMedicalSystem.Value.Id == e.Entity.Id) v.CurrentMedicalSystem.Value = e.Entity;
                        if (f == null)
                        {
                            v.Systems.Value.Insert(v.Systems.Value.Count() - 1,e.Entity);
                                v.Systems.Value.Reset();
                        }
                        else
                        {
                            //f = e.Entity;
                            var idx = v.Systems.Value.IndexOf(f);
                            v.Systems.Value.Remove(f);
                            v.Systems.Value.Insert(idx, e.Entity);
                            v.Systems.Value.Reset();
                        }
                        v.RowState.Value = RowState.Unchanged;
                        }));

                    }),

                new ViewEventSubscription<IInterviewListViewModel, IEntityViewWithChangesFound<ISyntomMedicalSystemInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IInterviewListViewModel, IEntityViewWithChangesFound<ISyntomMedicalSystemInfo>, bool>>(),
                    (v, e) =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {


                       var f = v.Systems.Value.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentMedicalSystem.Value.Id == e.Entity.Id) v.CurrentMedicalSystem.Value = e.Entity;
                        if (f == null)
                        {
                            v.Systems.Value.Insert(v.Systems.Value.Count() - 1,e.Entity);
                            v.Systems.Value.Reset();
                        }
                        else
                        {
                            //f = e.Entity;
                            var idx = v.Systems.Value.IndexOf(f);
                            v.Systems.Value.Remove(f);
                            v.Systems.Value.Insert(idx, e.Entity);
                            v.Systems.Value.Reset();
                        }
                        v.RowState.Value = RowState.Unchanged;
                        }));

                    }),

                ///////////////////// interviews

                new ViewEventSubscription<IInterviewListViewModel, IEntityViewWithChangesUpdated<IInterviewInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IInterviewListViewModel, IEntityViewWithChangesUpdated<IInterviewInfo>, bool>>(),
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

                new ViewEventSubscription<IInterviewListViewModel, IEntityViewWithChangesFound<IInterviewInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IInterviewListViewModel, IEntityViewWithChangesFound<IInterviewInfo>, bool>>(),
                    (v, e) =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {


                       var f = v.EntitySet.Value.FirstOrDefault(x => x.Id == e.Entity.Id);
                            if (e.Entity.SystemId == 0)
                            {
                                v.CurrentMedicalSystem.Value.Interviews.Add(e.Entity);
                                v.ChangeTracking.Clear();
                                v.ChangeTracking.Add(nameof(IMedicalSystemInterviews.MedicalSystemId), v.CurrentMedicalSystem.Value.MedicalSystemId);
                                v.ChangeTracking.Add(nameof(IMedicalSystemInterviews.InterviewId), e.Entity.Id);
                            }

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
                new ViewEventPublication<IInterviewListViewModel, IViewStateLoaded<IInterviewListViewModel,IProcessStateList<IInterviewInfo>>>(
                    key:"IPatientInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IInterviewListViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s => new ViewEventPublicationParameter(new object[] {s,s.State.Value},new StateEventInfo(s.Process.Id, RevolutionData.Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                new ViewEventPublication<IInterviewListViewModel, ICurrentEntityChanged<IInterviewInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IInterviewListViewModel, bool>>{},
                    messageData:s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},new StateEventInfo(s.Process.Id, RevolutionData.Context.View.Events.ProcessStateLoaded),s.Process,s.Source)),

                new ViewEventPublication<IInterviewListViewModel, ICurrentEntityChanged<ISyntomMedicalSystemInfo>>(
                    key:"CurrentSystemChanged",
                    subject:v => v.CurrentMedicalSystem,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IInterviewListViewModel, bool>>{},
                    messageData:s =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() => { 
                                                                                   IList<IInterviewInfo> observableList = s.CurrentMedicalSystem.Value.Interviews;
                        
                                                                                   var res = observableList?.ToList() ?? new List<IInterviewInfo>();
                                                                                   res.Add(new InterviewInfo() {Interview = "Create New..."});

                                                                                   s.EntitySet.Value = new ObservableList<IInterviewInfo>(res);
                                                                                   s.NotifyPropertyChanged(nameof(s.EntitySet));
                                                                                   s.CurrentEntity.Value = res.First();

                                                                    }));
                        return new ViewEventPublicationParameter(new object[] {s.CurrentMedicalSystem.Value},
                            new StateEventInfo(s.Process.Id, RevolutionData.Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    }),
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IInterviewListViewModel, ILoadEntityViewSetWithChanges<IInterviewInfo,IPartialMatch>>(
                    key:"Search",
                    commandPredicate:new List<Func<IInterviewListViewModel, bool>>
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
                                RevolutionData.Context.EntityView.Commands.LoadEntityViewSetWithChanges), s.Process,
                            s.Source);
                    }),

                new ViewEventCommand<IInterviewListViewModel, IViewRowStateChanged<IInterviewInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IInterviewListViewModel, bool>>
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
                                RevolutionData.Context.Process.Commands.CurrentEntityChanged), s.Process,
                            s.Source);
                    }),

                new ViewEventCommand<IInterviewListViewModel, IUpdateEntityWithChanges<ISyntomMedicalSystems>>(
                    key:"CreateSyntomMedicalSystem",
                    subject:v => v.WhenAnyValue(x => x.SelectedMedicalSystem.Value),
                    commandPredicate: new List<Func<IInterviewListViewModel, bool>>
                    {
                       v => v.CurrentMedicalSystem.Value.Id == 0 &&  v.SelectedMedicalSystem.Value.Id != 0 && v.CurrentPatientSyntom.Value?.Id != 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        var res = v.ChangeTracking;
                        res.Add(nameof(ISyntomMedicalSystems.SyntomId), v.CurrentPatientSyntom.Value.SyntomId);
                        res.Add(nameof(ISyntomMedicalSystems.MedicalSystemId), v.SelectedMedicalSystem.Value.Id);

                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.CurrentMedicalSystem.Value.Id,
                                res.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, RevolutionData.Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),

                new ViewEventCommand<IInterviewListViewModel, IUpdateEntityViewWithChanges<ISyntomMedicalSystemInfo>>(
                    key:"EditISyntomMedicalSystemInfo",
                    subject:v => v.WhenAnyValue(x => x.SelectedMedicalSystem.Value),
                    commandPredicate: new List<Func<IInterviewListViewModel, bool>>
                    {
                       v => v.CurrentMedicalSystem.Value.Id != 0 
                            &&  v.SelectedMedicalSystem.Value.Id != 0 
                            && v.CurrentPatientSyntom.Value?.Id != 0
                            && v.CurrentMedicalSystem.Value.MedicalSystemId != v.SelectedMedicalSystem.Value.Id
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        var res = v.ChangeTracking;
                        res.Add(nameof(ISyntomMedicalSystems.MedicalSystemId), v.SelectedMedicalSystem.Value.Id);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.CurrentMedicalSystem.Value.Id,
                                res.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, RevolutionData.Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),

                ////////////////////////////////Interview section
                
                new ViewEventCommand<IInterviewListViewModel, IUpdateEntityWithChanges<IInterviews>>(
                    key:"CreateInterview",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IInterviewListViewModel, bool>>
                    {
                       v => v.CurrentMedicalSystem.Value.Id != 0 &&  v.SelectedMedicalSystem.Value.Id != 0 && v.CurrentPatientSyntom.Value?.Id != 0
                            && v.CurrentEntity.Value?.Id == 0 && v.ChangeTracking.Count == 3
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        var res = v.ChangeTracking;
                        var name = res[nameof(IInterviewInfo.Interview)];
                        res.Remove(nameof(IInterviewInfo.Interview));
                        res.Add(nameof(IInterviews.Name), name);
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.CurrentEntity.Value.Id,
                                res.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, RevolutionData.Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),

                new ViewEventCommand<IInterviewListViewModel, IUpdateEntityViewWithChanges<IInterviewInfo>>(
                    key:"EditInterview",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IInterviewListViewModel, bool>>
                    {
                       v => v.CurrentMedicalSystem.Value.Id != 0 &&  v.SelectedMedicalSystem.Value.Id != 0 && v.CurrentPatientSyntom.Value?.Id != 0
                            && v.CurrentEntity.Value.Id != 0 && v.ChangeTracking.Count >= 1
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        var res = v.ChangeTracking;
                        if (res.ContainsKey(nameof(IInterviewInfo.Interview)))
                        {
                            var name = res[nameof(IInterviewInfo.Interview)];
                            res.Remove(nameof(IInterviewInfo.Interview));
                            res.Add(nameof(IInterviews.Name), name);
                        }
                        
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                v.CurrentEntity.Value.Id,
                                res.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, RevolutionData.Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),


                /////////////////////////// medical interviews
                 new ViewEventCommand<IInterviewListViewModel, IUpdateEntityWithChanges<IMedicalSystemInterviews>>(
                    key:"CreateSystemInterview",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IInterviewListViewModel, bool>>
                    {
                       v => v.ChangeTracking.Count == 2 && v.ChangeTracking.ContainsKey(nameof(IMedicalSystemInterviews.InterviewId))
                            &&  v.ChangeTracking.ContainsKey(nameof(IMedicalSystemInterviews.MedicalSystemId))
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: v =>
                    {
                        var res = v.ChangeTracking;
                       var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                0,
                                res.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(v.Process.Id, RevolutionData.Context.EntityView.Commands.GetEntityView), v.Process,
                            v.Source);
                        v.ChangeTracking.Clear();
                        return msg;
                    }),

            },
            typeof(IInterviewListViewModel),
            typeof(IBodyViewModel),4);


    }
}