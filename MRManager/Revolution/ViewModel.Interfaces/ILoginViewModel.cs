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
}
