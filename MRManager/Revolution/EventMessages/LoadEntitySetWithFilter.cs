using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
  

    public class LoadEntitySetWithFilter<T> : ProcessSystemMessage, ILoadEntitySetWithFilter<T> where T : IEntity
    {
        public List<Expression<Func<T, bool>>> Filter { get; }
      
      
        public LoadEntitySetWithFilter(List<Expression<Func<T,bool>>> filter, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Contract.Requires(filter != null);
            Filter = filter;
        }
    }
}
