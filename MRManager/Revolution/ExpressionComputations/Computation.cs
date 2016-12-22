using System;
using System.Linq.Expressions;

namespace ExpressionComputations
{

    public abstract class Computation<TItem, TValue>
    {
        public abstract Expression<Func<ComputedInput<TSource, TItem>, ComputedOutput<TSource, TValue>>> GetComputation<TSource>();

        public TValue GetValue(TItem item)
        {
            return GetComputation<object>()
                .Compile()
                .Invoke(new ComputedInput<object, TItem>
                {
                    Item = item
                })
                .Value;
        }
    }
}
