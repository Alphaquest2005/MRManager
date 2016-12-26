using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadEntitySetWithFilter<T> : ProcessSystemMessage where T : IEntity
    {
        public List<Expression<Func<T, bool>>> Filter { get; }
      
      
        public LoadEntitySetWithFilter(List<Expression<Func<T,bool>>> filter, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            Contract.Requires(filter != null);
            Filter = filter;
        }
    }
}
