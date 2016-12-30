using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessState
    {
        int ProcessId { get; }
        IProcessStateDetailedInfo StateInfo { get; }
        
    }

    [InheritedExport]
    public interface IProcessState<out TEntity>: IProcessState where TEntity:IEntity
    {
       TEntity Entity { get; }
    }
}