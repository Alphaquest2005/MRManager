using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using ViewModelInterfaces;

namespace ViewModel.Interfaces
{
    
    public interface IViewModelLoaded<out TLoadingViewModel, TViewModel> : IProcessSystemMessage, IViewModelEvent<TViewModel>
    {
        TLoadingViewModel LoadingViewModel { get; }
        TViewModel ViewModel { get; }
    }
}
