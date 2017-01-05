using System;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    public class LoadEntityView<TEntityView> : ProcessSystemMessage //where TEntityView:IEntityView<IEntity>
    {
        public LoadEntityView(Expression func, Type viewType, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
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
