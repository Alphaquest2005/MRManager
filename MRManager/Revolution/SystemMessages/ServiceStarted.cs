using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;


namespace SystemMessages
{
    [Export]
    public class ServiceStarted<TService> : SystemProcessMessage 
    {
        public ServiceStarted(ISystemProcess process, MessageSource source) : base(process, source)
        {
        }
    }
}
