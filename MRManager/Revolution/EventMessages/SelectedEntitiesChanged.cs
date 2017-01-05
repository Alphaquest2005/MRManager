﻿using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class SelectedEntitiesChanged<T> : ProcessSystemMessage where T : IEntity
    {
        public IList<T> Changes { get; }
        
        public SelectedEntitiesChanged(IList<T> changes, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
           Changes = changes;
        }
    }
}
