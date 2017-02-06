using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    

    public class LoadEntityViewSetWithFilter<T> : ProcessSystemMessage
    {
        public LoadEntityViewSetWithFilter(Expression<Func<T, bool>> filter, Expression func, Type viewType, Type viewDbType, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
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
