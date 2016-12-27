using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;


namespace SystemMessages
{
    [Export]
    public class ServiceStarted<TService> : ProcessSystemMessage 
    {
        public ServiceStarted(ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
        }
    }
}
