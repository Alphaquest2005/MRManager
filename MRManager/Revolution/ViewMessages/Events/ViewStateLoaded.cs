using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    public class ViewStateLoaded<TViewModel,TProcessState>: ProcessSystemMessage, IViewStateLoaded<TViewModel,TProcessState> where TProcessState : IProcessState where TViewModel : IViewModel
    {
        public ViewStateLoaded(TViewModel viewModel, TProcessState state, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            ViewModel = viewModel;
            State = state;
        }

        public TViewModel ViewModel { get; }
        public TProcessState State { get; }
    }


    public class ViewRowStateChanged<TEntity> : ProcessSystemMessage, IViewRowStateChanged where TEntity:IEntityId
    {
        public IViewModel ViewModel { get; }
        public RowState RowState { get; }

        public ViewRowStateChanged(IViewModel viewModel, RowState rowState, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            ViewModel = viewModel;
            RowState = rowState;
        }
    }
}