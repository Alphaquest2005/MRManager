using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IEntityViewWithChangesFound<>))]
    public class EntityViewWithChangesFound<TView> : ProcessSystemMessage, IEntityViewWithChangesFound<TView> where TView:IEntityView
    {
        public EntityViewWithChangesFound() { }
        public EntityViewWithChangesFound(TView entity, Dictionary<string, dynamic> changes, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Entity = entity;
            Changes = changes;
        }

        public TView Entity { get; set; }
        public Dictionary<string, dynamic> Changes { get; }

    }
    //TODO: Refactor cuz all that change is the operation add type to operation
    //TODO: refactor seperate data and operation as types 

    //    eg. public class EntityViewWithChanges<TView,TOperation> : ProcessSystemMessage, IEntityViewWithChangesUpdated<TView> where TView : IEntityView
}