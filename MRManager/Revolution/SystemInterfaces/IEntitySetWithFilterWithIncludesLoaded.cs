using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    
    public interface IEntitySetWithFilterWithIncludesLoaded<T> : IProcessSystemMessage where T : IEntity
    {
        IList<T> Entities { get; }
        IList<Expression<Func<T, object>>> Includes { get; }
    }
}
