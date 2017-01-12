using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;


namespace SystemMessages
{
   

    [Export]
    public class ServiceStarted<TService> : ProcessSystemMessage, IServiceStarted<TService>
    {
        public ServiceStarted(TService service, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Service = service;
        }

        public TService Service { get; }
    }
}
