using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;


namespace SystemMessages
{
   

    [Export]
    public class ServiceStarted<TService> : ProcessSystemMessage, IServiceStarted<TService>
    {
        public ServiceStarted(TService service, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Service = service;
        }

        public TService Service { get; }
    }
}
