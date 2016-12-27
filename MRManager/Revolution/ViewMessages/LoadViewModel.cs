using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    [Export]
    public class LoadViewModel : ProcessSystemMessage
    {
        public LoadViewModel(IViewModelInfo viewModelInfo, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            ViewModelInfo = viewModelInfo;

        }

        public IViewModelInfo ViewModelInfo { get;}
    }

    [Export]
    public class UnloadViewModel : ProcessSystemMessage
    {
        public UnloadViewModel(IViewModelInfo viewModelInfo, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            ViewModelInfo = viewModelInfo;

        }

        public IViewModelInfo ViewModelInfo { get; }
    }
}