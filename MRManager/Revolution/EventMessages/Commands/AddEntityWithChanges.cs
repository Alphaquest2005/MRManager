using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IAddOrGetEntityWithChanges<>))]

    public class AddOrGetEntityWithChanges<TEntity> : ProcessSystemMessage, IAddOrGetEntityWithChanges<TEntity> where TEntity : IEntity
    {
        public AddOrGetEntityWithChanges() { }
        public Dictionary<string, dynamic> Changes { get; }
        
        public AddOrGetEntityWithChanges(Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Contract.Requires(changes.Count > 0);
            Changes = changes;
        }
        public Type ViewType => typeof(TEntity);
    }
}