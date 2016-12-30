using System.ComponentModel.Composition;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IViewModelCreated<TViewModel> : IProcessSystemMessage, IViewModelEvent<TViewModel>
    {
    }
}