using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
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
}
