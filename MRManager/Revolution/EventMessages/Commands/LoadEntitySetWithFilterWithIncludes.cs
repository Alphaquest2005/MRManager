using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(ILoadEntitySetWithFilterWithIncludes<>))]


    public class LoadEntitySetWithFilterWithIncludes<T> : ProcessSystemMessage, ILoadEntitySetWithFilterWithIncludes<T> where T : IEntity
    {
        public LoadEntitySetWithFilterWithIncludes() { }
        public List<Expression<Func<T, bool>>> Filter { get; }
        public List<Expression<Func<T,dynamic>>> Includes { get; }
        
        public LoadEntitySetWithFilterWithIncludes(List<Expression<Func<T,bool>>> filter, List<Expression<Func<T, dynamic>>> includes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(filter != null);
            Contract.Requires(includes != null);
            Filter = filter;
            Includes = includes;
           
        }
        public Type ViewType => typeof(T);
    }
}
