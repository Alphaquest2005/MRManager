using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEvent
    {
        IMessageSource Source { get; }
    }
}