using System.ComponentModel.Composition;

namespace StartUp.Messages
{
    [InheritedExport]
    public interface IViewModelService : IService<IViewModelService>
    {

    }
}