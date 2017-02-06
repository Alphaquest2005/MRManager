using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    
    public interface ILoadEntitySetWithFilter<TEntity> : IProcessSystemMessage, IEntityRequest<TEntity> where TEntity : IEntity
    {
        List<Expression<Func<TEntity, bool>>> Filter { get; }
    }
}
