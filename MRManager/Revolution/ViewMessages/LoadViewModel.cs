using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    [Export]
    public class LoadViewModel : ProcessSystemMessage
    {
        public LoadViewModel(IViewModelInfo viewModelInfo, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            ViewModelInfo = viewModelInfo;

        }

        public IViewModelInfo ViewModelInfo { get;}
    }

    [Export]
    public class UnloadViewModel : ProcessSystemMessage
    {
        public UnloadViewModel(IViewModelInfo viewModelInfo, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            ViewModelInfo = viewModelInfo;

        }

        public IViewModelInfo ViewModelInfo { get; }
    }
}