using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    public class ViewStateLoaded<TViewModel,TProcessState>: ProcessSystemMessage, IViewStateLoaded<TViewModel,TProcessState> where TProcessState : IProcessState where TViewModel : IViewModel
    {
        public ViewStateLoaded(TViewModel viewModel, TProcessState state, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            ViewModel = viewModel;
            State = state;
        }

        public TViewModel ViewModel { get; }
        public TProcessState State { get; }
    }
}