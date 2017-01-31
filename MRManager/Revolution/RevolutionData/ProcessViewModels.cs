using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using SystemInterfaces;
using EventMessages;
using EventMessages.Commands;
using EventMessages.Events;
using Interfaces;
using JB.Reactive.ExtensionMethods;
using MoreLinq;
using Reactive.Bindings;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewMessages;
using ViewModel.Interfaces;
using Utilities;

namespace RevolutionData
{
    public class ProcessViewModels
    {
        public static readonly List<IViewModelInfo> ProcessViewModelInfos = new List<IViewModelInfo>
        {
            MainWindowViewModelInfo.MainWindowViewModel,
            ScreenViewModelInfo.ScreenViewModel,
            SigninViewModelInfo.SigninViewModel,
            PatientSummaryListViewModelInfo.PatientSummaryListViewModel,
            PatientDetailsViewModelInfo.PatientDetailsViewModel,
            InterviewListViewModelInfo.InterviewListViewModel,
            HeaderViewModelInfo.HeaderViewModel,
            QuestionListViewModelInfo.QuestionListViewModel,
            QuestionaireViewModelInfo.QuestionairenaireViewViewModel,
            PatientVisitViewModelInfo.PatientVisitViewModel,
        };
    }


}
