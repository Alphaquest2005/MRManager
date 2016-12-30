using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessSystemMessage : ISystemMessage, IProcess
    {
        ISystemProcess Process { get; }
    }
}