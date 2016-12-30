using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IMachineInfo
    {

        string MachineName { get; }
        int Processors { get; }
    }
}
