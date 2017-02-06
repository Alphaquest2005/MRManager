using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IEvent
    {
        //ToDo:Check to remove this
        ISystemSource Source { get; }
    }
}