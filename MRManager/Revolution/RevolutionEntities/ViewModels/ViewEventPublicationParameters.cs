using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewEventPublicationParameter :IViewEventPublicationParameter
    {
        public ViewEventPublicationParameter(object[] @params, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source)
        {
            Params = @params;
            Process = process;
            Source = source;
            ProcessInfo = processInfo;
        }

        public object[] Params { get; }
        public ISystemProcess Process { get; }
        public ISystemSource Source { get; }
        public IStateEventInfo ProcessInfo { get; }
    }

    public class ViewEventCommandParameter : IViewEventCommandParameter
    {
        public ViewEventCommandParameter(object[] @params, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source)
        {
            Params = @params;
            Process = process;
            Source = source;
            ProcessInfo = processInfo;
        }

        public object[] Params { get; }
        public ISystemProcess Process { get; }
        public ISystemSource Source { get; }
        public IStateCommandInfo ProcessInfo { get; }
    }
}