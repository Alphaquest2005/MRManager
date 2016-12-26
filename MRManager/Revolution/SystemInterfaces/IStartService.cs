using System.ComponentModel.Composition;

namespace StartUp.Messages
{
    [InheritedExport]
    public interface IStartService<TService> where TService:IService<TService>
    {
    }
}