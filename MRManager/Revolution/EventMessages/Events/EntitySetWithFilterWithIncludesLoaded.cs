using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
  

    public class EntitySetWithFilterWithIncludesLoaded<T> : ProcessSystemMessage, IEntitySetWithFilterWithIncludesLoaded<T> where T : IEntity
    {
        public IList<T> Entities { get; }
        public IList<Expression<Func<T, dynamic>>> Includes { get; }
        //Todo: include filter just to match name
        public EntitySetWithFilterWithIncludesLoaded(IList<T> entities, IList<Expression<Func<T, dynamic>>> includes, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Entities = entities;
            Includes = includes;
            
        }
    }
}
