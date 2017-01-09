using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{

    public class EntityViewLoaded<TView> : ProcessSystemMessage, IEntityViewLoaded<TView> where TView : IEntityView
    {
        public IEnumerable<TView> Entities { get; }
        

        public EntityViewLoaded(IEnumerable<TView> entities, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Entities = entities;
        }
    }
}
