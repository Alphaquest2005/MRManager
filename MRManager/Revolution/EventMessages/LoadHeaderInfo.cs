﻿using System;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadHeaderInfo<T> : ProcessSystemMessage where T:class, IEntity
    {
        public LoadHeaderInfo( Expression<Func<T,IHeaderInfo<T>>> selectExpression, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            SelectExpression = selectExpression;
        }

     
        public Expression<Func<T, IHeaderInfo<T>>> SelectExpression { get; }
    }
}
