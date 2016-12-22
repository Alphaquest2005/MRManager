using System.Collections.Generic;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntitySetWithFilterLoaded<T> : BaseMessage where T : IEntity
    {
        public IList<T> Entities { get; }
        

        public EntitySetWithFilterLoaded(IList<T> entities, MessageSource source) : base(source)
        {
            Entities = entities;
        }
    }
}
