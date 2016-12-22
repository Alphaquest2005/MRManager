using System.ComponentModel.Composition;

namespace ViewModelInterfaces
{
    [InheritedExport]
    public interface IViewModelLocator
    {
        void Intialize();

    }
}
