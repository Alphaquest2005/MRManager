using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    public interface IEntityUpdated<out TEntity> : IProcessSystemMessage where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}
