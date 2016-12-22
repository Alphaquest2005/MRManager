using System.ComponentModel.Composition;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface IViewModelEvent<T> 
    {
        T ViewModel { get; }
    }
}