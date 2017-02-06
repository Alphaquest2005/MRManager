using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using ViewModel.Interfaces;

namespace ViewMessages
{
    [Export(typeof(ILoadViewModel))]
    public class LoadViewModel : ProcessSystemMessage, ILoadViewModel
    {
        public LoadViewModel() { }
        public LoadViewModel(IViewModelInfo viewModelInfo, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            ViewModelInfo = viewModelInfo;

        }

        public IViewModelInfo ViewModelInfo { get;}
    }
}