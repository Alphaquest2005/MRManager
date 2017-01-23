using System.ComponentModel.Composition;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IUnloadViewModel : IProcessSystemMessage
    {
        IViewModelInfo ViewModelInfo { get; }
    }
}