using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using Akka.Actor;
using CommonMessages;

namespace EventMessages
{


    public class CreateComplexEventService:ProcessSystemMessage, ICreateComplexEventService
    {
        public CreateComplexEventService(IComplexEventService complexEventService, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            ComplexEventService = complexEventService;
        }

        public IComplexEventService ComplexEventService { get;  }
    }
}
