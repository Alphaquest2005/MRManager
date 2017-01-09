using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ISystemSource:ISource
    {
        IMachineInfo MachineInfo { get; }
    }
}