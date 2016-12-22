using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadEntitySetWithFilterWithIncludes<T> : BaseMessage where T : IEntity
    {
        public List<Expression<Func<T, bool>>> Filter { get; }
        public List<Expression<Func<T,dynamic>>> Includes { get; }
        
        public LoadEntitySetWithFilterWithIncludes(List<Expression<Func<T,bool>>> filter, List<Expression<Func<T, dynamic>>> includes, MessageSource source) : base(source)
        {
            Contract.Requires(filter != null);
            Contract.Requires(includes != null);
            Filter = filter;
            Includes = includes;
           
        }
    }
}
