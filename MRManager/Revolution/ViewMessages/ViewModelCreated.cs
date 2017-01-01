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
        public ViewModelCreated(TViewModel viewModel, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }

    }
}