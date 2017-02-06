using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IMachineInfo
    {

        string MachineName { get; }
        int Processors { get; }
    }
}
