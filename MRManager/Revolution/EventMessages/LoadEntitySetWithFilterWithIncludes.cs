using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadEntitySetWithFilterWithIncludes<T> : ProcessSystemMessage where T : IEntity
    {
        public List<Expression<Func<T, bool>>> Filter { get; }
        public List<Expression<Func<T,dynamic>>> Includes { get; }
        
        public LoadEntitySetWithFilterWithIncludes(List<Expression<Func<T,bool>>> filter, List<Expression<Func<T, dynamic>>> includes, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            Contract.Requires(filter != null);
            Contract.Requires(includes != null);
            Filter = filter;
            Includes = includes;
           
        }
    }
}
