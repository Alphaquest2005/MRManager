using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ILoadEntitySetWithFilter<T> : IProcessSystemMessage where T : IEntity
    {
        List<Expression<Func<T, bool>>> Filter { get; }
    }
}
