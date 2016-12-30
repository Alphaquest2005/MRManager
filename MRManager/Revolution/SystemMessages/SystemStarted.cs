using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;


namespace SystemMessages
{
 
    [Export]
    public class SystemStarted : ProcessSystemMessage, ISystemStarted
    {
        public SystemStarted(ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
        }
    }
}
