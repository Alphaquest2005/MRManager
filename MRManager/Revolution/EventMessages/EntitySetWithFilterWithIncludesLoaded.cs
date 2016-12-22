﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntitySetWithFilterWithIncludesLoaded<T> : SystemProcessMessage where T : IEntity
    {
        public IList<T> Entities { get; }
        public IList<Expression<Func<T, dynamic>>> Includes { get; }

        public EntitySetWithFilterWithIncludesLoaded(IList<T> entities, IList<Expression<Func<T, dynamic>>> includes, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Entities = entities;
            Includes = includes;
            
        }
    }
}
