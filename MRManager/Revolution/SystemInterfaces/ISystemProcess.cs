using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ISystemProcess : ISystem,IProcess
    {
        
    }
}