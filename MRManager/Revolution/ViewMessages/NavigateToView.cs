using CommonMessages;
using System.ComponentModel.Composition;
using SystemInterfaces;
using ViewModel.Interfaces;

namespace ViewMessages
{


    [Export(typeof(INavigateToView))]
    public class NavigateToView: ProcessSystemMessage, INavigateToView
    {
        public NavigateToView() { }
        public NavigateToView(string view, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            View = view;
        }

        public string View { get; }

    }
}
