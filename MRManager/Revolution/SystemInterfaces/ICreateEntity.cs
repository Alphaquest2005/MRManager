﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    
    public interface ICreateEntity<out TEntity>:IProcessSystemMessage, IEntityRequest<TEntity> where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}
