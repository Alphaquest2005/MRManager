using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using Common.Dynamic;
using EF.Entities;
using EventAggregator;
using EventMessages.Commands;
using Interfaces;
using MoreLinq;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class MedicalReportViewModelInfo
    {
        public static readonly ViewModelInfo MedicalReportViewModel = new ViewModelInfo
            (
            3,
            new ViewInfo("MedicalReport", "", "Medical Report"),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                 

                 new ViewEventSubscription <IMedicalReportViewModel,IProcessStateMessage<IPatientDetailsInfo>>(
                    processId: 3,
                    eventPredicate: e => e != null,
                    actionPredicate: new List<Func<IMedicalReportViewModel, IProcessStateMessage<IPatientDetailsInfo>, bool>>(),
                    action: (v, e) =>
                    {
                        v.PatientDetails.Value = e.State.Entity;

                    }),

                 new ViewEventSubscription<IMedicalReportViewModel, IUpdateProcessStateList<IPatientVisitInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IMedicalReportViewModel, IUpdateProcessStateList<IPatientVisitInfo>, bool>>(),
                    (v,e) =>
                    {

                        v.PatientVisits.AddRangeOnScheduler(e.State.EntitySet.ToList());
                        foreach (var visit in v.PatientVisits)
                        {
                            RequestDataList<IPatientSyntomInfo>("PatientVisitId",visit.Id.ToString(), v);
                        }
                    }),

                 new ViewEventSubscription<IMedicalReportViewModel, IEntityViewSetWithChangesLoaded<IPatientSyntomInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IMedicalReportViewModel, IEntityViewSetWithChangesLoaded<IPatientSyntomInfo>, bool>>(),
                    (v, e) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            foreach (
                                var syntom in
                                    e.EntitySet.Where(synptom => v.Synptoms.All(x => x.Id != synptom.SyntomId)))
                            {
                                if (syntom.Id == 0) continue;
                                v.Synptoms.Add(new SyntomInfo()
                                {
                                    Id = syntom.SyntomId,
                                    Status = syntom.Status,
                                    Priority = syntom.Priority,
                                    SyntomName = syntom.SyntomName,
                                    MedicalSystems = new List<IMedicalSystemInfo>()
                                });
                                RequestDataList<ISyntomMedicalSystemInfo>("SyntomId", syntom.Id.ToString(), v);
                            }
                        });

                    }),

                  //new ViewEventSubscription<IMedicalReportViewModel, IEntityViewSetWithChangesLoaded<ISyntomMedicalSystemInfo>>(
                  //  3,
                  //  e => e != null,
                  //  new List<Func<IMedicalReportViewModel, IEntityViewSetWithChangesLoaded<ISyntomMedicalSystemInfo>, bool>>(),
                  //  (v,e) =>
                  //  {
                  //      foreach (var system in e.EntitySet)
                  //      {
                  //          var syntom = v.Synptoms.Value.FirstOrDefault(x => x.Id == system.SyntomId);
                  //          if (syntom == null || syntom.MedicalSystems.Any(x => x.Id == system.MedicalSystemId)) continue;

                  //          var ms = new MedicalSystemInfo()
                  //          {
                  //              Id = system.MedicalSystemId,
                  //              Name = system.System,
                  //              Interviews = system.Interviews.ToList()
                  //          };
                  //          ms.Interviews.ForEach(x => x.Questions = new List<IQuestionResponseOptionInfo>());
                  //          syntom.MedicalSystems.Add(ms);
                  //          ms.Interviews.ToList().ForEach(z => RequestDataList<IQuestionResponseOptionInfo>("InterviewId", z.Id.ToString(), v));
                            
                  //      }
                  //  }),


                  //new ViewEventSubscription<IMedicalReportViewModel, IEntityViewSetWithChangesLoaded<IQuestionResponseOptionInfo>>(
                  //  3,
                  //  e => e != null,
                  //  new List<Func<IMedicalReportViewModel, IEntityViewSetWithChangesLoaded<IQuestionResponseOptionInfo>, bool>>(),
                  //  (v,e) =>
                  //  {
                  //      var questionResponseOptionInfo = e.EntitySet.FirstOrDefault();
                  //      if (questionResponseOptionInfo != null)
                  //      {
                  //          var interviewid = questionResponseOptionInfo.InterviewId;

                  //          var interview =
                  //              v.Synptoms.Value.SelectMany(x => x.MedicalSystems.SelectMany(z => z.Interviews))
                  //                  .FirstOrDefault(x => x.Id == interviewid);
                  //          interview?.Questions.AddRange(e.EntitySet);
                           
                  //      }
                  //  }),

                    },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                //new ViewEventPublication<IMedicalReportViewModel, IViewStateLoaded<IMedicalReportViewModel,IProcessStateList<IQuestionInfo>>>(
                //    key:"IPatientInfoViewStateLoaded",
                //    subject:v => v.State,
                //    subjectPredicate:new List<Func<IMedicalReportViewModel, bool>>
                //    {
                //        v => v.State != null
                //    },
                //    messageData:s =>
                //    {
                         
                //        return new ViewEventPublicationParameter(new object[] {s, s.State.Value},
                //            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                //            s.Source);
                //    }),

                //new ViewEventPublication<IMedicalReportViewModel, ICurrentEntityChanged<IQuestionInfo>>(
                //    key:"CurrentEntityChanged",
                //    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                //    subjectPredicate:new List<Func<IMedicalReportViewModel, bool>>{},
                //    messageData:s =>
                //    {
                //        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                //         {
                //              s.NotifyPropertyChanged(nameof(s.EntitySet));
                //         }));
                //        return new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},
                //            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                //            s.Source);
                //    })
            },
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IMedicalReportViewModel, ILoadEntityViewSetWithChanges<IQuestionInfo,IPartialMatch>>(
                    key:"Search",
                    commandPredicate:new List<Func<IMedicalReportViewModel, bool>>
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
            typeof(IMedicalReportViewModel),
            typeof(IBodyViewModel),5);

        private static void RequestDataList<TView>(string key, string value, IViewModel v) where TView : IEntityView
        {
           
            var changes = new Dictionary<string, dynamic>() {{key, value}};
            var msg = new LoadEntityViewSetWithChanges<TView, IExactMatch>(changes,
                new StateCommandInfo(v.Process.Id, Context.EntityView.Commands.GetEntityView),
                v.Process, v.Source);

            EventMessageBus.Current.Publish(msg, v.Source);
        }
    }


}