namespace SystemInterfaces
{
    public interface IProcessSystemMessage : ISystemMessage, IProcess
    {
        ISystemProcess Process { get; }
    }
}