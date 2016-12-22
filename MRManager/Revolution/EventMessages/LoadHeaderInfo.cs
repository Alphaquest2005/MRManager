using System;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadHeaderInfo<T> : SystemProcessMessage where T:class, IEntity
    {
        public LoadHeaderInfo( Expression<Func<T,IHeaderInfo<T>>> selectExpression, ISystemProcess process, MessageSource source) : base(process, source)
        {
            SelectExpression = selectExpression;
        }

     
        public Expression<Func<T, IHeaderInfo<T>>> SelectExpression { get; }
    }
}
