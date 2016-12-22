﻿using SystemInterfaces;
using CommonMessages;
using DataInterfaces;
using ViewModelInterfaces;

namespace ViewMessages
{
    
    public class CreateViewModel<T> : SystemProcessMessage, ICreateViewModel<T> where T : IEntity
    {
        public CreateViewModel(ISystemProcess process, MessageSource source) : base(process,source)
        {
        }
    }
}
