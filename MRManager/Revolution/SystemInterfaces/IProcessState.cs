using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IProcessState
    {
        int ProcessId { get; }
        IProcessStateInfo StateInfo { get; }

        ISystemProcess Process { get; }

    }

    
    public interface IProcessState<out TEntity>: IProcessState where TEntity: IEntityId
    {
       TEntity Entity { get; }
    }

    
    public interface IProcessStateList<out TEntity> : IProcessState<TEntity> where TEntity : IEntityId
    {
        IEnumerable<TEntity> EntitySet { get; }
        IEnumerable<TEntity> SelectedEntities { get; }
        
    }

}