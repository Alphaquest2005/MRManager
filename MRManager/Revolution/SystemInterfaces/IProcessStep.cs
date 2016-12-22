namespace SystemInterfaces
{
    public interface IProcess
    {
        int Id { get;}
        int ProcessId { get; }
        string Name { get;}
        string Description { get;}
        string Symbol { get;}
        
    }

    public interface ISystemProcess : IProcess
    {
        IMachineInfo MachineInfo { get; }
        IUser User { get; }
    }
}
