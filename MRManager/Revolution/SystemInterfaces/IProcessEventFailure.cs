﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessEventFailure
    {
        Type FailedEventType { get; set; }
        IProcessSystemMessage FailedEventMessage { get; set; }
        Type ExpectedEventType { get; set; }
        Exception Exception { get; set; }
    }
}