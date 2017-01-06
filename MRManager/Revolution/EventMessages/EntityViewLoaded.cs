using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{

    public class EntityViewLoaded<TView> : ProcessSystemMessage, IEntityViewLoaded<TView> where TView : IEntityView
    {
        public IEnumerable<TView> Entities { get; }
        

        public EntityViewLoaded(IEnumerable<TView> entities, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Entities = entities;
        }
    }
}
