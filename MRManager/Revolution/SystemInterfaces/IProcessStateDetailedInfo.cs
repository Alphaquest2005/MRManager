﻿using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessStateInfo
    {
        int ProcessId { get; }
        IState State { get; }
    }
}