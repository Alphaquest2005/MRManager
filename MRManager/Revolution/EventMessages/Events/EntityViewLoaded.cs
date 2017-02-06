using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IEntityViewLoaded<>))]
    public class EntityViewLoaded<TView> : ProcessSystemMessage, IEntityViewLoaded<TView> where TView : IEntityView
    {
        public EntityViewLoaded() { }
        public IEnumerable<TView> Entities { get; }
        

        public EntityViewLoaded(IEnumerable<TView> entities, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Entities = entities;
        }
    }
}
