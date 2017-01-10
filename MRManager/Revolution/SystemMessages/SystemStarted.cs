using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;


namespace SystemMessages
{
 
    [Export]
    public class SystemStarted : ProcessSystemMessage, ISystemStarted
    {
        public SystemStarted(IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }
    }
}
