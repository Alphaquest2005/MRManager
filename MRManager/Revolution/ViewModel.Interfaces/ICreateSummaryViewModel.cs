using System.ComponentModel.Composition;
using DataInterfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface ICreateSummaryViewModel<T> where T:IEntity
    {
    }
}