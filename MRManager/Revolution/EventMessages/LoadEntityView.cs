using System;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    public class LoadEntityView<T> : SystemProcessMessage
    {
        public LoadEntityView(Expression func, Type viewType, ISystemProcess process, MessageSource source) : base(process, source)
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
