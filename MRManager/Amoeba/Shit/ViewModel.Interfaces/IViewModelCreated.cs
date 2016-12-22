using System.ComponentModel.Composition;
using DataInterfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface IViewModelCreated<T> where T: IEntity
    {
        IViewModel<T> ViewModel { get; }
    }
}