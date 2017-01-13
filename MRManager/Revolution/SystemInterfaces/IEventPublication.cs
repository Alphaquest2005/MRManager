using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEventPublication
    {
        string Key { get; }
        Type EventType { get; }
    }



 }