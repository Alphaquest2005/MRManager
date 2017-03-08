using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{

    [Export(typeof(ICreateEntity<>))]
    public class CreateEntity<T> : ProcessSystemMessage, ICreateEntity<T> where T : IEntity
    {
        public CreateEntity() { }
        public T Entity { get; }
        
        public CreateEntity(T entity, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }
        public Type ViewType => typeof(T);
    }
}
