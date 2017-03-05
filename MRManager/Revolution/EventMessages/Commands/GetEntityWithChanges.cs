using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IGetEntityWithChanges<>))]

    public class GetEntityWithChanges<TEntity> : ProcessSystemMessage, IGetEntityWithChanges<TEntity> where TEntity : IEntity
    {
        public GetEntityWithChanges() { }
        public GetEntityWithChanges( Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Changes = changes;
            
        }

        public Dictionary<string, dynamic> Changes { get; }
        public Type ViewType => typeof(TEntity);
    }
}