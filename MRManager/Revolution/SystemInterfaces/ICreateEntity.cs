using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ICreateEntity<out TEntity>:IProcessSystemMessage where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}
