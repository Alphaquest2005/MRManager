using System.ComponentModel.Composition;
using SystemInterfaces;
using DataInterfaces;

namespace ViewModelInterfaces
{

    [InheritedExport]
    public interface ICreateSummaryViewModel<T> where T:IEntity
    {
    }
}