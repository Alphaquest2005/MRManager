using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessStateMessage<out TEntity>:IProcessSystemMessage where TEntity : IEntityId
    {
        IProcessState<TEntity> State { get; }
    }
}
