using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IMessageSource
    {
        string Source { get; }
    }
}