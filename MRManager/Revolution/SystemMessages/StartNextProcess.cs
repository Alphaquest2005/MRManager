using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace SystemMessages
{
    [Export]
    public class StartNextProcess : ProcessSystemMessage
    {
        public StartNextProcess(ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {

        }

    }
}