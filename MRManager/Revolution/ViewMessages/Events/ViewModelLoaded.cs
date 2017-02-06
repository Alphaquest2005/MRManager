using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewMessages
{
    [Export(typeof(IViewModelLoaded<, >))]
    public class ViewModelLoaded<TLoadingViewModel,TViewModel> : ProcessSystemMessage, IViewModelLoaded<TLoadingViewModel,TViewModel>
    {
        //occurs when viewmodel loaded in View
        public ViewModelLoaded() { }
        public ViewModelLoaded(TLoadingViewModel loadingViewModel, TViewModel viewModel, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            LoadingViewModel = loadingViewModel;
            ViewModel = viewModel;
        }

        public TLoadingViewModel LoadingViewModel { get; }
        public TViewModel ViewModel { get; }

    }
}
