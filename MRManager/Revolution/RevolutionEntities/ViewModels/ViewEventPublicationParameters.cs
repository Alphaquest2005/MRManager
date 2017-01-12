using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewEventPublicationParameter :IViewEventPublicationParameter
    {
        public ViewEventPublicationParameter(object[] @params, IStateEventInfo processInfo, ISystemProcess process, ISource source)
        {
            Params = @params;
            Process = process;
            Source = source;
            ProcessInfo = processInfo;
        }

        public object[] Params { get; }
        public ISystemProcess Process { get; }
        public ISource Source { get; }
        public IStateEventInfo ProcessInfo { get; }
    }
}