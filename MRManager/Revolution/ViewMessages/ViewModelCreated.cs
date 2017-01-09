using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    [Export]
    public class ViewModelCreated<TViewModel> : ProcessSystemMessage, IViewModelCreated<TViewModel>
    {
        [ImportingConstructor]
        public ViewModelCreated(TViewModel viewModel, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }

    }
}