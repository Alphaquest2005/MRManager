using System;

namespace SystemInterfaces
{
    public interface IMessage : IEvent
    {
        DateTime MessageDateTime { get; }
    }
}
