using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    public class CleanUpSystemProcess : ProcessSystemMessage, ICleanUpSystemProcess
    {
        public int ProcessToBeCleanedUpId { get; }

        public CleanUpSystemProcess(int processId, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            ProcessToBeCleanedUpId = processId;
        }
    }
}