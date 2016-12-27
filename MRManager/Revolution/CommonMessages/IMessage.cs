using System;
using SystemInterfaces;

namespace CommonMessages
{
    public interface IMessage: IEvent
    {
       DateTime MessageDateTime { get; }
    }
}