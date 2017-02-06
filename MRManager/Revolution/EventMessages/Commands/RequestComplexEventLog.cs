using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IRequestComplexEventLog))]

    public class RequestComplexEventLog : ProcessSystemMessage, IRequestComplexEventLog
    {
        public RequestComplexEventLog() { }
        public RequestComplexEventLog(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }
    }
}
