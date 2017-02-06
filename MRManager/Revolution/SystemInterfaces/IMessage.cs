using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IMessage : IEvent
    {
        DateTime MessageDateTime { get; }
    }
}
