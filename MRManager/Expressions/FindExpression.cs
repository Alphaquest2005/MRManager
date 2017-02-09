using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expressions
{
    public static class FindExpressionClass
    {
        public static Expression<Func<TEntity, TView>> FindExpression<TEntity, TView>()
        {
            var types = typeof(FindExpressionClass).Assembly.ExportedTypes
                .Where(t => t
                    .GetMethods(
                        ).Any(m => m.ReturnType == typeof(Expression<Func<TEntity, TView>>)));
            var lst = types as IList<Type> ?? types.ToList();
            if(!lst.Any()) throw new ApplicationException($"No Expression for {typeof(TEntity).Name} and {typeof(TView).Name} Found");
            return (Expression<Func<TEntity,TView >>)lst.First().GetMethods().First(x => x.ReturnType == typeof(Expression<Func<TEntity, TView>>)).Invoke(null, null);
            
        }
    }
}
