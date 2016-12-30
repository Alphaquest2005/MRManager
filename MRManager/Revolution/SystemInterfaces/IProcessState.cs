namespace SystemInterfaces
{
    public interface IProcessState
    {
        int ProcessId { get; }
        IProcessStateDetailedInfo StateInfo { get; }
        
    }

    public interface IProcessStateDetailedInfo
    {
        string Status { get; }
        string Notes { get; }
    }

    public interface IProcessState<out TEntity>: IProcessState where TEntity:IEntity
    {
       TEntity Entity { get; }
    }
}