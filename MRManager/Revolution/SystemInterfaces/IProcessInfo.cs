﻿using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessInfo
    {
        int Id { get; }
        int ParentProcessId { get; }
        string Name { get; }
        string Description { get; }
        string Symbol { get; }
    }

    public interface IProcessInfo<TEntity>:IProcessInfo where TEntity:IEntity
    {
      Type EntityType { get; }
    }
}