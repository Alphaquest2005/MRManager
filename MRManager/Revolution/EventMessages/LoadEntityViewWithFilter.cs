using System;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    public class LoadEntityViewWithFilter<T> : SystemProcessMessage
    {
        public LoadEntityViewWithFilter(Expression<Func<T, bool>> filter, Expression func, Type viewType, Type viewDbType, ISystemProcess process, MessageSource source) : base(process, source)
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
