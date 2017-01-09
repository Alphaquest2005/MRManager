using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewMessages
{
    [Export]
    public class ViewModelLoaded<TLoadingViewModel,TViewModel> : ProcessSystemMessage, IViewModelLoaded<TLoadingViewModel,TViewModel>
    {
        //occurs when viewmodel loaded in View
        [ImportingConstructor]
        public ViewModelLoaded(TLoadingViewModel loadingViewModel, TViewModel viewModel, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            LoadingViewModel = loadingViewModel;
            ViewModel = viewModel;
        }

        public TLoadingViewModel LoadingViewModel { get; }
        public TViewModel ViewModel { get; }

    }

    [Export]
    public class ViewModelUnloaded<TViewModel> : ProcessSystemMessage, IViewModelEvent<TViewModel>
    {
        [ImportingConstructor]
        public ViewModelUnloaded(TViewModel viewModel, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }
    }
}
