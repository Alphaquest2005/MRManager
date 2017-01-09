using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewEventPublicationParameter :IViewEventPublicationParameter
    {
        public ViewEventPublicationParameter(object[] @params, ISystemProcess process, ISource source)
        {
            Params = @params;
            Process = process;
            Source = source;
        }

        public object[] Params { get; }
        public ISystemProcess Process { get; }
        public ISource Source { get; }
    }
}