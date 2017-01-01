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
        public ViewModelLoaded(TLoadingViewModel loadingViewModel, TViewModel viewModel, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
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
        public ViewModelUnloaded(TViewModel viewModel, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }
    }
}
