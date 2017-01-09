using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IRequestComplexEventLog : IProcessSystemMessage
    {
    }

    [InheritedExport]
    public interface IComplexEventLogCreated : IProcessSystemMessage
    {
        IComplexEventLog EventLog { get; }
    }

    public interface IComplexEventLog
    {
        //ToDo create eventlog
    }
}
