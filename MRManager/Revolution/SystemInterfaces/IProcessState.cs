using DataInterfaces;

namespace SystemInterfaces
{
    public interface IProcessState
    {
        int ProcessId { get; }
        string Status { get; }
    }

    public interface IProcessState<out TEntity>: IProcessState where TEntity:IEntity
    {
       TEntity Entity { get; }
    }
}