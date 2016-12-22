using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace SystemMessages
{
    [Export]
    public class StartNextProcess : SystemProcessMessage
    {
        public StartNextProcess(ISystemProcess process, MessageSource source) : base(process, source)
        {

        }

    }
}