using System.ComponentModel.Composition;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages.Commands
{

    [Export(typeof(ICreateComplexEventService))]
    public class CreateComplexEventService:ProcessSystemMessage, ICreateComplexEventService
    {
        public CreateComplexEventService() { }
        public CreateComplexEventService(IComplexEventService complexEventService, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            ComplexEventService = complexEventService;
        }

        public IComplexEventService ComplexEventService { get;  }
    }
}
