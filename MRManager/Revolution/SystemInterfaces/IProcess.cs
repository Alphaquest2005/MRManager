namespace SystemInterfaces
{
    public interface IProcess
    {
        int Id { get; }
        int ParentProcessId { get; }
        string Name { get; }
        string Description { get; }
        string Symbol { get; }
        IUser User { get; }

    }
}
