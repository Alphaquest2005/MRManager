using System.ComponentModel.Composition;
using SystemInterfaces;
using DataInterfaces;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface IViewModel<out TEntity>: IViewModel where TEntity:IEntity
    {
        
    }

    
}
