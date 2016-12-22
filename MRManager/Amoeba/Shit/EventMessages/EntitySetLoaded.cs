using System.Collections.Generic;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntitySetLoaded<T> : BaseMessage where T : IEntity
    {
        public IList<T> Entities { get; }
        

        public EntitySetLoaded(IList<T> entities, MessageSource source) : base(source)
        {
            Entities = entities;
        }
    }
}
