using System.ComponentModel.Composition;
using SystemInterfaces;


namespace ViewModelInterfaces
{

    [InheritedExport]
    public interface ICreateSummaryViewModel<T> where T:IEntity
    {
    }
}