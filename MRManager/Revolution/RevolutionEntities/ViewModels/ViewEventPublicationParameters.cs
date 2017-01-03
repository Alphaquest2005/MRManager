using SystemInterfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.ViewModels
{
    public class ViewEventPublicationParameter :IViewEventPublicationParameter
    {
        public ViewEventPublicationParameter(object[] @params, ISystemProcess process, ISourceMessage sourceMessage)
        {
            Params = @params;
            Process = process;
            SourceMessage = sourceMessage;
        }

        public object[] Params { get; }
        public ISystemProcess Process { get; }
        public ISourceMessage SourceMessage { get; }
    }
}