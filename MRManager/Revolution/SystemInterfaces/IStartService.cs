using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IStartService<TService> where TService:IService<TService>
    {
    }
}