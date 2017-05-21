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
    public class MedicalReportViewModelInfo
    {
        public static readonly ViewModelInfo MedicalReportViewModel = new ViewModelInfo
            (
            3,
            new ViewInfo("MedicalReport", "", "Medical Report"),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {
                new ViewEventSubscription<IMedicalReportViewModel, IUpdateProcessStateList<IQuestionInfo>>(
                    3,
                    e => e != null,
                    new List<Func<IMedicalReportViewModel, IUpdateProcessStateList<IQuestionInfo>, bool>>(),
                    (v,e) => 
                    {
                        if (v.State.Value == e.State) return;
                        v.State.Value = e.State;
                    }),

               

                    },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IMedicalReportViewModel, IViewStateLoaded<IMedicalReportViewModel,IProcessStateList<IQuestionInfo>>>(
                    key:"IPatientInfoViewStateLoaded",
                    subject:v => v.State,
                    subjectPredicate:new List<Func<IMedicalReportViewModel, bool>>
                    {
                        v => v.State != null
                    },
                    messageData:s =>
                    {
                         
                        return new ViewEventPublicationParameter(new object[] {s, s.State.Value},
                            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    }),

                new ViewEventPublication<IMedicalReportViewModel, ICurrentEntityChanged<IQuestionInfo>>(
                    key:"CurrentEntityChanged",
                    subject:v => v.CurrentEntity,//.WhenAnyValue(x => x.Value),
                    subjectPredicate:new List<Func<IMedicalReportViewModel, bool>>{},
                    messageData:s =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                         {
                              s.NotifyPropertyChanged(nameof(s.EntitySet));
                         }));
                        return new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value},
                            new StateEventInfo(s.Process.Id, Context.View.Events.ProcessStateLoaded), s.Process,
                            s.Source);
                    })
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

    }
}