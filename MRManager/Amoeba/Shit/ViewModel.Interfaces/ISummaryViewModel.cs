using System.ComponentModel.Composition;
using DataInterfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface ISummaryViewModel<T> where T:IEntity
    {
       
    }
}
