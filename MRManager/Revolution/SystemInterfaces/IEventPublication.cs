using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEventPublication
    {
        Type EventType { get; }
    }



 }