using System;
using System.Linq.Expressions;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadHeaderInfo<T> : BaseMessage where T:class, IEntity
    {
        public LoadHeaderInfo( Expression<Func<T,IHeaderInfo<T>>> selectExpression, MessageSource source) : base(source)
        {
            SelectExpression = selectExpression;
        }

     
        public Expression<Func<T, IHeaderInfo<T>>> SelectExpression { get; }
    }
}
