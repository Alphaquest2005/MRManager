using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface ISystemSource:ISource
    {
        IMachineInfo MachineInfo { get; }
    }
}