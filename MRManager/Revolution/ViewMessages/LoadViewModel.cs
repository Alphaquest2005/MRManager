using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    [Export]
    public class LoadViewModel : SystemProcessMessage
    {
        public LoadViewModel(IViewModelInfo viewModelInfo, ISystemProcess process, MessageSource source) : base(process, source)
        {
            ViewModelInfo = viewModelInfo;

        }

        public IViewModelInfo ViewModelInfo { get;}
    }

    [Export]
    public class UnloadViewModel : SystemProcessMessage
    {
        public UnloadViewModel(IViewModelInfo viewModelInfo, ISystemProcess process, MessageSource source) : base(process,source)
        {
            ViewModelInfo = viewModelInfo;

        }

        public IViewModelInfo ViewModelInfo { get; }
    }
}