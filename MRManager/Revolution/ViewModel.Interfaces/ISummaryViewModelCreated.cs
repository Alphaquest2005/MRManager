using System.ComponentModel.Composition;
using DataInterfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface ISummaryViewModelCreated<T> where T: IEntity
    {
        ISummaryViewModel<T> ViewModel { get; }
    }
}