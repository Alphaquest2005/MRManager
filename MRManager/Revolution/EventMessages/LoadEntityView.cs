using System;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    public class LoadEntityView<T> : ProcessSystemMessage
    {
        public LoadEntityView(Expression func, Type viewType, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            
            try
            {
                Expression = func;
                ViewType = viewType;
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public Type ViewType { get; }

       public Expression Expression { get; }
       
    }
}
