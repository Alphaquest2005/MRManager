﻿using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ISystemProcessCompleted : IProcessSystemMessage
    {
    }
}