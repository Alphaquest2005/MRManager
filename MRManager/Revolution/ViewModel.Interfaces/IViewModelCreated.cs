using System.ComponentModel.Composition;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    
    public interface IViewModelCreated<TViewModel> : IProcessSystemMessage, IViewModelEvent<TViewModel>
    {
        TViewModel ViewModel { get; }
    }
}