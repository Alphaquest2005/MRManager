using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessState
    {
        int ProcessId { get; }
        IProcessStateInfo StateInfo { get; }
        
    }

    [InheritedExport]
    public interface IProcessState<out TEntity>: IProcessState where TEntity: IEntityId
    {
       TEntity Entity { get; }
    }
}