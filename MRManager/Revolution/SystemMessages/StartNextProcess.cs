using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace SystemMessages
{
    [Export]
    public class StartNextProcess : ProcessSystemMessage
    {
        public StartNextProcess(ISystemProcess process, ISystemSource source) : base(process, source)
        {

        }

    }
}