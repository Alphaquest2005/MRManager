using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModelInterfaces;

namespace ViewMessages
{
    [Export]
    public class ViewModelCreated<TViewModel> : ProcessSystemMessage, IViewModelEvent<TViewModel>
    {
        [ImportingConstructor]
        public ViewModelCreated(TViewModel viewModel, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }

    }

    [Export]
    public class ViewLoadedViewModel<TViewModel> : ProcessSystemMessage, IViewModelEvent<TViewModel>
    {
        //occurs when viewmodel loaded in View
        [ImportingConstructor]
        public ViewLoadedViewModel(TViewModel viewModel, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }

    }

    [Export]
    public class ViewModelUnloaded<TViewModel> : ProcessSystemMessage, IViewModelEvent<TViewModel>
    {
        [ImportingConstructor]
        public ViewModelUnloaded(TViewModel viewModel, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }
    }
}
