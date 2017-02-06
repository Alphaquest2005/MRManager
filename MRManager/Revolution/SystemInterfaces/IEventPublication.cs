using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IEventPublication
    {
        string Key { get; }
        Type EventType { get; }
    }



 }