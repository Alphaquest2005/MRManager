using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Utilities
{
    public static class FuncExtensions
    {
         /// <summary>
    ///     Converts <see cref="Func{object, object}" /> to <see cref="Func{T, TResult}" />.
    /// </summary>
    public static Delegate Convert<T1,T2,R>(this Func<T1, T2,R> func, Type argType1, Type argType2, Type resultType)
    {
        // If we need more versions of func then consider using params Type as we can abstract some of the
        // conversion then.

        Contract.Requires(func != null);
        Contract.Requires(resultType != null);

        var param = Expression.Parameter(argType1);
        var param2 = Expression.Parameter(argType2);
        var convertedParam = new Expression[] { Expression.Convert(param, typeof(T1)), Expression.Convert(param2, typeof(T2))};

        
        

            // This is gnarly... If a func contains a closure, then even though its static, its first
            // param is used to carry the closure, so its as if it is not a static method, so we need
            // to check for that param and call the func with it if it has one...
            Expression call;
        call = Expression.Convert(
            func.Target == null
            ? Expression.Call(func.Method, convertedParam)
            : Expression.Call(Expression.Constant(func.Target), func.Method, convertedParam), resultType);

        var delegateType = typeof(Func<,,>).MakeGenericType(argType1,argType2, resultType);
        return Expression.Lambda(delegateType, call, param, param2).Compile();
    }

        public static Delegate Convert<T1, R>(this Func<T1, R> func, Type argType1, Type resultType)
        {
            // If we need more versions of func then consider using params Type as we can abstract some of the
            // conversion then.

            Contract.Requires(func != null);
            Contract.Requires(resultType != null);

            var param = Expression.Parameter(argType1);
           var convertedParam = new Expression[] { Expression.Convert(param, typeof(T1))};




            // This is gnarly... If a func contains a closure, then even though its static, its first
            // param is used to carry the closure, so its as if it is not a static method, so we need
            // to check for that param and call the func with it if it has one...
            Expression call;
            call = Expression.Convert(
                func.Target == null
                ? Expression.Call(func.Method, convertedParam)
                : Expression.Call(Expression.Constant(func.Target), func.Method, convertedParam), resultType);

            var delegateType = typeof(Func<,>).MakeGenericType(argType1, resultType);
            return Expression.Lambda(delegateType, call, param).Compile();
        }

        public static Delegate Convert<T1,T2>(this Action<T1,T2> action, Type argType1, Type argType2)
        {
            // If we need more versions of func then consider using params Type as we can abstract some of the
            // conversion then.

            Contract.Requires(action != null);
            
            var param = Expression.Parameter(argType1);
            var param2 = Expression.Parameter(argType2);
            var convertedParam = new Expression[] { Expression.Convert(param, typeof(T1)), Expression.Convert(param2, typeof(T2)) };




            // This is gnarly... If a func contains a closure, then even though its static, its first
            // param is used to carry the closure, so its as if it is not a static method, so we need
            // to check for that param and call the func with it if it has one...
            Expression call;
            call = Expression.Convert(
                action.Target == null
                ? Expression.Call(action.Method, convertedParam)
                : Expression.Call(Expression.Constant(action.Target), action.Method, convertedParam),typeof(void));

            var delegateType = typeof(Action<,>).MakeGenericType(argType1, argType2);
            return Expression.Lambda(delegateType, call, param, param2).Compile();
        }
    }
}
