﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    
    public interface IEntitySetLoaded<TEntity>:IProcessSystemMessage where TEntity : IEntity
    {
        IList<TEntity> Entities { get; }
    }
}
