using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages
{

    public class ComplexEventServiceCreated : ProcessSystemMessage, IComplexEventServiceCreated
    {
        public IComplexEventService Service { get; set; }

        public ComplexEventServiceCreated(IComplexEventService service, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Service = service;
        }
    }
}