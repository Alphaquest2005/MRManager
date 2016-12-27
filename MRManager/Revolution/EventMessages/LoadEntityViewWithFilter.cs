using System;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    public class LoadEntityViewWithFilter<T> : ProcessSystemMessage
    {
        public LoadEntityViewWithFilter(Expression<Func<T, bool>> filter, Expression func, Type viewType, Type viewDbType, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            
            try
            {
                Filter = filter;
                Expression = func;
                ViewType = viewType;
                ViewDbType = viewDbType;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public Type ViewType { get; }

        public Expression<Func<T, bool>> Filter { get;  }

        public Expression Expression { get; }
        public Type ViewDbType { get;  }
    }
}
