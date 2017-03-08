using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(ILoadEntitySetWithFilter<>))]


    public class LoadEntitySetWithFilter<T> : ProcessSystemMessage, ILoadEntitySetWithFilter<T> where T : IEntity
    {
        public LoadEntitySetWithFilter() { }
        public List<Expression<Func<T, bool>>> Filter { get; }
      
      
        public LoadEntitySetWithFilter(List<Expression<Func<T,bool>>> filter, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(filter != null);
            Filter = filter;
        }
        public Type ViewType => typeof(T);
    }
}
