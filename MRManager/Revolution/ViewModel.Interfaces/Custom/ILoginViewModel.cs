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
    
    public interface ISigninViewModel: IEntityViewModel<ISignInInfo>
    {
    }

    
    public interface IPatientSummaryListViewModel : IEntityListViewModel<IPatientInfo>
    {
         string Field { get; set; }
         string Value { get; set; }
        
    }

    
    public interface IPatientDetailsViewModel : IEntityViewModel<IPatientDetailsInfo>
    {
        IPatientInfo CurrentPatient { get; set; }
        IList<IPersonAddressInfo> Addresses { get; set; }
        IList<IPersonPhoneNumberInfo> PhoneNumbers { get; set; }
        IList<INextOfKinInfo> NextOfKins { get; set; }
        INonResidentInfo NonResidentInfo { get; set; }
    }

    
    public interface IInterviewListViewModel : IEntityListViewModel<IInterviewInfo>
    {
        string Field { get; set; }
        string Value { get; set; }
        ReactiveProperty<IPatientSyntomInfo> CurrentPatientSyntom { get;  }
        ReactiveProperty<ISyntomMedicalSystemInfo> CurrentMedicalSystem { get; }
        ReactiveProperty<ObservableList<ISyntomMedicalSystemInfo>> Systems { get; }
        ReactiveProperty<IMedicalSystems> SelectedMedicalSystem { get; }
    }

    
    public interface IQuestionaireViewModel : IEntityListViewModel<IResponseOptionInfo>
    {
        ObservableList<IQuestionResponseOptionInfo> Questions { get; set; }
        IPatientVisitInfo CurrentPatientVisit { get; set; }
        IPatientSyntomInfo CurrentPatientSyntom { get; set; }
        ReactiveProperty<IQuestionResponseOptionInfo> CurrentQuestion { get; }
        ReactiveProperty<IQuestionResponseTypes> DataType { get; }
    }

    
    public interface IQuestionListViewModel : IEntityListViewModel<IQuestionInfo>
    {
        IInterviewInfo CurrentInterview { get; set; }
    }

    
    public interface IPatientVisitViewModel : IEntityListViewModel<IPatientVisitInfo>
    {
        IPatientInfo CurrentPatient { get; set; }
    }

    
    public interface IPatientSyntomViewModel : IEntityListViewModel<IPatientSyntomInfo>
    {
        ReactiveProperty<IPatientVisitInfo> CurrentPatientVisit { get; }
    }
}
