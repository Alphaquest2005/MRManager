using System.ComponentModel.Composition;
using SystemInterfaces;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IService<TService>: IProcessSource
    {
    }
}