using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    [Export(typeof(IViewStateLoaded<,>))]
    public class ViewStateLoaded<TViewModel,TProcessState>: ProcessSystemMessage, IViewStateLoaded<TViewModel,TProcessState> where TProcessState : IProcessState where TViewModel : IViewModel
    {
        public ViewStateLoaded() { }
        public ViewStateLoaded(TViewModel viewModel, TProcessState state, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            ViewModel = viewModel;
            State = state;
        }

        public TViewModel ViewModel { get; }
        public TProcessState State { get; }
    }
}