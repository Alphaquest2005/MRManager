using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{

    public class CurrentEntityChanged<TEntity> : ProcessSystemMessage, ICurrentEntityChanged<TEntity>
    {
        public TEntity Entity { get; }
        public CurrentEntityChanged(TEntity entity, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Entity = entity;
        }
    }
}
