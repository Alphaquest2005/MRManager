using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessSource
    {
        ISystemSource Source { get; }
    }
}