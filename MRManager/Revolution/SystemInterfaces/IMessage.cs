﻿using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IMessage : IEvent
    {
        DateTime MessageDateTime { get; }
    }
}