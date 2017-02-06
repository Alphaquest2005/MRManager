using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    
    public interface IProcessStateMessage<out TEntity>:IProcessSystemMessage where TEntity : IEntityId
    {
        IProcessState<TEntity> State { get; }
    }

    
    public interface IUpdateProcessStateList<out TEntity> : IProcessSystemMessage where TEntity : IEntityId
    {
        IProcessStateList<TEntity> State { get; }
    }
}
