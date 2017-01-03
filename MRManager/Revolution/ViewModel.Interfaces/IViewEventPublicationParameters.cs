using System.ComponentModel.Composition;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IViewEventPublicationParameter
    {
        object[] Params { get; }
        ISystemProcess Process { get; }
        ISourceMessage SourceMessage { get; }
    }

  
}