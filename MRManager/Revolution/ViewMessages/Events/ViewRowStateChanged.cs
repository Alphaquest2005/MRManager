using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    [Export(typeof(IViewRowStateChanged<>))]
    public class ViewRowStateChanged<TEntity> : ProcessSystemMessage, IViewRowStateChanged<TEntity> where TEntity:IEntityId
    {
        public IViewModel ViewModel { get; }
        public RowState RowState { get; }
        public ViewRowStateChanged() { }
        public ViewRowStateChanged(IViewModel viewModel, RowState rowState, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            ViewModel = viewModel;
            RowState = rowState;
        }
    }
}