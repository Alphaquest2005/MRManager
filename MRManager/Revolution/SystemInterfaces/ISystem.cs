using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface ISystem
    {
        IMachineInfo MachineInfo { get; }
    }
}