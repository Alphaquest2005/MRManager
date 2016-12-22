using System;
using System.Linq.Expressions;

namespace Utilities
{
    public sealed class BoxingSafeConverter<TIn, TOut>
    {
        public static readonly BoxingSafeConverter<TIn, TOut> Instance = new BoxingSafeConverter<TIn, TOut>();
        private readonly Func<TIn, TOut> convert;

        public Func<TIn, TOut> Convert => convert;

        private BoxingSafeConverter()
        {
            if (typeof(TIn) != typeof(TOut))
            {
                throw new InvalidOperationException("Both generic type parameters must represent the same type.");
            }
            var paramExpr = Expression.Parameter(typeof(TIn));
            convert =
                Expression.Lambda<Func<TIn, TOut>>(paramExpr, // this conversion is legal as typeof(TIn) = typeof(TOut)
                    paramExpr)
                    .Compile();
        }
    }
}
