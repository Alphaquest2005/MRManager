using System;
using SystemInterfaces;

namespace CommonMessages
{
    public interface IMessage: IEvent
    {
       DateTime MessageDateTime { get; }
    }

    public interface ISystemMessage : IMessage, ISystem
    {
        
        
    }

}