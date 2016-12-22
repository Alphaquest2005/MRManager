using SystemInterfaces;

namespace CommonMessages
{
    public interface IMessage
    {
        MessageSource Source { get; }
        IMachineInfo MachineInfo { get; }
    }

    public interface ISystemMessage : IMessage, IEvent
    {

    }

}