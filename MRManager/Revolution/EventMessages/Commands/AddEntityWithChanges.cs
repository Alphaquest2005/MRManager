using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IAddEntityWithChanges<>))]

    public class AddEntityWithChanges<TEntity> : ProcessSystemMessage, IAddEntityWithChanges<TEntity> where TEntity : IEntity
    {
        public AddEntityWithChanges() { }
        public Dictionary<string, dynamic> Changes { get; }
        
        public AddEntityWithChanges(Dictionary<string, dynamic> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Contract.Requires(changes.Count > 0);
            Changes = changes;

        }
    }
}