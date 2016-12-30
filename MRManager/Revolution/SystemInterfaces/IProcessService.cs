using System.ComponentModel.Composition;

namespace StartUp.Messages
{
    [InheritedExport]
    public interface IProcessService : IService<IProcessService>
    {

    }
}