﻿using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
   

    public class LoadEntitySet<TEntity> : ProcessSystemMessage, ILoadEntitySet<TEntity> where TEntity : IEntity
    {
        public LoadEntitySet(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            
        }
    }
}