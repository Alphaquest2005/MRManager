using System;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionComputations
{
    public static class ComputationExtensions
    {
        public static IQueryable<TResult> Compute<TSource, TItem, TValue, TResult>(
            this IQueryable<TSource> source,
            Computation<TItem, TValue> computation,
            Expression<Func<TSource, ComputedInput<TSource, TItem>>> itemSelector,
            Expression<Func<ComputedOutput<TSource, TValue>, TResult>> resultSelector)
        {
            Expression<Func<ComputedInput<TSource, TItem>, ComputedOutput<TSource, TValue>>> computationExpression
                = computation.GetComputation<TSource>();
            return source
                .Select(itemSelector)
                .Select(computationExpression)
                .Select(resultSelector);
        }

        public static IQueryable<TResult> Compute<TItem, TValue, TResult>(
            this IQueryable<TItem> source,
            Computation<TItem, TValue> computation,
            Expression<Func<ComputedOutput<TItem, TValue>, TResult>> resultSelector)
        {
            return source.Compute(computation,
                x => new ComputedInput<TItem, TItem>
                {
                    Source = x,
                    Item = x
                },
                resultSelector);
        }

        public static IQueryable<ComputedOutput<TItem, TValue>> Compute<TItem, TValue>(
            this IQueryable<TItem> source,
            Computation<TItem, TValue> computation)
        {
            return source.Compute(computation, x => x);
        }
    }
}