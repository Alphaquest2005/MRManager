using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IViewModelService : IService<IViewModelService>
    {

    }
}