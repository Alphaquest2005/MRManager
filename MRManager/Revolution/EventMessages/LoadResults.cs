using System;
using System.Linq.Expressions;
using CommonMessages;

namespace EventMessages
{
    public class LoadResults<T,TResults>
    {
        public LoadResults(Expression<Func<T, bool>> filter, Expression<Func<T, TResults>> result, MessageSource source)
        {
            Result = result;
            Filter = filter;
            Source = source;
        }

        public MessageSource Source { get; }

        public Expression<Func<T, bool>> Filter { get; }
        public Expression<Func<T, TResults>> Result { get; }
    }
}
