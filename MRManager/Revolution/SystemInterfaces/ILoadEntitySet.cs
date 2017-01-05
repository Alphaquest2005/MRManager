using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ILoadEntitySet<TEntity> : IProcessSystemMessage where TEntity:IEntity
    {
    }
}
