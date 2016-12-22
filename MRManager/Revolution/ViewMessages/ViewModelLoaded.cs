using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModelInterfaces;

namespace ViewMessages
{
    [Export]
    public class ViewModelCreated<TViewModel> : SystemProcessMessage, IViewModelEvent<TViewModel>
    {
        [ImportingConstructor]
        public ViewModelCreated(TViewModel viewModel, ISystemProcess process, MessageSource source) : base(process,source)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }

    }

    [Export]
    public class ViewLoadedViewModel<TViewModel> : SystemProcessMessage, IViewModelEvent<TViewModel>
    {
        //occurs when viewmodel loaded in View
        [ImportingConstructor]
        public ViewLoadedViewModel(TViewModel viewModel, ISystemProcess process, MessageSource source) : base(process, source)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }

    }

    [Export]
    public class ViewModelUnloaded<TViewModel> : SystemProcessMessage, IViewModelEvent<TViewModel>
    {
        [ImportingConstructor]
        public ViewModelUnloaded(TViewModel viewModel, ISystemProcess process, MessageSource source) : base(process,source)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }
    }
}
