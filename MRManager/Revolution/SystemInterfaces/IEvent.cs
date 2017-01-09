using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEvent
    {
        //ToDo:Check to remove this
        ISystemSource Source { get; }
    }
}