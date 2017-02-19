using System.ComponentModel.Composition;
using SystemInterfaces;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;

namespace ViewModel.Interfaces
{
    
    public interface IFooterViewModel : IViewModel
    {
        ReactiveProperty<IPatientInfo> CurrentPatient { get; }
        ReactiveProperty<IPatientVisitInfo> CurrentPatientVisit { get; }
        ReactiveProperty<IPatientSyntomInfo> CurrentPatientSyntom { get; }
        ReactiveProperty<IInterviewInfo> CurrentInterview { get; }
    }
}