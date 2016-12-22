using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadEntitySetWithFilter<T> : BaseMessage where T : IEntity
    {
        public List<Expression<Func<T, bool>>> Filter { get; }
      
      
        public LoadEntitySetWithFilter(List<Expression<Func<T,bool>>> filter,  MessageSource source): base(source)
        {
            Contract.Requires(filter != null);
            Filter = filter;
        }
    }
}
