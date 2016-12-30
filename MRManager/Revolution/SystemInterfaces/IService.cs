using System.ComponentModel.Composition;

namespace StartUp.Messages
{
    [InheritedExport]
    public interface IService<TService>
    {
    }
}