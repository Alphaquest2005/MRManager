using SystemInterfaces;

namespace CommonMessages
{
    public interface IProcessSystemMessage:  ISystemMessage, IProcess
    {
        ISystemProcess Process { get; }
    }
}