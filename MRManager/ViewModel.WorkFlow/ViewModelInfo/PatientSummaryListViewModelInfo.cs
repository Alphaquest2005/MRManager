using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using DomainMessages;
using EF.Entities;
using Interfaces;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class PatientSummaryListViewModelInfo
    {
        public static readonly ViewModelInfo PatientSummaryListViewModel = new ViewModelInfo
            (
            3,
            new ViewInfo("PatientSummaryList", "", "Patient List"),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IPatientSummaryListViewModel, IUpdateProcessStateList<IPatientInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSummaryListViewModel, IUpdateProcessStateList<IPatientInfo>, bool>>(),
                    (v, e) =>
                    {
                        
                            if (v.Instance.State.Value == e.State) return;
                            v.Instance.State.Value = e.State;

                        
                    }),

                new ViewEventSubscription<IPatientSummaryListViewModel, IEntityViewWithChangesUpdated<IPatientInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IPatientSummaryListViewModel, IEntityViewWithChangesUpdated<IPatientInfo>, bool>>(),
                    (v, e) =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {


                       var f = v.EntitySet.Value.FirstOrDefault(x => x.Id == e.Entity.Id);
                        if (v.CurrentEntity.Value?.Id == e.Entity.Id) v.CurrentEntity.Value = e.Entity;
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
                new ViewEventPublication<IPatientSummaryListViewModel, IViewStateLoaded<IPatientSummaryListViewModel,IProcessStateList<IPatientInfo>>>(
                    key:"IPatientInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IPatientSummaryListViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s =>
                    {
                        
                            
                        

                       return new ViewEventPublicationParameter(new object[] {s, s.State.Value},
                            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    }),

                new ViewEventPublication<IPatientSummaryListViewModel, ICurrentEntityChanged<IPatientInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,
                    subjectPredicate:new List<Func<IPatientSummaryListViewModel, bool>>{},
                    messageData:s =>
                    {
                        if (s.Instance.EntitySet.Value.FirstOrDefault(x => x.Id == 0) == null)
                        {
                            s.Instance.EntitySet.Value.Add(new PatientInfo() {Name = "Create New..."});
                            s.Instance.NotifyPropertyChanged(nameof(s.EntitySet));
                        }
                        return  new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},
                            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    }),

               
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IPatientSummaryListViewModel, ILoadEntityViewSetWithChanges<IPatientInfo,IPartialMatch>>(
                    key:"Search",
                    commandPredicate:new List<Func<IPatientSummaryListViewModel, bool>>
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

                new ViewEventCommand<IPatientSummaryListViewModel, IViewRowStateChanged<IPatientInfo>>(
                    key:"EditEntity",
                    commandPredicate:new List<Func<IPatientSummaryListViewModel, bool>>
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

                new ViewEventCommand<IPatientSummaryListViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key:"SavePatientInfo",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IPatientSummaryListViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count == 1
                             && v.ChangeTracking.First().Value != null
                             && v.CurrentEntity.Value.Id != 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                ((dynamic)s).Id,
                                "Patient",
                                "General",
                                "Personal Information",
                                s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        return msg;
                    }),

                new ViewEventCommand<IPatientSummaryListViewModel, IUpdatePatientEntityWithChanges<IPatients>>(
                    key:"CreatePatientInfo",
                    subject:v => v.ChangeTracking.DictionaryChanges,
                    commandPredicate: new List<Func<IPatientSummaryListViewModel, bool>>
                    {
                        v => v.ChangeTracking.Count == 4
                             && v.CurrentEntity.Value.Id == 0
                    },
                    //TODO: Make a type to capture this info... i killing it here
                    messageData: s =>
                    {
                        var msg = new ViewEventCommandParameter(
                            new object[]
                            {
                                ((dynamic)s).Id,
                                "Patient",
                                "General",
                                "Personal Information",
                                s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                            },
                            new StateCommandInfo(s.Process.Id, Context.EntityView.Commands.GetEntityView), s.Process,
                            s.Source);
                        s.ChangeTracking.Clear();
                        return msg;
                    }),

            },
            typeof(IPatientSummaryListViewModel),
            typeof(IBodyViewModel), 0);


    }
}