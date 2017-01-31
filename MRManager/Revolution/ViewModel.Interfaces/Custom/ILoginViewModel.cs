using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ViewModelInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface ISigninViewModel: IEntityViewModel<ISignInInfo>
    {
    }

    [InheritedExport]
    public interface IPatientSummaryListViewModel : IEntityListViewModel<IPatientInfo>
    {
         string Field { get; set; }
         string Value { get; set; }
    }

    [InheritedExport]
    public interface IPatientDetailsViewModel : IEntityViewModel<IPatientDetailsInfo>
    {

    }

    [InheritedExport]
    public interface IInterviewListViewModel : IEntityListViewModel<IInterviewInfo>
    {
        string Field { get; set; }
        string Value { get; set; }
        ReactiveProperty<IPatientSyntomInfo> CurrentPatientSyntom { get;  }
        ReactiveProperty<ISyntomMedicalSystemInfo> CurrentMedicalSystem { get; }
    }

    [InheritedExport]
    public interface IQuestionaireViewModel : IEntityListViewModel<IQuestionResponseOptionInfo>
    {
        ObservableBindingList<IResponseOptionInfo> ChangeTrackingList { get; }
        IPatientInfo CurrentPatient { get; set; }
    }

    [InheritedExport]
    public interface IQuestionListViewModel : IEntityListViewModel<IQuestionInfo>
    {
        ObservableBindingList<IQuestionInfo> ChangeTrackingList { get; }
        IInterviewInfo CurrentInterview { get; set; }
    }

    [InheritedExport]
    public interface IPatientVisitViewModel : IEntityListViewModel<IPatientVisitInfo>
    {
        IPatientInfo CurrentPatient { get; set; }
    }

    [InheritedExport]
    public interface IPatientSyntomViewModel : IEntityListViewModel<IPatientSyntomInfo>
    {
        IPatientVisitInfo CurrentPatientVisit { get; set; }
    }
}
