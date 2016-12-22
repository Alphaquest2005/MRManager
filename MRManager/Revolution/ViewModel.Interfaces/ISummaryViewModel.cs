using System.ComponentModel.Composition;
using SystemInterfaces;
using DataInterfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface ISummaryViewModel<T>:IViewModel where T:IEntity
    {
       
    }
}
