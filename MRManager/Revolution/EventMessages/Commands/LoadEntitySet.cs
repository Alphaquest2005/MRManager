using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(ILoadEntitySet<>))]


    public class LoadEntitySet<TEntity> : ProcessSystemMessage, ILoadEntitySet<TEntity> where TEntity : IEntity
    {
        public LoadEntitySet() { }
        public LoadEntitySet(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            
        }
        public Type ViewType => typeof(TEntity);
    }
}
