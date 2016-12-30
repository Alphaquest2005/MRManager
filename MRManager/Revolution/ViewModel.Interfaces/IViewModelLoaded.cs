using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using ViewModelInterfaces;

namespace ViewModel.Interfaces
{
    public interface IViewModelLoaded<out TLoadingViewModel, TViewModel> : IProcessSystemMessage, IViewModelEvent<TViewModel>
    {
        TLoadingViewModel LoadingViewModel { get; }
    }
}
