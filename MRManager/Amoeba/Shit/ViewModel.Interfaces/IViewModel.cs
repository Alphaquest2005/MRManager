using System.ComponentModel.Composition;
using DataInterfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface IViewModel<T> where T:IEntity
    {
       
    }
}
