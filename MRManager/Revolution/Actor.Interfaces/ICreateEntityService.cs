﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace Actor.Interfaces
{
    [InheritedExport]
    public interface ICreateEntityService : IProcessSystemMessage
    {
        Type ActorType { get; }
        object Action { get; }
    }
}
