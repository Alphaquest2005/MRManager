using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ISystem
    {
        IMachineInfo MachineInfo { get; }
    }
}