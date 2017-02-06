using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    [Export(typeof(IViewModelCreated<>))]
    public class ViewModelCreated<TViewModel> : ProcessSystemMessage, IViewModelCreated<TViewModel>
    {
        public ViewModelCreated() { }
        [ImportingConstructor]
        public ViewModelCreated(TViewModel viewModel, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }

    }
}