using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
   

    public class LoadEntityView<TEntityView> : ProcessSystemMessage// where TEntityView:IEntityView<IEntity>
    {
        public LoadEntityView(Expression func, Type viewType, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
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
